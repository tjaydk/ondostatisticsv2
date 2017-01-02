$ErrorActionPreference = 'Stop'

$EFDefaultParameterValues = @{
    ProjectName = ''
    ContextTypeName = ''
}

#
# Use-DbContext
#

Register-TabExpansion Use-DbContext @{
    Context = { param ($tabExpansionContext) GetContextTypes $tabExpansionContext.Project $tabExpansionContext.StartupProject $tabExpansionContext.Environment }
    Project = { GetProjects }
    StartupProject = { GetProjects }
}

<#
.SYNOPSIS
    Sets the default DbContext to use.

.DESCRIPTION
    Sets the default DbContext to use.

.PARAMETER Context
    Specifies the DbContext to use.

.PARAMETER Project
    Specifies the project to use. If omitted, the default project is used.

.PARAMETER StartupProject
    Specifies the startup project to use. If omitted, the solution's startup project is used.

.PARAMETER Environment
    Specifies the environment to use. If omitted, "Development" is used.

.LINK
    about_EntityFrameworkCore
#>
function Use-DbContext {
    [CmdletBinding(PositionalBinding = $false)]
    param ([Parameter(Position = 0, Mandatory = $true)] [string] $Context, [string] $Project, [string] $StartupProject, [string] $Environment)

    $dteProject = GetProject $Project

    $contextTypes = GetContextTypes $Project $StartupProject $Environment
    $candidates = $contextTypes | ? { $_ -ilike "*$Context" }
    $exactMatch = $contextTypes | ? { $_ -eq $Context }
    if ($candidates.length -gt 1 -and $exactMatch -is 'String') {
        $candidates = $exactMatch
    }

    if ($candidates.length -lt 1) {
        throw "No DbContext named '$Context' was found"
    } elseif ($candidates.length -gt 1 -and !($candidates -is 'String')) {
        throw "More than one DbContext named '$Context' was found. Specify which one to use by providing its fully qualified name."
    }

    $EFDefaultParameterValues.ContextTypeName = $candidates
    $EFDefaultParameterValues.ProjectName = $dteProject.ProjectName
}

#
# Add-Migration
#

Register-TabExpansion Add-Migration @{
    Context = { param ($tabExpansionContext) GetContextTypes $tabExpansionContext.Project $tabExpansionContext.StartupProject $tabExpansionContext.Environment }
    Project = { GetProjects }
    StartupProject = { GetProjects }
    # Disables tab completion on output dir.
    # PMC will show completion options relative to the solution directory, but OutputDir should be relative to project
    OutputDir = { }
}

<#
.SYNOPSIS
    Adds a new migration.

.DESCRIPTION
    Adds a new migration.

.PARAMETER Name
    Specifies the name of the migration.

.PARAMETER OutputDir
    The directory (and sub-namespace) to use. If omitted, "Migrations" is used. Relative paths are relative to project directory.

.PARAMETER Context
    Specifies the DbContext to use. If omitted, the default DbContext is used.

.PARAMETER Project
    Specifies the project to use. If omitted, the default project is used.

.PARAMETER StartupProject
    Specifies the startup project to use. If omitted, the solution's startup project is used.

.PARAMETER Environment
    Specifies the environment to use. If omitted, "Development" is used.

.LINK
    Remove-Migration
    Update-Database
    about_EntityFrameworkCore
#>
function Add-Migration {
    [CmdletBinding(PositionalBinding = $false)]
    param (
        [Parameter(Position = 0, Mandatory = $true)]
        [string] $Name,
        [string] $OutputDir,
        [string] $Context,
        [string] $Project,
        [string] $StartupProject,
        [string] $Environment)

    WhichEF $MyInvocation.MyCommand
    $values = ProcessCommonParameters $StartupProject $Project $Context $Environment

    $options = @()
    if($OutputDir) {
        $options += '--output-dir', $OutputDir
    }
    $artifacts = InvokeOperation $values -json migrations add $Name @options

    if (!(IsDotNetProject $values.Project)) {
        if ($artifacts.MigrationFile) {
            $values.Project.ProjectItems.AddFromFile($artifacts.MigrationFile) | Out-Null
        }
        try {
            $values.Project.ProjectItems.AddFromFile($artifacts.MetadataFile) | Out-Null
        } catch {
            # in some SKUs the call to add MigrationFile will automatically add the MetadataFile because it is named ".Designer.cs"
            # this will throw a non fatal error when -OutputDir is outside the main project directory
        }

        if ($artifacts.SnapshotFile) {
            $values.Project.ProjectItems.AddFromFile($artifacts.SnapshotFile) | Out-Null
        }
    }

    if ($artifacts.MigrationFile) {
        $DTE.ItemOperations.OpenFile($artifacts.MigrationFile) | Out-Null
    }
    ShowConsole
    Write-Output 'To undo this action, use Remove-Migration.'
}


#
# Drop-Database
#

Register-TabExpansion Drop-Database @{
    Context = { param ($tabExpansionContext) GetContextTypes $tabExpansionContext.Project $tabExpansionContext.StartupProject $tabExpansionContext.Environment }
    Project = { GetProjects }
    StartupProject = { GetProjects }
}

<#
.SYNOPSIS
    Drops the database.

.DESCRIPTION
    Drops the database.

.PARAMETER Context
    Specifies the DbContext to use. If omitted, the default DbContext is used.

.PARAMETER Project
    Specifies the project to use. If omitted, the default project is used.

.PARAMETER StartupProject
    Specifies the startup project to use. If omitted, the solution's startup project is used.

.PARAMETER Environment
    Specifies the environment to use. If omitted, "Development" is used.

.PARAMETER WhatIf
    Displays a message describing the effect of the database drop without actually dropping the database.

.LINK
    Update-Database
    about_EntityFrameworkCore
#>
function Drop-Database {
    [CmdletBinding(PositionalBinding = $false,
        SupportsShouldProcess = $true,
        ConfirmImpact = 'High')]
    param (
        [string] $Context,
        [string] $Project,
        [string] $StartupProject,
        [string] $Environment)

    $values = ProcessCommonParameters $StartupProject $Project $Context $Environment
    if (IsUwpProject $values.Project) {
        throw 'Drop-Database should not be used with Universal Windows apps. Instead, call DbContext.Database.EnsureDeleted() at runtime.'
    }

    $info = InvokeOperation $values -json database drop --dry-run

    ShowConsole

    if (!($info)) {
        Write-Output 'Could not find a database to remove'
        return
    }

    $msg = 'This command will permanently drop the database:'
    $msg += "`n    Database name : $($info.databaseName)"
    $msg += "`n    Data source   : $($info.dataSource)"

    if ($PSCmdlet.ShouldProcess(
            "Dropping database '$($info.databaseName)' on '$($info.dataSource)'", # verbose and what-if output
            'Are you sure you want to proceed?', # question
            $msg # caption
            )) {
        Write-Output 'Starting database drop'
        InvokeOperation $values -skipBuild database drop --force
    }
}

#
# Update-Database
#

Register-TabExpansion Update-Database @{
    Migration = { param ($tabExpansionContext) GetMigrations $tabExpansionContext.Context $tabExpansionContext.Project $tabExpansionContext.StartupProject $tabExpansionContext.Environment }
    Context = { param ($tabExpansionContext) GetContextTypes $tabExpansionContext.Project $tabExpansionContext.StartupProject $tabExpansionContext.Environment }
    Project = { GetProjects }
    StartupProject = { GetProjects }
}

<#
.SYNOPSIS
    Updates the database to a specified migration.

.DESCRIPTION
    Updates the database to a specified migration.

.PARAMETER Migration
    Specifies the target migration. If '0', all migrations will be reverted. If omitted, all pending migrations will be applied.

.PARAMETER Context
    Specifies the DbContext to use. If omitted, the default DbContext is used.

.PARAMETER Project
    Specifies the project to use. If omitted, the default project is used.

.PARAMETER StartupProject
    Specifies the startup project to use. If omitted, the solution's startup project is used.

.PARAMETER Environment
    Specifies the environment to use. If omitted, "Development" is used.

.LINK
    Script-Migration
    about_EntityFrameworkCore
#>
function Update-Database {
    [CmdletBinding(PositionalBinding = $false)]
    param (
        [Parameter(Position = 0)]
        [string] $Migration,
        [string] $Context,
        [string] $Project,
        [string] $StartupProject,
        [string] $Environment)

    WhichEF $MyInvocation.MyCommand
    $values = ProcessCommonParameters $StartupProject $Project $Context $Environment
    if (IsUwpProject $values.Project) {
        throw 'Update-Database should not be used with Universal Windows apps. Instead, call DbContext.Database.Migrate() at runtime.'
    }

    InvokeOperation $values database update $Migration
    ShowConsole
}

#
# Script-Migration
#

Register-TabExpansion Script-Migration @{
    From = { param ($tabExpansionContext) GetMigrations $tabExpansionContext.Context $tabExpansionContext.Project $tabExpansionContext.StartupProject $tabExpansionContext.Environment }
    To = { param ($tabExpansionContext) GetMigrations $tabExpansionContext.Context $tabExpansionContext.Project $tabExpansionContext.StartupProject $tabExpansionContext.Environment }
    Context = { param ($tabExpansionContext) GetContextTypes $tabExpansionContext.Project $tabExpansionContext.StartupProject $tabExpansionContext.Environment }
    Project = { GetProjects }
    StartupProject = { GetProjects }
}

<#
.SYNOPSIS
    Generates a SQL script from migrations.

.DESCRIPTION
    Generates a SQL script from migrations.

.PARAMETER From
    Specifies the starting migration. If omitted, '0' (the initial database) is used.

.PARAMETER To
    Specifies the ending migration. If omitted, the last migration is used.

.PARAMETER Idempotent
    Generates an idempotent script that can be used on a database at any migration.

.PARAMETER Context
    Specifies the DbContext to use. If omitted, the default DbContext is used.

.PARAMETER Project
    Specifies the project to use. If omitted, the default project is used.

.PARAMETER StartupProject
    Specifies the startup project to use. If omitted, the solution's startup project is used.

.PARAMETER Environment
    Specifies the environment to use. If omitted, "Development" is used.

.LINK
    Update-Database
    about_EntityFrameworkCore
#>
function Script-Migration {
    [CmdletBinding(PositionalBinding = $false)]
    param (
        [Parameter(ParameterSetName = 'WithoutTo')]
        [Parameter(ParameterSetName = 'WithTo', Mandatory = $true)]
        [string] $From,
        [Parameter(ParameterSetName = 'WithTo', Mandatory = $true)]
        [string] $To,
        [switch] $Idempotent,
        [string] $Context,
        [string] $Project,
        [string] $StartupProject,
        [string] $Environment)

    $values = ProcessCommonParameters $StartupProject $Project $Context $Environment

    $fullPath = GetProperty $values.Project.Properties FullPath
    $intermediatePath = if (IsDotNetProject $values.Project) { 'obj\Debug\' }
        else { GetProperty $values.Project.ConfigurationManager.ActiveConfiguration.Properties IntermediatePath }
    $fullIntermediatePath = Join-Path $fullPath $intermediatePath
    $fileName = [IO.Path]::GetRandomFileName()
    $fileName = [IO.Path]::ChangeExtension($fileName, '.sql')
    $scriptFile = Join-Path $fullIntermediatePath $fileName

    $options = '--output',$scriptFile
    if ($Idempotent) {
        $options += ,'--idempotent'
    }

    InvokeOperation $values migrations script $From $To @options

    $DTE.ItemOperations.OpenFile($scriptFile) | Out-Null

    ShowConsole
}

#
# Remove-Migration
#

Register-TabExpansion Remove-Migration @{
    Context = { param ($tabExpansionContext) GetContextTypes $tabExpansionContext.Project $tabExpansionContext.StartupProject $tabExpansionContext.Environment }
    Project = { GetProjects }
    StartupProject = { GetProjects }
}

<#
.SYNOPSIS
    Removes the last migration.

.DESCRIPTION
    Removes the last migration.

.PARAMETER Context
    Specifies the DbContext to use. If omitted, the default DbContext is used.

.PARAMETER Project
    Specifies the project to use. If omitted, the default project is used.

.PARAMETER StartupProject
    Specifies the startup project to use. If omitted, the solution's startup project is used.

.PARAMETER Environment
    Specifies the environment to use. If omitted, "Development" is used.

.PARAMETER Force
    Removes the last migration without checking the database. If the last migration has been applied to the database, you will need to manually reverse the changes it made.

.LINK
    Add-Migration
    about_EntityFrameworkCore
#>
function Remove-Migration {
    [CmdletBinding(PositionalBinding = $false)]
    param ([string] $Context, [string] $Project, [string] $StartupProject, [string] $Environment, [switch] $Force)

    $values = ProcessCommonParameters $StartupProject $Project $Context $Environment

    $forceRemove = $Force -or (IsUwpProject $values.Project)

    $options=@()
    if ($forceRemove) {
        $options += ,'--force'
    }

    $result = InvokeOperation $values -json migrations remove @options

    if (!(IsDotNetProject $values.Project) -and $result.files) {
        $result.files | %{
            $projectItem = GetProjectItem $values.Project $_
            if ($projectItem) {
                $projectItem.Remove()
            }
        }
    }

    ShowConsole
}

#
# Scaffold-DbContext
#

Register-TabExpansion Scaffold-DbContext @{
    Provider = { param ($tabExpansionContext) GetProviders $tabExpansionContext.Project }
    Project = { GetProjects }
    StartupProject = { GetProjects }
    # Disables tab completion on output dir.
    # PMC will show completion options relative to the solution directory, but OutputDir should be relative to project
    OutputDir = { }
}

<#
.SYNOPSIS
    Scaffolds a DbContext and entity type classes for a specified database.

.DESCRIPTION
    Scaffolds a DbContext and entity type classes for a specified database.

.PARAMETER Connection
    Specifies the connection string of the database.

.PARAMETER Provider
    Specifies the provider to use. For example, Microsoft.EntityFrameworkCore.SqlServer.

.PARAMETER OutputDir
    Specifies the directory to use to output the classes. If omitted, the top-level project directory is used.

.PARAMETER Context
    Specifies the name of the generated DbContext class.

.PARAMETER Schemas
    Specifies the schemas for which to generate classes.

.PARAMETER Tables
    Specifies the tables for which to generate classes.

.PARAMETER DataAnnotations
    Use DataAnnotation attributes to configure the model where possible. If omitted, the output code will use only the fluent API.

.PARAMETER Force
    Force scaffolding to overwrite existing files. Otherwise, the code will only proceed if no output files would be overwritten.

.PARAMETER Project
    Specifies the project to use. If omitted, the default project is used.

.PARAMETER StartupProject
    Specifies the startup project to use. If omitted, the solution's startup project is used.

.PARAMETER Environment
    Specifies the environment to use. If omitted, "Development" is used.

.LINK
    about_EntityFrameworkCore
#>
function Scaffold-DbContext {
    [CmdletBinding(PositionalBinding = $false)]
    param (
        [Parameter(Position = 0, Mandatory = $true)]
        [string] $Connection,
        [Parameter(Position = 1, Mandatory =  $true)]
        [string] $Provider,
        [string] $OutputDir,
        [string] $Context,
        [string[]] $Schemas = @(),
        [string[]] $Tables = @(),
        [switch] $DataAnnotations,
        [switch] $Force,
        [string] $Project,
        [string] $StartupProject,
        [string] $Environment)

    $values = ProcessCommonParameters $StartupProject $Project $Context $Environment

    $options = @()
    if ($OutputDir) {
        $options += '--output-dir', $OutputDir
    }
    if ($DataAnnotations) {
        $options += ,'--data-annotations'
    }
    if ($Force) {
        $options += ,'--force'
    }
    $options += $Schemas | % { '--schema', $_ }
    $options += $Tables | % { '--table', $_ }

    $result = InvokeOperation $values -json dbcontext scaffold $Connection $Provider @options

    if (!(IsDotNetProject $values.Project) -and $result.files) {
        $result.files | %{ $values.Project.ProjectItems.AddFromFile($_) | Out-Null }
        $DTE.ItemOperations.OpenFile($result.files[0]) | Out-Null
    }

    ShowConsole
}

#
# Enable-Migrations (Obsolete)
#

function Enable-Migrations {
    # TODO: Link to some docs on the changes to Migrations
    WhichEF $MyInvocation.MyCommand
    Write-Warning 'Enable-Migrations is obsolete. Use Add-Migration to start using Migrations.'
}

#
# (Private Helpers)
#

function GetProjects {
    $projects = Get-Project -All
    $groups = $projects | group Name

    return $projects | %{
        if ($groups | ? Name -eq $_.Name | ? Count -eq 1) {
            return $_.Name
        }

        return $_.ProjectName
    }
}

function GetContextTypes($projectName, $startupProjectName, $environment) {
    $values = ProcessCommonParameters $startupProjectName $projectName $null $environment -suppressContextOption
    $types = InvokeOperation $values -json -skipBuild dbcontext list
    return $types | %{ $_.safeName }
}

function GetMigrations($contextTypeName, $projectName, $startupProjectName, $environment) {
    $values = ProcessCommonParameters $startupProjectName $projectName $contextTypeName $environment
    $migrations = InvokeOperation $values -json -skipBuild migrations list
    return $migrations | %{ $_.safeName }
}

function IsDotNetProject($project) {
    $project.FileName -like '*.xproj' -or $project.Kind -eq '{8BB2217D-0F2D-49D1-97BC-3654ED321F3B}'
}

function IsUwpProject($project) {
    $targetDesription = GetProperty $project.Properties Project.TargetDescriptions
    return $targetDesription -eq 'Universal Windows'
}

function IsClassLibrary($project) {
    if (IsDotNetProject $project) {
        return GetProperty $project.Properties IsClasslibraryProject
    }
    $type = GetProperty $project.Properties OutputType
    return $type -eq 2 -or $type -eq 'Library'
}

function GetProject($projectName) {
    if ($projectName) {
        return Get-Project $projectName
    }

    return Get-Project
}

function ShowConsole {
    $componentModel = Get-VSComponentModel
    $powerConsoleWindow = $componentModel.GetService([NuGetConsole.IPowerConsoleWindow])
    $powerConsoleWindow.Show()
}

function GetDotNet {
    if ($env:DOTNET_INSTALL_DIR) {
        $dotnet = Join-Path $env:DOTNET_INSTALL_DIR dotnet.exe
    } else {
        $cmd = Get-Command dotnet -ErrorAction Ignore # searches $env:PATH
        if ($cmd) {
            $dotnet = $cmd.Path
        }
    }

    if (!(Test-Path $dotnet)) {
        throw 'Could not find .NET Core SDK (dotnet.exe) in the PATH or DOTNET_INSTALL_DIR environment variables. .NET Core SDK is required to execute EF commands on this project type.'
    }
    return $dotnet
}

function GetTargetFramework($projectDir) {
    $targetProjectJson = Join-Path $projectDir project.json
    try {
        Write-Debug "Reading $targetProjectJson"
        $project = Get-Content $targetProjectJson -Raw | ConvertFrom-Json
    } catch {
        Write-Verbose $_.Exception.Message
        throw "Invalid JSON file in '$targetProjectJson'"
    }

    $frameworks = $project.frameworks | Get-Member -MemberType NoteProperty | % Name
    if ($frameworks -is 'String') {
        # when there is one framework listed
        return $frameworks
    }
    $choices = [System.Management.Automation.Host.ChoiceDescription[]]( $frameworks | % {$i=0} { new-object System.Management.Automation.Host.ChoiceDescription "Option &$i`: $_"; $i++} )
    $choice = $Host.Ui.PromptForChoice('Multiple target frameworks available', 'Which framework should the command use?', $choices, 0)
    return $frameworks[$choice]
}

function GetDotNetArguments($startupProject, $outputFileName) {
    #TODO use more CPS APIs when/if they become available

    $startupProjectDir = GetProperty $startupProject.Properties FullPath
    $tfm = GetTargetFramework $startupProjectDir
    Write-Verbose "Using framework '$tfm'"
    # only returns part of the actual output path
    $outputPath = Join-Path $startupProjectDir (GetProperty $startupProject.ConfigurationManager.ActiveConfiguration.Properties OutputPath)

    $config = $startupProject.ConfigurationManager.ActiveConfiguration.ConfigurationName

    # TODO get the actual output file from VS when/if CPS API's become available
    $assemblyName = GetProperty $startupProject.Properties AssemblyName

    $arguments = @()
    if ($tfm -like 'net45*' -or $tfm -like 'net46*') {
        # TODO get the actual output file from VS when/if CPS API's become available
        $startupOutputFileName = "$assemblyName.exe"
        # TODO determine if desktop app is x86 or has a different runtimes
        $outputPath = Join-Path $outputPath "$config/$tfm/win7-x64/"
        $exe = Join-Path $PSScriptRoot 'net451/ef.exe'
    } elseif ($tfm -eq 'netcoreapp1.0') {
        # TODO handle self-contained apps

        $outputPath = Join-Path $outputPath "$config/$tfm"
        $exe = GetDotNet
        $arguments += 'exec'
        $arguments += '--additionalprobingpath', "$env:USERPROFILE/.nuget/packages"
        $arguments += '--depsfile', (Join-Path $outputPath "$assemblyName.deps.json")
        $arguments += '--runtimeconfig', (Join-Path $outputPath "$assemblyName.runtimeconfig.json")
        $arguments += Join-Path $PSScriptRoot 'netcoreapp1.0/ef.dll'
        $startupOutputFileName = "$assemblyName.dll"
    } else {
        throw "Commands could not invoke on target framework '$tfm'.`nCommands on ASP.NET Core and .NET Core projects currently only support .NET Core ('netcoreapp1.0') or .NET Framework (e.g. 'net451') target frameworks."
    }


    if ($startupProjectDir -eq $projectDir) {
        $outputFileName = $startupOutputFileName
    }

    $arguments += '--assembly', (Join-Path $outputPath $outputFileName)
    $arguments += '--startup-assembly', (Join-Path $outputPath $startupOutputFileName)

    Write-Verbose "Using data directory '$outputPath'"
    $arguments += '--data-dir', $outputPath

    return @{
        Arguments = $arguments
        Executable = $exe
        OutputPath = $outputPath
    }
}

function GetCsprojArguments($startupProject, $outputFileName) {
    $startupProjectDir = GetProperty $startupProject.Properties FullPath
    $outputPath = Join-Path $startupProjectDir (GetProperty $startupProject.ConfigurationManager.ActiveConfiguration.Properties OutputPath)
    $startupOutputFileName = GetProperty $startupProject.Properties OutputFileName
    $webConfig = GetProjectItem $startupProject 'Web.Config'
    $appConfig = GetProjectItem $startupProject 'App.Config'
    $dataDirectory = $outputPath
    if ($webConfig) {
        $configurationFile = GetProperty $webConfig.Properties FullPath
        $dataDirectory = Join-Path $startupProjectDir 'App_Data'
    } elseif ($appConfig) {
        $configurationFile = GetProperty $appConfig.Properties FullPath
    } elseif (IsUwpProject $startupProject) {
        $configurationFile = Join-Path $PSScriptRoot 'uap10.0/ef.exe.config'
    }

    $arch = GetProperty $startupProject.ConfigurationManager.ActiveConfiguration.Properties PlatformTarget
    Write-Verbose "Startup project architecture is '$arch'"
    if ($arch -eq 'x86') {
        $exe = Join-Path $PSScriptRoot 'net451/ef.x86.exe'
    } elseif ($arch -eq 'AnyCPU' -or $arch -eq 'x64') {
        $exe = Join-Path $PSScriptRoot 'net451/ef.exe'
    } else {
        throw "This command cannot use '$($startupProject.Name)' as the startup project because it targets an unsupported platform architecture '$arch'. Change the startup project or platform configuration and run this command again."
    }

    $arguments += '--assembly', (Join-Path $outputPath $outputFileName)
    $arguments += '--startup-assembly', (Join-Path $outputPath $startupOutputFileName)
    $arguments += '--data-dir', $dataDirectory

    if ($configurationFile -and !(IsUwpProject $startupProject)) {
        $arguments += '--config', $configurationFile
    }

    return @{
        Arguments = $arguments
        Executable = $exe
        OutputPath = $outputPath
        ConfigFile = $configurationFile
    }
}

function ProcessCommonParameters($startupProjectName, $projectName, $contextTypeName, $environment, [switch] $suppressContextOption) {
    $project = GetProject $projectName
    $projectDir = GetProperty $project.Properties FullPath

    if (!$contextTypeName -and $project.ProjectName -eq $EFDefaultParameterValues.ProjectName) {
        $contextTypeName = $EFDefaultParameterValues.ContextTypeName
    }

    $startupProject = GetStartupProject $startupProjectName $project
    $startupProjectDir = GetProperty $startupProject.Properties FullPath

    # Enforce project-type restrictions
    if ((IsUwpProject $startupProject) -and (IsClassLibrary $startupProject)) {
        throw "This command cannot use '$($startupProject.Name)' as the startup project because it is a Univeral Windows class library project. Change the startup project to a Universal Windows application project and run this command again."
    }

    if ((IsDotNetProject $startupProject) -and (IsClassLibrary $startupProject)) {
        throw "Could not invoke this command on the startup project '$($startupProject.Name)'.`nEntity Framework Core does not support commands on class library projects in ASP.NET Core and .NET Core applications."
    }

    if (IsDotNetProject $project) {
        $rootNamespace = GetProperty $project.Properties RootNamespace
        # TODO get the actual output file from VS when/if CPS API's allow
        $outputFileName = GetProperty $project.Properties AssemblyName
        $outputFileName += '.dll'
    } else {
        $outputFileName = GetProperty $project.Properties OutputFileName
        $rootNamespace = GetProperty $project.Properties DefaultNamespace
    }

    if (IsDotNetProject $startupProject) {
        if (!(IsDotNetProject $project)) {
            Write-Warning "This command may fail unless both the targeted project and startup project are ASP.NET Core or .NET Core projects."
        }

        $values = GetDotNetArguments $startupProject $outputFileName
    } else {
        $values = GetCsprojArguments $startupProject $outputFileName
    }

    $arguments = @()
    $arguments += $values.Arguments
    $arguments += '--no-color', '--prefix-output', '--verbose'
    $arguments += '--project-dir', $projectDir
    $arguments += '--content-root-path', $startupProjectDir

    if ($rootNamespace) {
        $arguments += '--root-namespace', $rootNamespace
    }

    $options=@()
    if ($environment) {
        $options += '--environment', $environment
    }
    if ($contextTypeName -and !$suppressContextOption) {
        $options += '--context', $contextTypeName
    }

    return @{
        Project = $project
        StartupProject = $startupProject
        Executable = $values.Executable
        OutputPath = $values.OutputPath
        ConfigFile = $values.ConfigFile
        Arguments = $arguments
        Options = $options
    }
}

function InvokeOperation($commonParams, [switch] $json, [switch] $skipBuild) {
    $project = $commonParams.Project
    $startupProject = $commonParams.StartupProject

    if (!$skipBuild) {
        if (IsUwpProject $startupProject) {
            $config = $startupProject.ConfigurationManager.ActiveConfiguration.ConfigurationName
            $configProperties = $startupProject.ConfigurationManager.ActiveConfiguration.Properties
            $isNative = (GetProperty $configProperties ProjectN.UseDotNetNativeToolchain) -eq 'True'

            if ($isNative) {
                throw "Cannot run in '$config' mode because 'Compile with the .NET Native tool chain' is enabled. Disable this setting or use a different configuration and try again."
            }
        }

        Write-Verbose 'Build started...'

        # TODO: Only build required project. Don't use BuildProject, you can't specify platform
        $solutionBuild = $DTE.Solution.SolutionBuild
        $solutionBuild.Build($true)
        if ($solutionBuild.LastBuildInfo) {
            throw 'Build failed.'
        }

        Write-Verbose 'Build succeeded.'
    }

    $output = $null

    $arguments = @()

    if (IsUwpProject $startupProject) {
        $arguments += , '--no-appdomain'
        $exeCopied = $true
    }

    $arguments += $commonParams.Arguments
    $arguments += $args
    $arguments += $commonParams.Options

    if ($json) {
        $arguments += '--json'
    }

    try {
        $exe = $commonParams.Executable
        if ($exeCopied) {
            Write-Debug "Copying '$($commonParams.Executable)' to '$($commonParams.OutputPath)'"
            Copy-Item $commonParams.Executable $commonParams.OutputPath
            $exeFileName = Split-Path -Leaf $commonParams.Executable
            $exe = Join-Path $commonParams.OutputPath $exeFileName
            if ($commonParams.ConfigFile) {
                # copy binding redirects
                $configFile = Join-Path $commonParams.OutputPath "$exeFileName.config"
                Write-Debug "Copying config file '$($commonParams.ConfigFile)' to '$configFile'"
                Copy-Item $commonParams.ConfigFile $configFile
            }
        }

        try {
            $intermediatePath = Join-Path (GetProperty $commonParams.StartupProject.Properties FullPath) obj
            $rspFile = Join-Path $intermediatePath 'ef.rsp'
            $exe | Out-File -FilePath $rspFile -Confirm:$false -WhatIf:$false
            $arguments | Out-File -FilePath $rspFile -Append -Confirm:$false -WhatIf:$false
        } catch {
            Write-Debug 'Failed to write rsp file'
        }

        Write-Verbose "Running '$exe'"

        # Invoke-Process comes from the native module Microsoft.EntityFrameworkCore.Tools
        if ($json) {
            Invoke-Process -Executable $exe -Arguments $arguments -RedirectByPrefix -ErrorAction SilentlyContinue -ErrorVariable invokeErrors -JsonOutput | ConvertFrom-Json
        } else {
            # don't capture output. Output lines flow through
            Invoke-Process -Executable $exe -Arguments $arguments -RedirectByPrefix -ErrorAction SilentlyContinue -ErrorVariable invokeErrors
        }

    } finally {
        if ($exeCopied) {
            Write-Debug "Cleaning up '$exe' and '$configFile'"
            Remove-Item $exe -ErrorAction SilentlyContinue | Out-Null
            if ($configFile) {
                Remove-Item $configFile -ErrorAction SilentlyContinue | Out-Null
            }
        }
    }

    if ($invokeErrors) {
        $combined = ($invokeErrors | `
            ? { !($_.Exception.Message -like '*non-zero exit code')} | `
            % { $_.Exception.Message }) -join "`n"
        if (!$combined) {
            $lastError = $invokeErrors | Select-Object -Last 1
            if (!$lastError.Exception.Message) {
                throw 'Operation failed with unspecified error'
            }

            throw $lastError.Exception.Message
        }
        throw $combined
    }
}

function GetProperty($properties, $propertyName) {
    try {
        return $properties.Item($propertyName).Value
    } catch {
        return $null
    }
}

function GetProjectItem($project, $path) {
    $fullPath = GetProperty $project.Properties FullPath

    if (Split-Path $path -IsAbsolute) {
        $path = $path.Substring($fullPath.Length)
    }

    $itemDirectory = (Split-Path $path -Parent)

    $projectItems = $project.ProjectItems
    if ($itemDirectory) {
        $directories = $itemDirectory.Split('\')
        $directories | %{
            $projectItems = $projectItems.Item($_).ProjectItems
        }
    }

    $itemName = Split-Path $path -Leaf

    try {
        return $projectItems.Item($itemName)
    }
    catch [Exception] {
    }

    return $null
}

function GetStartUpProject($name, $fallbackProject) {
    if ($name) {
        return Get-Project $name
    }

    $startupProjectPaths = $DTE.Solution.SolutionBuild.StartupProjects
    if ($startupProjectPaths) {
        if ($startupProjectPaths.Length -eq 1) {
            $startupProjectPath = $startupProjectPaths[0]
            if (!(Split-Path -IsAbsolute $startupProjectPath)) {
                $solutionPath = Split-Path (GetProperty $DTE.Solution.Properties Path)
                $startupProjectPath = Join-Path $solutionPath $startupProjectPath -Resolve
            }

            $startupProject = GetSolutionProjects | ?{
                try {
                    $fullName = $_.FullName
                }
                catch [NotImplementedException] {
                    return $false
                }

                if ($fullName -and $fullName.EndsWith('\')) {
                    $fullName = $fullName.Substring(0, $fullName.Length - 1)
                }

                return $fullName -eq $startupProjectPath
            }
            if ($startupProject) {
                return $startupProject
            }

            Write-Warning "Unable to resolve startup project '$startupProjectPath'."
        }
        else {
            Write-Verbose 'More than one startup project found.'
        }
    }
    else {
        Write-Verbose 'No startup project found.'
    }

    return $fallbackProject
}

function GetSolutionProjects() {
    $projects = New-Object System.Collections.Stack

    $DTE.Solution.Projects | %{
        $projects.Push($_)
    }

    while ($projects.Count -ne 0) {
        $project = $projects.Pop();

        # NOTE: This line is similar to doing a "yield return" in C#
        $project

        if ($project.ProjectItems) {
            $project.ProjectItems | ?{ $_.SubProject } | %{
                $projects.Push($_.SubProject)
            }
        }
    }
}

function GetProviders($projectName) {
    if (!($projectName)) {
        $projectName = (Get-Project).ProjectName
    }

    return Get-Package -ProjectName $projectName | select -ExpandProperty Id
}

function WhichEF ($name) {
    if (Get-Module | ? Name -eq EntityFramework) {
        Write-Warning "Both Entity Framework Core and Entity Framework 6.x commands are installed. The Entity Framework Core version is executing. You can fully qualify the command to select which one to execute, 'EntityFramework\$name' for EF6.x and 'EntityFrameworkCore\$name' for EF Core."
    }
}
# SIG # Begin signature block
# MIIkCAYJKoZIhvcNAQcCoIIj+TCCI/UCAQExDzANBglghkgBZQMEAgEFADB5Bgor
# BgEEAYI3AgEEoGswaTA0BgorBgEEAYI3AgEeMCYCAwEAAAQQH8w7YFlLCE63JNLG
# KX7zUQIBAAIBAAIBAAIBAAIBADAxMA0GCWCGSAFlAwQCAQUABCBvgxop8G9Rz6/h
# eh5wiM7msPTLnRm9bMhmQDyBINCut6CCDZIwggYQMIID+KADAgECAhMzAAAAZEeE
# lIbbQRk4AAAAAABkMA0GCSqGSIb3DQEBCwUAMH4xCzAJBgNVBAYTAlVTMRMwEQYD
# VQQIEwpXYXNoaW5ndG9uMRAwDgYDVQQHEwdSZWRtb25kMR4wHAYDVQQKExVNaWNy
# b3NvZnQgQ29ycG9yYXRpb24xKDAmBgNVBAMTH01pY3Jvc29mdCBDb2RlIFNpZ25p
# bmcgUENBIDIwMTEwHhcNMTUxMDI4MjAzMTQ2WhcNMTcwMTI4MjAzMTQ2WjCBgzEL
# MAkGA1UEBhMCVVMxEzARBgNVBAgTCldhc2hpbmd0b24xEDAOBgNVBAcTB1JlZG1v
# bmQxHjAcBgNVBAoTFU1pY3Jvc29mdCBDb3Jwb3JhdGlvbjENMAsGA1UECxMETU9Q
# UjEeMBwGA1UEAxMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMIIBIjANBgkqhkiG9w0B
# AQEFAAOCAQ8AMIIBCgKCAQEAky7a2OY+mNkbD2RfTahYTRQ793qE/DwRMTrvicJK
# LUGlSF3dEp7vq2YoNNV9KlV7TE2K8sDxstNSFYu2swi4i1AL3X/7agmg3GcExPHf
# vHUYIEC+eCyZVt3u9S7dPkL5Wh8wrgEUirCCtVGg4m1l/vcYCo0wbU06p8XzNi3u
# XyygkgCxHEziy/f/JCV/14/A3ZduzrIXtsccRKckyn6B5uYxuRbZXT7RaO6+zUjQ
# hiyu3A4hwcCKw+4bk1kT9sY7gHIYiFP7q78wPqB3vVKIv3rY6LCTraEbjNR+phBQ
# EL7hyBxk+ocu+8RHZhbAhHs2r1+6hURsAg8t4LAOG6I+JQIDAQABo4IBfzCCAXsw
# HwYDVR0lBBgwFgYIKwYBBQUHAwMGCisGAQQBgjdMCAEwHQYDVR0OBBYEFFhWcQTw
# vbsz9YNozOeARvdXr9IiMFEGA1UdEQRKMEikRjBEMQ0wCwYDVQQLEwRNT1BSMTMw
# MQYDVQQFEyozMTY0Mis0OWU4YzNmMy0yMzU5LTQ3ZjYtYTNiZS02YzhjNDc1MWM0
# YjYwHwYDVR0jBBgwFoAUSG5k5VAF04KqFzc3IrVtqMp1ApUwVAYDVR0fBE0wSzBJ
# oEegRYZDaHR0cDovL3d3dy5taWNyb3NvZnQuY29tL3BraW9wcy9jcmwvTWljQ29k
# U2lnUENBMjAxMV8yMDExLTA3LTA4LmNybDBhBggrBgEFBQcBAQRVMFMwUQYIKwYB
# BQUHMAKGRWh0dHA6Ly93d3cubWljcm9zb2Z0LmNvbS9wa2lvcHMvY2VydHMvTWlj
# Q29kU2lnUENBMjAxMV8yMDExLTA3LTA4LmNydDAMBgNVHRMBAf8EAjAAMA0GCSqG
# SIb3DQEBCwUAA4ICAQCI4gxkQx3dXK6MO4UktZ1A1r1mrFtXNdn06DrARZkQTdu0
# kOTLdlGBCfCzk0309RLkvUgnFKpvLddrg9TGp3n80yUbRsp2AogyrlBU+gP5ggHF
# i7NjGEpj5bH+FDsMw9PygLg8JelgsvBVudw1SgUt625nY7w1vrwk+cDd58TvAyJQ
# FAW1zJ+0ySgB9lu2vwg0NKetOyL7dxe3KoRLaztUcqXoYW5CkI+Mv3m8HOeqlhyf
# FTYxPB5YXyQJPKQJYh8zC9b90JXLT7raM7mQ94ygDuFmlaiZ+QSUR3XVupdEngrm
# ZgUB5jX13M+Pl2Vv7PPFU3xlo3Uhj1wtupNC81epoxGhJ0tRuLdEajD/dCZ0xIni
# esRXCKSC4HCL3BMnSwVXtIoj/QFymFYwD5+sAZuvRSgkKyD1rDA7MPcEI2i/Bh5O
# MAo9App4sR0Gp049oSkXNhvRi/au7QG6NJBTSBbNBGJG8Qp+5QThKoQUk8mj0ugr
# 4yWRsA9JTbmqVw7u9suB5OKYBMUN4hL/yI+aFVsE/KJInvnxSzXJ1YHka45ADYMK
# AMl+fLdIqm3nx6rIN0RkoDAbvTAAXGehUCsIod049A1T3IJyUJXt3OsTd3WabhIB
# XICYfxMg10naaWcyUePgW3+VwP0XLKu4O1+8ZeGyaDSi33GnzmmyYacX3BTqMDCC
# B3owggVioAMCAQICCmEOkNIAAAAAAAMwDQYJKoZIhvcNAQELBQAwgYgxCzAJBgNV
# BAYTAlVTMRMwEQYDVQQIEwpXYXNoaW5ndG9uMRAwDgYDVQQHEwdSZWRtb25kMR4w
# HAYDVQQKExVNaWNyb3NvZnQgQ29ycG9yYXRpb24xMjAwBgNVBAMTKU1pY3Jvc29m
# dCBSb290IENlcnRpZmljYXRlIEF1dGhvcml0eSAyMDExMB4XDTExMDcwODIwNTkw
# OVoXDTI2MDcwODIxMDkwOVowfjELMAkGA1UEBhMCVVMxEzARBgNVBAgTCldhc2hp
# bmd0b24xEDAOBgNVBAcTB1JlZG1vbmQxHjAcBgNVBAoTFU1pY3Jvc29mdCBDb3Jw
# b3JhdGlvbjEoMCYGA1UEAxMfTWljcm9zb2Z0IENvZGUgU2lnbmluZyBQQ0EgMjAx
# MTCCAiIwDQYJKoZIhvcNAQEBBQADggIPADCCAgoCggIBAKvw+nIQHC6t2G6qghBN
# NLrytlghn0IbKmvpWlCquAY4GgRJun/DDB7dN2vGEtgL8DjCmQawyDnVARQxQtOJ
# DXlkh36UYCRsr55JnOloXtLfm1OyCizDr9mpK656Ca/XllnKYBoF6WZ26DJSJhIv
# 56sIUM+zRLdd2MQuA3WraPPLbfM6XKEW9Ea64DhkrG5kNXimoGMPLdNAk/jj3gcN
# 1Vx5pUkp5w2+oBN3vpQ97/vjK1oQH01WKKJ6cuASOrdJXtjt7UORg9l7snuGG9k+
# sYxd6IlPhBryoS9Z5JA7La4zWMW3Pv4y07MDPbGyr5I4ftKdgCz1TlaRITUlwzlu
# ZH9TupwPrRkjhMv0ugOGjfdf8NBSv4yUh7zAIXQlXxgotswnKDglmDlKNs98sZKu
# HCOnqWbsYR9q4ShJnV+I4iVd0yFLPlLEtVc/JAPw0XpbL9Uj43BdD1FGd7P4AOG8
# rAKCX9vAFbO9G9RVS+c5oQ/pI0m8GLhEfEXkwcNyeuBy5yTfv0aZxe/CHFfbg43s
# TUkwp6uO3+xbn6/83bBm4sGXgXvt1u1L50kppxMopqd9Z4DmimJ4X7IvhNdXnFy/
# dygo8e1twyiPLI9AN0/B4YVEicQJTMXUpUMvdJX3bvh4IFgsE11glZo+TzOE2rCI
# F96eTvSWsLxGoGyY0uDWiIwLAgMBAAGjggHtMIIB6TAQBgkrBgEEAYI3FQEEAwIB
# ADAdBgNVHQ4EFgQUSG5k5VAF04KqFzc3IrVtqMp1ApUwGQYJKwYBBAGCNxQCBAwe
# CgBTAHUAYgBDAEEwCwYDVR0PBAQDAgGGMA8GA1UdEwEB/wQFMAMBAf8wHwYDVR0j
# BBgwFoAUci06AjGQQ7kUBU7h6qfHMdEjiTQwWgYDVR0fBFMwUTBPoE2gS4ZJaHR0
# cDovL2NybC5taWNyb3NvZnQuY29tL3BraS9jcmwvcHJvZHVjdHMvTWljUm9vQ2Vy
# QXV0MjAxMV8yMDExXzAzXzIyLmNybDBeBggrBgEFBQcBAQRSMFAwTgYIKwYBBQUH
# MAKGQmh0dHA6Ly93d3cubWljcm9zb2Z0LmNvbS9wa2kvY2VydHMvTWljUm9vQ2Vy
# QXV0MjAxMV8yMDExXzAzXzIyLmNydDCBnwYDVR0gBIGXMIGUMIGRBgkrBgEEAYI3
# LgMwgYMwPwYIKwYBBQUHAgEWM2h0dHA6Ly93d3cubWljcm9zb2Z0LmNvbS9wa2lv
# cHMvZG9jcy9wcmltYXJ5Y3BzLmh0bTBABggrBgEFBQcCAjA0HjIgHQBMAGUAZwBh
# AGwAXwBwAG8AbABpAGMAeQBfAHMAdABhAHQAZQBtAGUAbgB0AC4gHTANBgkqhkiG
# 9w0BAQsFAAOCAgEAZ/KGpZjgVHkaLtPYdGcimwuWEeFjkplCln3SeQyQwWVfLiw+
# +MNy0W2D/r4/6ArKO79HqaPzadtjvyI1pZddZYSQfYtGUFXYDJJ80hpLHPM8QotS
# 0LD9a+M+By4pm+Y9G6XUtR13lDni6WTJRD14eiPzE32mkHSDjfTLJgJGKsKKELuk
# qQUMm+1o+mgulaAqPyprWEljHwlpblqYluSD9MCP80Yr3vw70L01724lruWvJ+3Q
# 3fMOr5kol5hNDj0L8giJ1h/DMhji8MUtzluetEk5CsYKwsatruWy2dsViFFFWDgy
# cScaf7H0J/jeLDogaZiyWYlobm+nt3TDQAUGpgEqKD6CPxNNZgvAs0314Y9/HG8V
# fUWnduVAKmWjw11SYobDHWM2l4bf2vP48hahmifhzaWX0O5dY0HjWwechz4GdwbR
# BrF1HxS+YWG18NzGGwS+30HHDiju3mUv7Jf2oVyW2ADWoUa9WfOXpQlLSBCZgB/Q
# ACnFsZulP0V3HjXG0qKin3p6IvpIlR+r+0cjgPWe+L9rt0uX4ut1eBrs6jeZeRhL
# /9azI2h15q/6/IvrC4DqaTuv/DDtBEyO3991bWORPdGdVk5Pv4BXIqF4ETIheu9B
# CrE/+6jMpF3BoYibV3FWTkhFwELJm3ZbCoBIa/15n8G9bW1qyVJzEw16UM0xghXM
# MIIVyAIBATCBlTB+MQswCQYDVQQGEwJVUzETMBEGA1UECBMKV2FzaGluZ3RvbjEQ
# MA4GA1UEBxMHUmVkbW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0IENvcnBvcmF0aW9u
# MSgwJgYDVQQDEx9NaWNyb3NvZnQgQ29kZSBTaWduaW5nIFBDQSAyMDExAhMzAAAA
# ZEeElIbbQRk4AAAAAABkMA0GCWCGSAFlAwQCAQUAoIG6MBkGCSqGSIb3DQEJAzEM
# BgorBgEEAYI3AgEEMBwGCisGAQQBgjcCAQsxDjAMBgorBgEEAYI3AgEVMC8GCSqG
# SIb3DQEJBDEiBCAbbGGFMH7kkeGBhf2ppzxR1bcnHkcUBhl+D8fYw07mojBOBgor
# BgEEAYI3AgEMMUAwPqAkgCIATQBpAGMAcgBvAHMAbwBmAHQAIABBAFMAUAAuAE4A
# RQBUoRaAFGh0dHA6Ly93d3cuYXNwLm5ldC8gMA0GCSqGSIb3DQEBAQUABIIBAFj6
# w6AesOpqxlBgEScBUOivPR6Y3lTpASMUOhtETUaLotABSIvXr3BJAubG8wjDZlgB
# /HjghPGbz7oN+3ge1k2C44qS01IetDMyvYCxYlc7eVacd6EYYi6R96L1ihtMeFTA
# lmq9HkacOQyXJxL0hYCqETupOwmt8RpqwghynGSdsBRUk3F/BLwX5t4tsv9iT/8V
# O1RZGLPrYjRDe9DTrEucEIBAaVBu7cANlkHO5j/fKXZ+UeB9bHmimCJCvUytmZ1H
# BFMZR4lRKIkDGVlYT3+RCpTN095up0Z2xBpMkpZX/voXsUiQza6MGKLyFak9Vpx9
# EPPZKD1LqTexDTNmPayhghNKMIITRgYKKwYBBAGCNwMDATGCEzYwghMyBgkqhkiG
# 9w0BBwKgghMjMIITHwIBAzEPMA0GCWCGSAFlAwQCAQUAMIIBPQYLKoZIhvcNAQkQ
# AQSgggEsBIIBKDCCASQCAQEGCisGAQQBhFkKAwEwMTANBglghkgBZQMEAgEFAAQg
# kMBHYt1VZtQBfBCKj4S+aBz8p4NoFyha5rPC3CP5o2gCBlf+qcNhVBgTMjAxNjEw
# MTkyMjU3MzIuMzczWjAHAgEBgAIB9KCBuaSBtjCBszELMAkGA1UEBhMCVVMxEzAR
# BgNVBAgTCldhc2hpbmd0b24xEDAOBgNVBAcTB1JlZG1vbmQxHjAcBgNVBAoTFU1p
# Y3Jvc29mdCBDb3Jwb3JhdGlvbjENMAsGA1UECxMETU9QUjEnMCUGA1UECxMebkNp
# cGhlciBEU0UgRVNOOkMwRjQtMzA4Ni1ERUY4MSUwIwYDVQQDExxNaWNyb3NvZnQg
# VGltZS1TdGFtcCBTZXJ2aWNloIIOzTCCBnEwggRZoAMCAQICCmEJgSoAAAAAAAIw
# DQYJKoZIhvcNAQELBQAwgYgxCzAJBgNVBAYTAlVTMRMwEQYDVQQIEwpXYXNoaW5n
# dG9uMRAwDgYDVQQHEwdSZWRtb25kMR4wHAYDVQQKExVNaWNyb3NvZnQgQ29ycG9y
# YXRpb24xMjAwBgNVBAMTKU1pY3Jvc29mdCBSb290IENlcnRpZmljYXRlIEF1dGhv
# cml0eSAyMDEwMB4XDTEwMDcwMTIxMzY1NVoXDTI1MDcwMTIxNDY1NVowfDELMAkG
# A1UEBhMCVVMxEzARBgNVBAgTCldhc2hpbmd0b24xEDAOBgNVBAcTB1JlZG1vbmQx
# HjAcBgNVBAoTFU1pY3Jvc29mdCBDb3Jwb3JhdGlvbjEmMCQGA1UEAxMdTWljcm9z
# b2Z0IFRpbWUtU3RhbXAgUENBIDIwMTAwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAw
# ggEKAoIBAQCpHQ28dxGKOiDs/BOX9fp/aZRrdFQQ1aUKAIKF++18aEssX8XD5WHC
# drc+Zitb8BVTJwQxH0EbGpUdzgkTjnxhMFmxMEQP8WCIhFRDDNdNuDgIs0Ldk6zW
# czBXJoKjRQ3Q6vVHgc2/JGAyWGBG8lhHhjKEHnRhZ5FfgVSxz5NMksHEpl3RYRNu
# KMYa+YaAu99h/EbBJx0kZxJyGiGKr0tkiVBisV39dx898Fd1rL2KQk1AUdEPnAY+
# Z3/1ZsADlkR+79BL/W7lmsqxqPJ6Kgox8NpOBpG2iAg16HgcsOmZzTznL0S6p/Tc
# ZL2kAcEgCZN4zfy8wMlEXV4WnAEFTyJNAgMBAAGjggHmMIIB4jAQBgkrBgEEAYI3
# FQEEAwIBADAdBgNVHQ4EFgQU1WM6XIoxkPNDe3xGG8UzaFqFbVUwGQYJKwYBBAGC
# NxQCBAweCgBTAHUAYgBDAEEwCwYDVR0PBAQDAgGGMA8GA1UdEwEB/wQFMAMBAf8w
# HwYDVR0jBBgwFoAU1fZWy4/oolxiaNE9lJBb186aGMQwVgYDVR0fBE8wTTBLoEmg
# R4ZFaHR0cDovL2NybC5taWNyb3NvZnQuY29tL3BraS9jcmwvcHJvZHVjdHMvTWlj
# Um9vQ2VyQXV0XzIwMTAtMDYtMjMuY3JsMFoGCCsGAQUFBwEBBE4wTDBKBggrBgEF
# BQcwAoY+aHR0cDovL3d3dy5taWNyb3NvZnQuY29tL3BraS9jZXJ0cy9NaWNSb29D
# ZXJBdXRfMjAxMC0wNi0yMy5jcnQwgaAGA1UdIAEB/wSBlTCBkjCBjwYJKwYBBAGC
# Ny4DMIGBMD0GCCsGAQUFBwIBFjFodHRwOi8vd3d3Lm1pY3Jvc29mdC5jb20vUEtJ
# L2RvY3MvQ1BTL2RlZmF1bHQuaHRtMEAGCCsGAQUFBwICMDQeMiAdAEwAZQBnAGEA
# bABfAFAAbwBsAGkAYwB5AF8AUwB0AGEAdABlAG0AZQBuAHQALiAdMA0GCSqGSIb3
# DQEBCwUAA4ICAQAH5ohRDeLG4Jg/gXEDPZ2joSFvs+umzPUxvs8F4qn++ldtGTCz
# wsVmyWrf9efweL3HqJ4l4/m87WtUVwgrUYJEEvu5U4zM9GASinbMQEBBm9xcF/9c
# +V4XNZgkVkt070IQyK+/f8Z/8jd9Wj8c8pl5SpFSAK84Dxf1L3mBZdmptWvkx872
# ynoAb0swRCQiPM/tA6WWj1kpvLb9BOFwnzJKJ/1Vry/+tuWOM7tiX5rbV0Dp8c6Z
# ZpCM/2pif93FSguRJuI57BlKcWOdeyFtw5yjojz6f32WapB4pm3S4Zz5Hfw42JT0
# xqUKloakvZ4argRCg7i1gJsiOCC1JeVk7Pf0v35jWSUPei45V3aicaoGig+JFrph
# pxHLmtgOR5qAxdDNp9DvfYPw4TtxCd9ddJgiCGHasFAeb73x4QDf5zEHpJM692VH
# eOj4qEir995yfmFrb3epgcunCaw5u+zGy9iCtHLNHfS4hQEegPsbiSpUObJb2sgN
# VZl6h3M7COaYLeqN4DMuEin1wC9UJyH3yKxO2ii4sanblrKnQqLJzxlBTeCG+Sqa
# oxFmMNO7dDJL32N79ZmKLxvHIa9Zta7cRDyXUHHXodLFVeNp3lfB0d4wwP3M5k37
# Db9dT+mdHhk4L7zPWAUu7w2gUDXa7wknHNWzfjUeCLraNtvTX4/edIhJEjCCBNow
# ggPCoAMCAQICEzMAAACj7x8iIIFj3KUAAAAAAKMwDQYJKoZIhvcNAQELBQAwfDEL
# MAkGA1UEBhMCVVMxEzARBgNVBAgTCldhc2hpbmd0b24xEDAOBgNVBAcTB1JlZG1v
# bmQxHjAcBgNVBAoTFU1pY3Jvc29mdCBDb3Jwb3JhdGlvbjEmMCQGA1UEAxMdTWlj
# cm9zb2Z0IFRpbWUtU3RhbXAgUENBIDIwMTAwHhcNMTYwOTA3MTc1NjQ5WhcNMTgw
# OTA3MTc1NjQ5WjCBszELMAkGA1UEBhMCVVMxEzARBgNVBAgTCldhc2hpbmd0b24x
# EDAOBgNVBAcTB1JlZG1vbmQxHjAcBgNVBAoTFU1pY3Jvc29mdCBDb3Jwb3JhdGlv
# bjENMAsGA1UECxMETU9QUjEnMCUGA1UECxMebkNpcGhlciBEU0UgRVNOOkMwRjQt
# MzA4Ni1ERUY4MSUwIwYDVQQDExxNaWNyb3NvZnQgVGltZS1TdGFtcCBTZXJ2aWNl
# MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAqdEel8cTafg4OxUX5kgO
# +V+CrFSdMBqtEF3Q8gX3P2iGN1rQAFPmnG0caPIpx9b/MZhSTRG69cFkhjo5CSdW
# SV6foSEZKRvMWhbj830BVRcs6eGslvvHma8ocAB1IvoucpRUX7rRxawy1OXWHnnw
# gaMKvmO+eGln4o+F0cm+yH+Qi+S4fpiub74qZAgaSLc5Ichq9CRLYBDUcoByCDjp
# bvk7U+1Z2yTUTWHIW9NpYwAyvcyxUT3rQLh/uL67ch3BOGzeCY5uLZk6bEUI3rNP
# W21tgJHZI5tImUwe5RF/sxeedpG94iYWHxEAHDrfmegs/+x1LFgpcKcXLSjuj7Sj
# XwIDAQABo4IBGzCCARcwHQYDVR0OBBYEFPqFumZm6EaZ2nCfuiElQNvg6LFwMB8G
# A1UdIwQYMBaAFNVjOlyKMZDzQ3t8RhvFM2hahW1VMFYGA1UdHwRPME0wS6BJoEeG
# RWh0dHA6Ly9jcmwubWljcm9zb2Z0LmNvbS9wa2kvY3JsL3Byb2R1Y3RzL01pY1Rp
# bVN0YVBDQV8yMDEwLTA3LTAxLmNybDBaBggrBgEFBQcBAQROMEwwSgYIKwYBBQUH
# MAKGPmh0dHA6Ly93d3cubWljcm9zb2Z0LmNvbS9wa2kvY2VydHMvTWljVGltU3Rh
# UENBXzIwMTAtMDctMDEuY3J0MAwGA1UdEwEB/wQCMAAwEwYDVR0lBAwwCgYIKwYB
# BQUHAwgwDQYJKoZIhvcNAQELBQADggEBAB3RRbpbtL+K5oaNRc41iCYSRrAzg2ph
# McgWc/jmJHpqwcAVNzyNxykNSMt0l6Wyh+EGeNVDjFM68OJRDni20/wcjSXlUxoV
# 2T56vMe7wU5mWFEYD2UlYSGhvuaRw2CO+Qm0PojCpnKBOxzyEBzVBa6IXTRVUqhD
# hozwDVS+S+RL7heVtpu8AmsWzbItbPWr3zXhBoO0WUHnHgHzaE332N4kLEZLQsCN
# F3NEUCuN3nbNf3Rd3+ZkzDK4nsDPZVIRCAZ6l7aDZaNi2MODujmOR7hTqsNmGhy9
# SU703NQHrNK40WT54HfJ7HaAxKsXK+sjg7WWifHYS5aS3W+pwjvW85yhggN2MIIC
# XgIBATCB46GBuaSBtjCBszELMAkGA1UEBhMCVVMxEzARBgNVBAgTCldhc2hpbmd0
# b24xEDAOBgNVBAcTB1JlZG1vbmQxHjAcBgNVBAoTFU1pY3Jvc29mdCBDb3Jwb3Jh
# dGlvbjENMAsGA1UECxMETU9QUjEnMCUGA1UECxMebkNpcGhlciBEU0UgRVNOOkMw
# RjQtMzA4Ni1ERUY4MSUwIwYDVQQDExxNaWNyb3NvZnQgVGltZS1TdGFtcCBTZXJ2
# aWNloiUKAQEwCQYFKw4DAhoFAAMVADXko/tOP/8mDXH1bV4Se5GWOKaNoIHCMIG/
# pIG8MIG5MQswCQYDVQQGEwJVUzETMBEGA1UECBMKV2FzaGluZ3RvbjEQMA4GA1UE
# BxMHUmVkbW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMQ0wCwYD
# VQQLEwRNT1BSMScwJQYDVQQLEx5uQ2lwaGVyIE5UUyBFU046NTdGNi1DMUUwLTU1
# NEMxKzApBgNVBAMTIk1pY3Jvc29mdCBUaW1lIFNvdXJjZSBNYXN0ZXIgQ2xvY2sw
# DQYJKoZIhvcNAQEFBQACBQDbsiTIMCIYDzIwMTYxMDE5MTY1ODQ4WhgPMjAxNjEw
# MjAxNjU4NDhaMHQwOgYKKwYBBAGEWQoEATEsMCowCgIFANuyJMgCAQAwBwIBAAIC
# GIEwBwIBAAICGSwwCgIFANuzdkgCAQAwNgYKKwYBBAGEWQoEAjEoMCYwDAYKKwYB
# BAGEWQoDAaAKMAgCAQACAxbjYKEKMAgCAQACAwehIDANBgkqhkiG9w0BAQUFAAOC
# AQEArBWPmgO5+bhHZmDkUuAPSpIo4w904YP/i/JXwF0h5TQH8ygiJ3VhmrCi6tcH
# yLdLDjjz9CbTANi2Kf0vFgkGsBKMmZDbKx/jPZfuJkB09V9xo/wNR7+vr06O2FUv
# JSqk/AIhO4WTU/I55I3trRIVNPAozkKI32xvDO7ndg+jPNvzGMMC70qEhXOe9hr7
# C/Y2hlmd1JQCgP/L9It/B30GQNVbf56TeX4Sx2E3OfsR1ty2W2jlg4lUfzOr3AYK
# WgnR8IWE6bT8qs4F8XcHCX9MnoedhgKoH9EYUmyWHTHJxOkiaFn6+XREVls6A1Kt
# EHOWxlfCruFS18vHUmOMbY7HvDGCAvUwggLxAgEBMIGTMHwxCzAJBgNVBAYTAlVT
# MRMwEQYDVQQIEwpXYXNoaW5ndG9uMRAwDgYDVQQHEwdSZWRtb25kMR4wHAYDVQQK
# ExVNaWNyb3NvZnQgQ29ycG9yYXRpb24xJjAkBgNVBAMTHU1pY3Jvc29mdCBUaW1l
# LVN0YW1wIFBDQSAyMDEwAhMzAAAAo+8fIiCBY9ylAAAAAACjMA0GCWCGSAFlAwQC
# AQUAoIIBMjAaBgkqhkiG9w0BCQMxDQYLKoZIhvcNAQkQAQQwLwYJKoZIhvcNAQkE
# MSIEILFvnQ/tSYWn2nhHkWkarC/S4G9o2Bsn6HdImXij28ToMIHiBgsqhkiG9w0B
# CRACDDGB0jCBzzCBzDCBsQQUNeSj+04//yYNcfVtXhJ7kZY4po0wgZgwgYCkfjB8
# MQswCQYDVQQGEwJVUzETMBEGA1UECBMKV2FzaGluZ3RvbjEQMA4GA1UEBxMHUmVk
# bW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMSYwJAYDVQQDEx1N
# aWNyb3NvZnQgVGltZS1TdGFtcCBQQ0EgMjAxMAITMwAAAKPvHyIggWPcpQAAAAAA
# ozAWBBRMWugm3rkKRaEADMqJPYm3CwfbNjANBgkqhkiG9w0BAQsFAASCAQBqU9rr
# mn1PSl/9zcuENI/FAVf0JZTJ0n5ivc0m60L2KjgfDmwZmkpsvdAv8rLxSST3JXXp
# Jj9Z31O98b7GTXlDXvvLuqz/tDhu1ZHEkPxrPsS6OVqGm3S1fOkNcep3jtaovrHp
# rVvldghKT/NNr7e3YqPSNGRsJ4cXMo01Twb6PPkjxZigruzy7X9KiA9/EQlY83Hm
# 35fc7KKXeFtsYqGUT9kHARoBs1oAt3XocX/flEyRBFwXU66Uzwq57/J+qFEPRX+M
# HWlIFXGBUH0EGdkOBS9EZrO4S0B3ywaZfJ5I3xOE+RwpowM7SG05FRNlh48DSSS5
# ymJ0mNX8W6y8/gVj
# SIG # End signature block
