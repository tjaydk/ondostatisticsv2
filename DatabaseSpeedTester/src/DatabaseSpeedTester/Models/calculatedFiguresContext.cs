using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DatabaseSpeedTester.Models
{
    public partial class calculatedFiguresContext : DbContext
    {
        public virtual DbSet<TblClubData> TblClubData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Server=tcp:ondodeveloper.database.windows.net,1433;Initial Catalog=calculatedFigures;Persist Security Info=False;User ID=ondodeveloper_1;Password=0nd0devel0per!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblClubData>(entity =>
            {
                entity.HasKey(e => e.OndoId)
                    .HasName("PK__tmp_ms_x__E5D18D3278FEA0E3");

                entity.ToTable("tblClubData");

                entity.Property(e => e.OndoId)
                    .HasColumnName("ondoId")
                    .ValueGeneratedNever();

                entity.Property(e => e.ActiveUsers).HasColumnName("activeUsers");

                entity.Property(e => e.AppUsers).HasColumnName("appUsers");

                entity.Property(e => e.DaysLeftInQuarter).HasColumnName("daysLeftInQuarter");

                entity.Property(e => e.EightyPercentEstimatedPrognose).HasColumnName("eightyPercentEstimatedPrognose");

                entity.Property(e => e.EightyPercentPrognose).HasColumnName("eightyPercentPrognose");

                entity.Property(e => e.EstimatedPrognose).HasColumnName("estimatedPrognose");

                entity.Property(e => e.InactiveUsers).HasColumnName("inactiveUsers");

                entity.Property(e => e.NoAppUsers).HasColumnName("noAppUsers");

                entity.Property(e => e.PercentActiveUsers).HasColumnName("percentActiveUsers");

                entity.Property(e => e.PercentAppUsers).HasColumnName("percentAppUsers");

                entity.Property(e => e.ProfilePicture).HasColumnType("varchar(max)");

                entity.Property(e => e.Prognose).HasColumnName("prognose");

                entity.Property(e => e.Q1)
                    .HasColumnName("q1")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Q1label)
                    .HasColumnName("q1label")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Q2)
                    .HasColumnName("q2")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Q2label)
                    .HasColumnName("q2label")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Q3)
                    .HasColumnName("q3")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Q3label)
                    .HasColumnName("q3label")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Q4)
                    .HasColumnName("q4")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Q4label)
                    .HasColumnName("q4label")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Target).HasColumnName("target");

                entity.Property(e => e.Title).HasColumnType("varchar(50)");
            });
        }
    }
}