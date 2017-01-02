$(document).ready(function () {
    //script for redirecting to https
    if (window.location.hostname != "localhost" && window.location.protocol != "https") {
        window.location.href = "https:" + window.location.href.substring(window.location.protocol.length, window.location.href.length);
    };

    //script for checking browser version
    navigator.sayswho = (function () {
        var ua = navigator.userAgent, tem,
        M = ua.match(/(opera|chrome|safari|firefox|msie|trident(?=\/))\/?\s*(\d+)/i) || [];
        if (/trident/i.test(M[1])) {
            tem = /\brv[ :]+(\d+)/g.exec(ua) || [];
            return 'IE ' + (tem[1] || '');
        }
        if (M[1] === 'Chrome') {
            tem = ua.match(/\b(OPR|Edge)\/(\d+)/);
            if (tem != null) return tem.slice(1).join(' ').replace('OPR', 'Opera');
        }
        M = M[2] ? [M[1], M[2]] : [navigator.appName, navigator.appVersion, '-?'];
        if ((tem = ua.match(/version\/(\d+)/i)) != null) M.splice(1, 1, tem[1]);
        return M.join(' ');
    })();

    if ((navigator.sayswho + "").substring(0, 2) == "IE") {
        if (parseInt((navigator.sayswho + "").substring(3, 5)) < 9) {
            alert("Unsupported browser \n" + "Please upgrade to newest version of IE");
        }
    };
});


var app = angular.module("ondoApp", [
    'ngRoute',
    'navbarSideDirective',
    'navbarSideTradeDirective',
    'loadingDirective'
])
app.config(["$routeProvider", function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: 'views/login.html',
            controller: 'loginCtrl',
            controllerAs: "ctrl"
        })
        .when('/choose', {
            templateUrl: 'views/choose.html',
            controller: 'chooseOndoCtrl',
            controllerAs: "ctrl"
        })
        .when('/tradeunion', {
            templateUrl: 'views/tradeUnion.html',
            controller: 'tradeUnionCtrl',
            controllerAs: "ctrl"
        })
        .when('/tradeunion/shops', {
            templateUrl: 'views/tradeUnion/shops.html',
            controller: 'tradeUnionShopsCtrl',
            controllerAs: "ctrl"
        })
        .when('/tradeunion/clubs', {
            templateUrl: 'views/tradeUnion/clubs.html',
            controller: 'tradeUnionSClubsCtrl',
            controllerAs: "ctrl"
        })
        .when('/tradeunion/subscriptions', {
            templateUrl: 'views/tradeUnion/subscriptions.html',
            controller: 'tradeUnionSubscriptionsCtrl',
            controllerAs: "ctrl"
        })
        .when('/tradeunion/transactions', {
            templateUrl: 'views/tradeUnion/transactions.html',
            controller: 'tradeUnionTransactionsCtrl',
            controllerAs: "ctrl"
        })
        .when('/tradeunion/history', {
            templateUrl: 'views/tradeUnion/history.html',
            controller: 'tradeUnionHistoryCtrl',
            controllerAs: "ctrl"
        })
        .when('/tradeunion/userhistory', {
            templateUrl: 'views/tradeUnion/userhistory.html',
            controller: 'tradeUnionUserHistoryCtrl',
            controllerAs: "ctrl"
        })
        .when('/club', {
            templateUrl: 'views/club.html',
            controller: 'clubCtrl',
            controllerAs: "ctrl"
        })
        .when('/shop', {
            templateUrl: 'views/shop.html',
            controller: 'shopCtrl',
            controllerAs: "ctrl"
        })
        .when('/shop/subscriptions', {
            templateUrl: 'views/shop/subscriptions.html',
            controller: 'shopSubscriptionsCtrl',
            controllerAs: "ctrl"
        })
        .when('/shop/transactions', {
            templateUrl: 'views/shop/transactions.html',
            controller: 'shopTransactionsCtrl',
            controllerAs: "ctrl"
        })
        .when('/shop/history', {
            templateUrl: 'views/shop/history.html',
            controller: 'shopHistoryCtrl',
            controllerAs: "ctrl"
        })
    .otherwise({ redirectTo: '/' });
}])
app.controller('loginCtrl', ["$scope", "$http", "connectionUrlFactory", "dateFactory", "sessionStoreFactory", "fetchDataFactory", function ($scope, $http, connectionUrlFactory, dateFactory, sessionStoreFactory, fetchDataFactory) {
    var self = this;

    //initial username and password
    self.username = "";
    self.password = "";

    //end loading gif and display content
    $(".loginContent").css("-webkit-filter", "blur(0px)");
    $(".loginContent").css("filter", "blur(0px)");
    $("#loading").css("display", "none");

    //error text element
    var errorElement = document.getElementById("errorText");

    //login function
    self.login = function () {
        //start loading gif
        $(".loginContent").css("-webkit-filter", "blur(10px)");
        $(".loginContent").css("filter", "blur(10px)");
        $("#loading").css("display", "block");

        //try to login
        $.ajax({
            url: connectionUrlFactory.getConnectionUrl + "/api/login",
            type: "POST",
            contentType: 'application/json',
            data: JSON.stringify({
                Login: self.username,
                Password: self.password,
                IsTest: false
            }),
            dataType: "json",
            success: function (data) {

                //remove error message on login success
                errorElement.innerHTML = "";

                //$scope.$apply fires the function to check for dirty objects and update with data binding
                $scope.$apply(function () {
                    //reset username and password
                    self.username = "";
                    self.password = "";
                });

                //store a user in a session object
                sessionStoreFactory.saveJSON("userData", data);

                //create an user object in storage
                var userDate = dateFactory.getDkTime();
                var userDateStr = userDate.getUTCDate() + "/" + (userDate.getUTCMonth() + 1) + "/" + userDate.getUTCFullYear();

                console.log(data);
                console.log(data.length);

                //if user only has one ondo then fetch data and redirect to view
                if (data.length == 1) {

                    //get the type of the ondo
                    var type = data[0].type;

                    //fetch data and redirect
                    if (type == 1) { fetchDataFactory.tradeUnionData.getInitialData(data[0].ondoId, true, false); }
                    else if (type == 2) { fetchDataFactory.shopData.getInitialData(data[0].ondoId, true, false); }
                    else if (type == 3) { fetchDataFactory.clubData.getInitialData(data[0].ondoId, true, false); }

                } else {
                    for (var i = 0; i < data.length; i++) {
                        var redirect = false;
                        if (i == data.length - 1) { redirect = true }
                        var type = data[i].type;
                        //fetch data and redirect
                        if (type == 1) { fetchDataFactory.tradeUnionData.getInitialData(data[i].ondoId, false, redirect); }
                        else if (type == 2) { fetchDataFactory.shopData.getInitialData(data[i].ondoId, false, redirect); }
                        else if (type == 3) { fetchDataFactory.clubData.getInitialData(data[i].ondoId, false, redirect); }
                    }
                }

            },
            error: function (err, status, msg) {
                //end loading gif and display content
                $(".loginContent").css("-webkit-filter", "blur(0px)");
                $(".loginContent").css("filter", "blur(0px)");
                $("#loading").css("display", "none");

                if (err.status == 500) {
                    //print error message in html when login unsuccessful
                    errorElement.innerHTML = "<h3>Fejl, prøv igen senere.</h3>";

                    //$scope.$apply fires the function to check for dirty objects and update with data binding
                    $scope.$apply(function () {
                        self.username = "";
                        self.password = "";
                    });
                } else if (err.status == 403) {
                    //print error message in html when login unsuccessful
                    errorElement.innerHTML = "<h3>Uautoriseret adgang.</h3>";

                    //$scope.$apply fires the function to check for dirty objects and update with data binding
                    $scope.$apply(function () {
                        self.username = "";
                        self.password = "";
                    });
                } else {
                    //print error message in html when login unsuccessful
                    errorElement.innerHTML = "<h3>Forkert brugernavn og/eller password.</h3>";

                    //$scope.$apply fires the function to check for dirty objects and update with data binding
                    $scope.$apply(function () {
                        self.username = "";
                        self.password = "";
                    });
                }
            }
        });
    };
}])
app.controller('chooseOndoCtrl', ["$scope", "$http", "connectionUrlFactory", "dateFactory", "sessionStoreFactory", "fetchDataFactory", function ($scope, $http, connectionUrlFactory, dateFactory, sessionStoreFactory, fetchDataFactory) {
    var self = this;

    //end loading gif and display content
    $(".chooseOndoView").css("-webkit-filter", "blur(0px)");
    $(".chooseOndoView").css("filter", "blur(0px)");
    $("#loading").css("display", "none");

    //arrays for the different ondo types
    self.tradeUnions = [];
    self.shops = [];
    self.clubs = [];

    //retrieve userdata
    self.ondoIds = sessionStoreFactory.loadJSON("userData");

    //function to populate the ondo arrays used to display the ondo icons
    var populateOndoArrays = function (type, data) {
        //fecth ondo data and store in array based on type
        if (type == 1) {
            //push data to tradeunion array
            self.tradeUnions.push(data);
            //insert text if elements are present
            document.getElementById("tradeUnionList").innerHTML = "<h4 class='chooseHeader'>Vælg by</h4>";
        } else if (type == 2) {
            //push data to shop array
            self.shops.push(data);
            //insert text if elements are present
            document.getElementById("shopList").innerHTML = "<h4 class='chooseHeader'>Vælg butik</h4>";
        } else if (type == 3) {
            //push data to club array
            self.clubs.push(data);
            //insert text if elements are present
            document.getElementById("clubList").innerHTML = "<h4 class='chooseHeader'>Vælg klub</h4>";
        };
    }

    //if userdata is null then redirect to login page
    if (self.ondoIds == null) {
        window.location.href = "/";
    } else {
        for (var i = 0; i < self.ondoIds.length; i++) {
            var type = self.ondoIds[i].type;
            var data = sessionStoreFactory.loadJSON(self.ondoIds[i].ondoId);
            populateOndoArrays(type, data);
        }
    }

    //go to view function triggered by click
    self.goToView = function (event) {
        //start loading gif
        $(".chooseOndoView").css("-webkit-filter", "blur(10px)");
        $(".chooseOndoView").css("filter", "blur(10px)");
        $("#loading").css("display", "block");

        //get ondo id from click event
        var ondoId = event.ondo.ondoId;

        //set type to 2 by default
        var type = 2;

        //set the current ondo
        sessionStoreFactory.save("currentOndo", ondoId);

        //get the type of the ondo
        for (var i = 0; i < self.ondoIds.length; i++) {
            if (self.ondoIds[i].ondoId == ondoId) {
                type = self.ondoIds[i].type;
            }
        };

        //redirect to dashboard
        if (type == 1) { window.location.href = "#/tradeunion"; }
        if (type == 2) { window.location.href = "#/shop"; }
        if (type == 3) { window.location.href = "#/club"; }
    };

    //function to get the type from ondoid
    var getOndoType = function (ondoId) {

        //get the type of the ondo
        var type;

        //run through array to find the type from ondoid
        for (var j = 0; j < self.ondoIds.length; j++) {
            if (self.ondoIds[j].ondoId == ondoId) {
                type = self.ondoIds[j].type;
            }
        };

        //return the type
        return type;
    }
}])
app.controller("clubCtrl", ["$scope", "$http", "connectionUrlFactory", "dateFactory", "sessionStoreFactory", function ($scope, $http, connectionUrlFactory, dateFactory, sessionStoreFactory) {
    var self = this;

    //end loading gif and display content
    $(".mainContent").css("-webkit-filter", "blur(0px)");
    $(".mainContent").css("filter", "blur(0px)");
    $("#loading").css("display", "none");


    //if userdata is null then redirect to login page
    if (sessionStorage.getItem("userData") == null) {
        window.location.href = "/";
    }

    //get initial data
    var currentOndo                         = sessionStoreFactory.load("currentOndo");
    var clubData                            = sessionStoreFactory.loadJSON("Data:" + currentOndo)


    self.user                               = clubData.title;
    self.ondoProfilePicture                 = clubData.profilePicture;
    self.prognoseThisQuarter                = clubData.estimatedPrognose;
    self.prognoseThisQuarterFigures         = clubData.prognose;
    self.pronoseThisQuarterBarText          = "Indlæser...";
    self.prognoseEightyThisQuarter          = clubData.eightyPercentEstimatedPrognose;
    self.prognoseEightyThisQuarterFigures   = clubData.eightyPercentPrognose;
    self.prognoseEightyThisQuarterBarText   = "Indlæser...";
    self.currentBenefactorNumber            = clubData.activeUsers + clubData.inactiveUsers;
    self.currentInactiveBenefactors         = clubData.inactiveUsers;
    self.currentActiveBenefactors           = clubData.activeUsers;
    self.currentActiveBenefactorsPercent    = clubData.percentActiveUsers;
    self.quarterHistory                     = clubData.history;
    self.lastQuartersPoint                  = clubData.history[0];
    self.secondToLastQuartersPoint          = clubData.history[1];
    self.thirdToLastQuartersPoint           = clubData.history[2];
    self.fourthToLastQuartersPoint          = clubData.history[3];
    self.whyZeroInHistoryText               = "Hvorfor 0 ?"
    self.whyZeroInHistoryTextValue          = "Du har ingen historik for sidste kvartal";
    self.daysTillNextPayment                = clubData.daysLeftInQuarter;
    self.timeLeftOfDay                      = "Indlæser...";
    self.appUsers                           = clubData.appUsers;
    self.appUsersPercent                    = clubData.percentAppUsers;
    self.notAppUsers                        = clubData.noAppUsers;
    self.differenceToLastQuarter            = "Indlæser...";
    self.eightyFromTarget                   = "Indlæser...";
    self.thisYear                           = new Date().getFullYear();
    self.target                             = clubData.target;

    //set the quarter history text box text
    if (self.lastQuartersPoint.points != 0) {
        self.whyZeroInHistoryText = "Samlet antal point sidste 4 kvartaller";
        //sum the amount gained for last 4 quarters
        self.whyZeroInHistoryTextValue = (self.lastQuartersPoint.points + self.secondToLastQuartersPoint.points + self.thirdToLastQuartersPoint.points + self.fourthToLastQuartersPoint.points);
        $("#whyZeroInHistoryTextValue").css("font-size", "52px");
        $("#whyZeroInHistoryTextValue").css("margin-top", "0px");
        $("#whyZeroInHistoryTextValue").append("<span style='font-size: 18px;'></span>");
    }

    //add profilepicture to view
    $("#profilePictureClub").html('<img id="profilePicture" src="' + self.ondoProfilePicture + '" alt="Profile picture" />');

    var activityArray = [];
    var myClubChart;
    var miniLineChart;
    var myHistoryBarChart;
    var currentQuarter = dateFactory.getCurrentQuarter();
    var currentYear = dateFactory.getDkTime().getFullYear();

    //top box hover functions
    $(".boxClub").mouseenter(function () {
        var color = $(this).css("border-color");
        var colorStr = "rgba" + color.substring(3, color.length - 1) + ", .5)"
        $(this).css("background-color", colorStr);
    });
    $(".boxClub").mouseleave(function () {
        var color = $(this).css("border-color");
        var colorStr = "rgba" + color.substring(3, color.length - 1) + ", .2)"
        $(this).css("background-color", colorStr);
    });

    //toggle color theme
    // initiate a new Toggles class
    // With options (defaults shown below)
    $('.toggle').toggles({
        drag: true, // allow dragging the toggle between positions
        click: true, // allow clicking on the toggle
        text: {
            on: 'LIGHT', // text for the ON position
            off: 'DARK' // and off
        },
        on: true, // is the toggle ON on init
        animate: 250, // animation time (ms)
        easing: 'swing', // animation transition easing function
        checkbox: null, // the checkbox to toggle (for use in forms)
        clicker: null, // element that can be clicked on to toggle. removes binding from the toggle itself (use nesting)
        width: 50, // width used if not set in css
        height: 20, // height if not set in css
        type: 'compact' // if this is set to 'select' then the select style toggle will be used
    });


    // Getting notified of changes, and the new state:
    $('.toggle').on('toggle', function (e, active) {
        if (!active) {
            $('body').attr('style', 'transition: .2s all ease; overflow-x: hidden; background-color: #353535!important;');
            $(".clubContent").attr("style", "color: white!important; transition: .2s all ease;");
            $(".boxClub").css("color", "white");
            showGraphElement(5);
            Chart.defaults.global.defaultFontColor = '#FFF';
            barGraphValueColor = "#FFF";
            gridColor = "#444444";
            setTimeout(function () { createBarHistoryGraph(); }, 100);
        } else {
            $('body').attr('style', 'transition: .2s all ease; overflow-x: hidden; background-color: white!important;');
            $(".clubContent").attr("style", "color: black!important; transition: .2s all ease;");
            $(".boxClub").css("color", "black");
            showGraphElement(5);
            Chart.defaults.global.defaultFontColor = '#000';
            barGraphValueColor = "#000";
            gridColor = "#F3F3F3";
            setTimeout(function () { createBarHistoryGraph(); }, 100);
        }
    });

    //set top menu box colors function
    var setTopBoxColors = function (value, box) {
        //array with gradient color from red to green
        var colorArray = [
            "rgba(239, 56, 91, ",
            "rgba(241, 179, 27, ",
            "rgba(92, 197, 13, "
        ];
        //set color of background and border color
        if (value <= 40) {
            $(box).css("background-color", colorArray[0] + ".2)");
            $(box).css("border-color", colorArray[0] + "1)");
        } else if (value > 40 && value < 80) {
            $(box).css("background-color", colorArray[1] + ".2)");
            $(box).css("border-color", colorArray[1] + "1)");
        } else if (value >= 80 && value <= 100) {
            $(box).css("background-color", colorArray[2] + ".2)");
            $(box).css("border-color", colorArray[2] + "1)");
        }


    }

    //show graph element function form child index
    function showGraphElement(childIndex) {
        var graphBoxElement = document.getElementById("clubHistoryGraph");
        //hide all graph elements
        for (var i = 0; i < graphBoxElement.children.length; i++) {
            $(graphBoxElement.children[i]).addClass("hidden");
        }
        //show graph element selected
        $(graphBoxElement.children[childIndex]).removeClass("hidden");
        $(graphBoxElement.children[childIndex]).addClass("animated");
        $(graphBoxElement.children[childIndex]).addClass("fadeIn");

    }

    //Box click event
    $(".boxClub").click(function (event) {
        //get index for graph element from value attribute
        var childIndex = this.getAttribute("value");


        //flip all elements in sidebox menu
        for (var i = 0; i < 4; i++) {
            if ($(".clubHistoryInfo")[0].children[i].getAttribute("value") != childIndex && $($(".clubHistoryInfo")[0].children[i]).hasClass("flipInX")) {
                $($(".clubHistoryInfo")[0].children[i]).addClass("animated");
                $($(".clubHistoryInfo")[0].children[i]).addClass("flipInY");
                $($(".clubHistoryInfo")[0].children[i].children[0]).removeClass("hidden");
                $($(".clubHistoryInfo")[0].children[i].children[1]).addClass("hidden");
                $($(".clubHistoryInfo")[0].children[i]).css("cursor", "pointer");
            }
        }

        //show graph element
        showGraphElement(childIndex);

        if ($(event.currentTarget).hasClass("animated") && $(event.currentTarget).hasClass("flipInX")) {
            $(event.currentTarget).removeClass("flipInX");
            $(event.currentTarget).addClass("flipInY");
            $(event.currentTarget.children[0]).removeClass("hidden");
            $(event.currentTarget.children[1]).addClass("hidden");
        } else {
            $(event.currentTarget).removeClass("flipInY");
            $(event.currentTarget).addClass("flipInX");
            $(event.currentTarget.children[0]).addClass("hidden");
            $(event.currentTarget.children[1]).removeClass("hidden");
        }
    });


    //SideBox click event
    $(".clubHistoryInfoBox").click(function (event) {
        var childIndex = this.getAttribute("value");
        var childrenArraySize = event.currentTarget.parentElement.children.length;

        //flip all elements in top box menu
        for (var i = 0; i < 4; i++) {
            if ($($(".boxInfoClub")[0].children[0].children[i].children[0]).hasClass("flipInX")) {
                $($(".boxInfoClub")[0].children[0].children[i].children[0]).removeClass("flipInX");
                $($(".boxInfoClub")[0].children[0].children[i].children[0]).addClass("flipInY");
                $($(".boxInfoClub")[0].children[0].children[i].children[0].children[0]).removeClass("hidden");
                $($(".boxInfoClub")[0].children[0].children[i].children[0].children[1]).addClass("hidden");
                $($(".boxInfoClub")[0].children[0]).css("cursor", "pointer");
            }
        }

        //show graph element
        showGraphElement(parseInt(childIndex));

        //flip all elements not selected
        for (var i = 0; i < childrenArraySize; i++) {
            if (event.currentTarget.parentElement.children[i].getAttribute("value") != childIndex && $(event.currentTarget.parentElement.children[i]).hasClass("flipInX")) {
                $(event.currentTarget.parentElement.children[i]).addClass("animated");
                $(event.currentTarget.parentElement.children[i]).addClass("flipInY");
                $(event.currentTarget.parentElement.children[i].children[0]).removeClass("hidden");
                $(event.currentTarget.parentElement.children[i].children[1]).addClass("hidden");
                $(event.currentTarget.parentElement.children[i]).css("cursor", "pointer");
            }
        }

        //flip object selected
        if (!$(event.currentTarget).hasClass("animated") && !$(event.currentTarget).hasClass("flipInX")) {
            $(event.currentTarget).addClass("animated");
            $(event.currentTarget).addClass("flipInX");
            $(event.currentTarget.children[0]).addClass("hidden");
            $(event.currentTarget.children[1]).removeClass("hidden");
            $(event.currentTarget).css("cursor", "default");
        } else if ($(event.currentTarget).hasClass("flipInY")) {
            $(event.currentTarget).removeClass("flipInY")
            $(event.currentTarget).addClass("flipInX");
            $(event.currentTarget.children[0]).addClass("hidden");
            $(event.currentTarget.children[1]).removeClass("hidden");
            $(event.currentTarget).css("cursor", "default");
        }
    });


    //flip the start element
    var startFlipElement = document.getElementById("clubHistoryInfoBox0");
    startFlipElement.className = "clubHistoryInfoBox col-md-6 col-sm-3 animated flipInX";
    $(startFlipElement).css("cursor", "default");
    startFlipElement.children[0].className += " hidden";
    startFlipElement.children[1].className == startFlipElement.children[1].className.substring(0, startFlipElement.children[1].className.length - 7);

    //GLOBAL CHANGES TO CHART
    Chart.defaults.global.defaultFontColor      = '#000';
    Chart.defaults.global.scaleBeginAtZero      = true;
    Chart.defaults.global.barBeginAtOrigin      = false;
    Chart.defaults.global.defaultFontFamily     = "'Dosis', sans-serif";
    Chart.defaults.global.legend.display        = false;
    var barGraphValueColor                      = "#000";
    var gridColor                               = "#F3F3F3"

    //create history bar graph function
    var createBarHistoryGraph = function () {
        if (myHistoryBarChart != null) {
            myHistoryBarChart.destroy();
        }

        var myHistoryBarChartElement = document.getElementById("bar-history-club");


        //data
        var data = {
            labels: [
                self.lastQuartersPoint.quarterDate,
                self.secondToLastQuartersPoint.quarterDate,
                self.thirdToLastQuartersPoint.quarterDate,
                self.fourthToLastQuartersPoint.quarterDate,
            ].reverse(),
            datasets: [
                {
                    label: "My First dataset",
                    backgroundColor: [
                        'rgba(157, 235, 99, 0.8)',
                        'rgba(157, 235, 99, 0.8)',
                        'rgba(157, 235, 99, 0.8)',
                        'rgba(157, 235, 99, 0.8)'
                    ],
                    borderColor: [
                        'rgba(157, 235, 99, 1)',
                        'rgba(157, 235, 99, 1)',
                        'rgba(157, 235, 99, 1)',
                        'rgba(157, 235, 99, 1)'
                    ],
                    borderWidth: 1,
                    data: [
                        self.lastQuartersPoint.points,
                        self.secondToLastQuartersPoint.points,
                        self.thirdToLastQuartersPoint.points,
                        self.fourthToLastQuartersPoint.points
                    ].reverse(),
                }
            ]
        };

        myHistoryBarChart = new Chart(myHistoryBarChartElement, {
            type: 'bar',
            data: data,
            options: {
                tooltips: {
                    enabled: false
                },
                responsive: true,
                title: {
                    display: true
                },
                scales: {
                    yAxes: [{
                        gridLines: {
                            color: gridColor,
                        },
                        ticks: {
                            showTooltips: false,
                            beginAtZero: true
                        }
                    }],
                    xAxes: [{
                        gridLines: {
                            color: gridColor,
                        }
                    }]
                },
                hover: {
                    animationDuration: 0
                },
                animation: {
                    onComplete: function () {
                        var chartInstance = this.chart;
                        var ctx = chartInstance.ctx;
                        ctx.textAlign = "center";
                        ctx.fillStyle = barGraphValueColor;
                        Chart.helpers.each(this.data.datasets.forEach(function (dataset, i) {
                            var meta = chartInstance.controller.getDatasetMeta(i);
                            Chart.helpers.each(meta.data.forEach(function (bar, index) {
                                ctx.fillText(dataset.data[index] + " point", bar._model.x, bar._model.y - 12);
                            }), this)
                        }), this);
                    }
                }
            }
        });
    };

    createBarHistoryGraph();

    //progress bar current figures
    if (self.prognoseThisQuarterFigures > self.target) {
        self.miniBarCurrentProgress = "100%";
    } else {
        var currentProgressPercent = (self.prognoseThisQuarterFigures * 100) / self.target;
        self.miniBarCurrentProgress = currentProgressPercent + "%";
        if (self.prognoseThisQuarter > self.target) {
            self.miniBarPrognoseProgress = (100 - currentProgressPercent) + "%";
        } else {
            self.miniBarPrognoseProgress = (((self.prognoseThisQuarter - self.prognoseThisQuarterFigures) * 100) / self.target) + "%";
        }

    }

    //progress bar 10% figures
    if (self.prognoseTenThisQuarterFigures > self.target) {
        self.miniBarTenProgress = "100%";
    } else {
        var tenProgressPercent = (self.prognoseTenThisQuarterFigures * 100) / self.target;
        self.miniBarTenProgress = tenProgressPercent + "%";
        if (self.prognoseTenThisQuarter > self.target) {
            self.miniBarTenPrognoseProgress = (100 - tenProgressPercent) + "%";
        } else {
            self.miniBarTenPrognoseProgress = (((self.prognoseTenThisQuarter - self.prognoseTenThisQuarterFigures) * 100) / self.target) + "%";
        }

    }

    //progress bar 80% figures
    if (self.prognoseEightyThisQuarterFigures > self.target) {
        self.miniBarEightyProgress = "100%";
    } else {
        var eightyProgressPercent = (self.prognoseEightyThisQuarterFigures * 100) / self.target;
        self.miniBarEightyProgress = eightyProgressPercent + "%";
        if (self.prognoseEightyThisQuarter > self.target) {
            self.miniBarEightyPrognoseProgress = (100 - eightyProgressPercent) + "%";
        } else {
            self.miniBarEightyPrognoseProgress = (((self.prognoseEightyThisQuarter - self.prognoseEightyThisQuarterFigures) * 100) / self.target) + "%";
        }

    }

    //check eightypercent prognose numbers compared to target
    if (self.prognoseEightyThisQuarter > self.target) {
        self.eightyFromTarget = self.prognoseEightyThisQuarter - self.target + " point over target."
    } else if (self.prognoseEightyThisQuarter < self.target) {
        self.eightyFromTarget = self.prognoseEightyThisQuarter - self.target + " point under target."
    } else if (self.prognoseEightyThisQuarter == self.target) {
        self.eightyFromTarget = "Target: " + self.target + " point";
    }

    //check prognosebar text
    if (((self.prognoseThisQuarter - self.prognoseThisQuarterFigures) / self.target) * 100 < 10) {
        self.prognoseThisQuarterBarText = "";
    } else if (((self.prognoseThisQuarter - self.prognoseThisQuarterFigures) / self.target) * 100 >= 10) {
        self.prognoseThisQuarterBarText = self.prognoseThisQuarter + " point";
    }

    if (self.prognoseEightyThisQuarter < self.target) {
        if (((self.prognoseEightyThisQuarter - self.prognoseEightyThisQuarterFigures) / self.target) * 100 < 10) {
            self.prognoseEightyThisQuarterBarText = "";
        } else if (((self.prognoseEightyThisQuarter - self.prognoseEightyThisQuarterFigures) / self.target) * 100 >= 10) {
            self.prognoseEightyThisQuarterBarText = self.prognoseEightyThisQuarter + " point";
        }
    } else if (self.prognoseEightyThisQuarter > self.target) {
        if (((self.prognoseEightyThisQuarter - self.prognoseEightyThisQuarterFigures) / self.prognoseEightyThisQuarter) * 100 < 10) {
            self.prognoseEightyThisQuarterBarText = "";
        } else if (((self.prognoseEightyThisQuarter - self.prognoseEightyThisQuarterFigures) / self.prognoseEightyThisQuarter) * 100 >= 10) {
            self.prognoseEightyThisQuarterBarText = self.prognoseEightyThisQuarter + " point";
        }
    }

    //show date and time function
    setInterval(function () {
        $scope.$apply(function () {
            var date = dateFactory.getDkTime();
            self.time = (date.getHours() < 10 ? "0" + date.getHours() : date.getHours()) + ":" + (date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes()) + ":" + (date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds());
            //get day in danish
            switch (date.getUTCDay()) {
                case 0:
                    self.day = "Søndag"
                    break;
                case 1:
                    self.day = "Mandag"
                    break;
                case 2:
                    self.day = "Tirsdag"
                    break;
                case 3:
                    self.day = "Onsdag"
                    break;
                case 4:
                    self.day = "Torsdag"
                    break;
                case 5:
                    self.day = "Fredag"
                    break;
                case 6:
                    self.day = "Lørdag"
                    break;
            }
            //get month in danish    
            switch (date.getUTCMonth()) {
                case 0:
                    self.month = "Januar";
                    break;
                case 1:
                    self.month = "Februar";
                    break;
                case 2:
                    self.month = "Marts";
                    break;
                case 3:
                    self.month = "April";
                    break;
                case 4:
                    self.month = "Maj";
                    break;
                case 5:
                    self.month = "Juni";
                    break;
                case 6:
                    self.month = "Juli";
                    break;
                case 7:
                    self.month = "August";
                    break;
                case 8:
                    self.month = "September";
                    break;
                case 9:
                    self.month = "Oktober";
                    break;
                case 10:
                    self.month = "November";
                    break;
                case 11:
                    self.month = "December";
                    break;
            }
            self.date = date.getDate();
        });
    }, 1000);

    //show time left of day function
    setInterval(function () {
        $scope.$apply(function () {
            var date = dateFactory.getDkTime();
            self.timeLeftOfDay = (24 - (date.getHours() + 1) < 10 ? "0" + (24 - (date.getHours() + 1)) : 24 - (date.getHours() + 1)) + ":" + (60 - date.getMinutes() < 10 ? "0" + (60 - date.getMinutes()) : 60 - date.getMinutes()) + ":" + (60 - date.getSeconds() < 10 ? "0" + (60 - date.getSeconds()) : 60 - date.getSeconds());
        });
    }, 1000);


    //footer menu icon events
    var isMenuOpen = false;
    //menu text default
    self.menuText = "Klik for at åbne menu";

    //rotate on mouse enter
    $("#footerMenuIcon").mouseenter(function () {
        $(this).css("transform", "rotate(-45deg)");
        //show menu text
        $("#footerMenuIconText").css("opacity", "1");
    });

    //rotate back on mouse leave
    $("#footerMenuIcon").mouseleave(function () {
        $(this).css("transform", "rotate(45deg)");
        //hide menu text
        $("#footerMenuIconText").css("opacity", "0");
    });

    //shrink on mousedown
    $("#footerMenuIcon").mousedown(function () {
        $(this).css("transform", "scale(.8)");
        if (!isMenuOpen) {
            $("#navMenu").css("z-index", "10");
            $("#navMenu").css("opacity", "1");
            if (window.innerWidth < 767) {
                $("#navMenu").css("bottom", "60px");
            } else {
                $("#navMenu").css("bottom", "40px");
            }
            isMenuOpen = true;
            $("#footerMenuIconText").css("opacity", "0");
            self.menuText = "Luk menu";
        } else {
            isMenuOpen = false;
            $("#navMenu").css("opacity", "0");
            $("#navMenu").css("z-index", "-10");
            if (window.innerWidth < 767) {
                $("#navMenu").css("bottom", "-60px");
            } else {
                $("#navMenu").css("bottom", "-40px");
            }
            $("#footerMenuIconText").css("opacity", "0");
            self.menuText = "Klik for at åbne menu";
        }
    });

    //resize and open menu on mouseup
    $("#footerMenuIcon").mouseup(function () {
        $(this).css("transform", "scale(1)");
    });

    //logout function
    self.logout = function () {
        //remove the session object
        sessionStorage.clear();
        //redirect to loginpage
        window.location.href = "/";
    }

}])
app.controller("shopCtrl", ["$scope", "$http", "connectionUrlFactory", "sessionStoreFactory", "dateFactory", function ($scope, $http, connectionUrlFactory, sessionStoreFactory, dateFactory) {
    var self = this;

    //end loading gif and display content
    $(".mainContent").css("-webkit-filter", "blur(0px)");
    $(".mainContent").css("filter", "blur(0px)");
    $("#loading").css("display", "none");

    //if userdata is null then redirect to login page
    if (sessionStorage.getItem("userData") == null) {
        window.location.href = "/";
    }

    var ondo                                    = sessionStoreFactory.load("currentOndo")
    self.data                                   = sessionStoreFactory.loadJSON("Data:" + ondo);

    var myLineChartShop;


    self.currentWeek                            = self.data.weekNo;

    self.transactionsForCityCurrentWeek         = self.data.transactionsCurrentWeekCity;
    self.transactionsForCityCurrentQuarter      = self.data.transactionsCurrentQuarterCity;
    self.transactionsQuarterAvg                 = self.data.transactionsQuarterCityAvg;
    self.transactionsAgainstLastWeek            = "";
    self.transactionsAgainstQuarterAvg          = "";

    self.subscriptionsForCityCurrentWeek        = self.data.subscriptionsCurrentWeekCity;
    self.subscriptionsForCityCurrentQuarter     = self.data.subscriptionsCurrentQuarterCity;
    self.subscriptionsQuarterAvg                = self.data.subsriptionsQuarterCityAvg;
    self.subscriptionsAgainstLastWeek           = "";
    self.subscriptionsAgainstQuarterAvg         = "";

    //get box information
    self.creationsInShopCurrentWeek             = self.data.subscriptionsCurrentWeekShop;
    self.transactionsInShopCurrentWeek          = self.data.transactionsCurrentWeekShop;
    self.creationsInShopCurrentQuarter          = self.data.subscriptionsCurrentQuarterShop;
    self.transactionsInShopCurrentQuarter       = self.data.transactionsCurrentQuarterShop;

    //get points information
    self.pointsCurrentQuarter                   = self.data.pointsCurrentQuarter;

    //references to graph elements from page
    var transactionsInShopBarChart = document.getElementById("bar-transactions-shop");

    var calculateWeeklyData = function () {

        var lastWeeksTransactions               = self.data.weekDataArray[1].transactions;
        var PreviousToLastWeeksTransactions     = self.data.weekDataArray[2].transactions;
        var lastWeeksSubscriptions              = self.data.weekDataArray[1].subscriptions;
        var PreviousToLastWeeksSubscriptions    = self.data.weekDataArray[2].subscriptions;

        var quartersTransactionsAvg             = 0;
        var quartersSubscriptionsAvg            = 0;

        for (var i = 0; i < self.data.weekDataArray.length; i++) {
            quartersTransactionsAvg += self.data.weekDataArray[i].transactions;
            quartersSubscriptionsAvg += self.data.weekDataArray[i].subscriptions;
        }

        quartersTransactionsAvg                 = quartersTransactionsAvg / self.data.weekDataArray.length;
        quartersSubscriptionsAvg                = quartersSubscriptionsAvg / self.data.weekDataArray.length;

        //set the arrow for transactions against last weeks transactions
        if (PreviousToLastWeeksTransactions < lastWeeksTransactions) {
            $("#transAgainstWeek").addClass("fa-arrow-up");
        } else if (PreviousToLastWeeksTransactions === lastWeeksTransactions) {
            $("#transAgainstWeek").addClass("fa-arrow-left");
        } else if (PreviousToLastWeeksTransactions > lastWeeksTransactions) {
            $("#transAgainstWeek").addClass("fa-arrow-down");
        }

        //set the arrow for subscriptions against last weeks subscriptions
        if (PreviousToLastWeeksSubscriptions < lastWeeksSubscriptions) {
            $("#subsAgainstWeek").addClass("fa-arrow-up");
        } else if (PreviousToLastWeeksSubscriptions === lastWeeksSubscriptions) {
            $("#subsAgainstWeek").addClass("fa-arrow-left");
        } else if (PreviousToLastWeeksSubscriptions > lastWeeksSubscriptions) {
            $("#subsAgainstWeek").addClass("fa-arrow-down");
        }

        //set the arrow for transactions against quarters transactions
        if (quartersTransactionsAvg < lastWeeksTransactions) {
            $("#transAgainstQuart").addClass("fa-arrow-up");
        } else if (quartersTransactionsAvg === lastWeeksTransactions) {
            $("#transAgainstQuart").addClass("fa-arrow-left");
        } else if (quartersTransactionsAvg > lastWeeksTransactions) {
            $("#transAgainstQuart").addClass("fa-arrow-down");
        }

        //set the arrow for subscriptions against quarters subscriptions
        if (quartersSubscriptionsAvg < lastWeeksSubscriptions) {
            $("#subsAgainstQuart").addClass("fa-arrow-up");
        } else if (quartersSubscriptionsAvg === lastWeeksSubscriptions) {
            $("#subsAgainstQuart").addClass("fa-arrow-left");
        } else if (quartersSubscriptionsAvg > lastWeeksSubscriptions) {
            $("#subsAgainstQuart").addClass("fa-arrow-down");
        }
    }
    calculateWeeklyData();

    //GLOBAL CHANGES TO CHART
    Chart.defaults.global.defaultFontColor = '#000';
    Chart.defaults.global.scaleBeginAtZero = true;
    Chart.defaults.global.barBeginAtOrigin = false;

    //FUNCTION TO CREATE GRAPH TAKES DATA OBJECT WITH TWO ARRAYS - DATELABELS AND DATA
    var createLineGraph = function () {
        //REMOVES THE CHART IF ALREADY EXIST TO INSURE CORRECT UPDATE
        if (myLineChartShop != null) {
            myLineChartShop.destroy();
        }


        //GLOBAL CHANGES TO CHART
        Chart.defaults.global.defaultFontColor = '#000';
        Chart.defaults.global.scaleBeginAtZero = true;
        Chart.defaults.global.barBeginAtOrigin = false;
        Chart.defaults.global.defaultFontFamily = "'Dosis', sans-serif";

        // GET GRAPH DOM ELEMENT
        var transactionsGraph = document.getElementById("line-transactions-shop");

        // Generate arrays to hold data
        var labels          = [];
        var transactions    = [];
        var subscriptions   = [];
        var activity        = [];

        //fill arrays with data
        for (var i = 0; i < self.data.weekDataArray.length; i++) {
            labels.push(self.data.weekDataArray[i].weekLabel);
            transactions.push(self.data.weekDataArray[i].transactions);
            subscriptions.push(self.data.weekDataArray[i].subscriptions);
            activity.push(self.data.weekDataArray[i].activity);
        }

        //DATA OBJECT
        var data = {
            labels: labels, // ALL THE NAMES OF THE DATES
            datasets: [
                {
                    label: "Antal transaktioner",
                    fill: true,
                    lineTension: 0.1,
                    backgroundColor: "rgba(97,147,189,0.4)",
                    borderColor: "rgba(97,147,189,1)",
                    borderCapStyle: 'butt',
                    borderDash: [],
                    borderDashOffset: 0.0,
                    borderJoinStyle: 'miter',
                    pointBorderColor: "rgba(75,192,192,1)",
                    pointBackgroundColor: "#fff",
                    pointBorderWidth: 1,
                    pointHoverRadius: 5,
                    pointHoverBackgroundColor: "rgba(75,192,192,1)",
                    pointHoverBorderColor: "rgba(220,220,220,1)",
                    pointHoverBorderWidth: 2,
                    pointRadius: 1,
                    pointHitRadius: 10,
                    data: transactions, // ALL THE VALUES FOR EACH DATE
                    spanGaps: false,
                },
                {
                    label: "Antal oprettelser",
                    fill: true,
                    lineTension: 0.1,
                    backgroundColor: "rgba(13,24,192,0.4)",
                    borderColor: "rgba(13,24,135,1)",
                    borderCapStyle: 'butt',
                    borderDash: [],
                    borderDashOffset: 0.0,
                    borderJoinStyle: 'miter',
                    pointBorderColor: "rgba(75,192,192,1)",
                    pointBackgroundColor: "#fff",
                    pointBorderWidth: 1,
                    pointHoverRadius: 5,
                    pointHoverBackgroundColor: "rgba(75,192,192,1)",
                    pointHoverBorderColor: "rgba(220,220,220,1)",
                    pointHoverBorderWidth: 2,
                    pointRadius: 1,
                    pointHitRadius: 10,
                    data: subscriptions, // ALL THE VALUES FOR EACH DATE
                    spanGaps: false,
                },
                {
                    label: "Aktivitet",
                    fill: true,
                    lineTension: 0.1,
                    backgroundColor: "rgba(133,185,228,0.4)",
                    borderColor: "rgba(133,185,228,1)",
                    borderCapStyle: 'butt',
                    borderDash: [],
                    borderDashOffset: 0.0,
                    borderJoinStyle: 'miter',
                    pointBorderColor: "rgba(75,192,192,1)",
                    pointBackgroundColor: "#fff",
                    pointBorderWidth: 1,
                    pointHoverRadius: 5,
                    pointHoverBackgroundColor: "rgba(75,192,192,1)",
                    pointHoverBorderColor: "rgba(220,220,220,1)",
                    pointHoverBorderWidth: 2,
                    pointRadius: 1,
                    pointHitRadius: 10,
                    data: activity, // ALL THE VALUES FOR EACH DATE
                    spanGaps: false,
                }
            ]
        };

        // CREATE NEW CHARTJS LINECHART
        myLineChartShop = new Chart(transactionsGraph, {
            type: 'line',
            data: data,
            options: {
                responsive: true
            }
        });
    };
    createLineGraph();
}])
app.controller('shopSubscriptionsCtrl', ["sessionStoreFactory", function (sessionStoreFactory) {
    var self = this;

    //end loading gif and display content
    $(".mainContent").css("-webkit-filter", "blur(0px)");
    $(".mainContent").css("filter", "blur(0px)");
    $("#loading").css("display", "none");

    var currentOndo = sessionStoreFactory.load("currentOndo");
    self.data = sessionStoreFactory.loadJSON("Data:" + currentOndo);
    var myLineChartSubscriptions;

    //create line graph vertical
    var createLineGraph = function () {
        //REMOVES THE CHART IF ALREADY EXIST TO INSURE CORRECT UPDATE
        if (myLineChartSubscriptions != null) {
            myLineChartSubscriptions.destroy();
        }

        //TAKES THE DATE LABELS AND CONVERTS TO DD-MM-YYYY
        var labels = [];
        var subscriptions = [];

        for (var i = 0; i < self.data.dailyDataArray.length; i++) {
            labels.push(self.data.dailyDataArray[i].date);
            subscriptions.push(self.data.dailyDataArray[i].subscriptions);
        }

        //GLOBAL CHANGES TO CHART
        Chart.defaults.global.defaultFontColor = '#000';
        Chart.defaults.global.scaleBeginAtZero = true;
        Chart.defaults.global.barBeginAtOrigin = false;

        // GET GRAPH DOM ELEMENT
        var transactionsGraph = document.getElementById("line-subscriptions-shop-menu");

        //DATA OBJECT
        var data = {
            labels: labels, // ALL THE NAMES OF THE DATES
            datasets: [
                {
                    label: "Antal transaktioner",
                    fill: true,
                    lineTension: 0.1,
                    backgroundColor: "rgba(75,192,192,0.4)",
                    borderColor: "rgba(75,192,192,1)",
                    borderCapStyle: 'butt',
                    borderDash: [],
                    borderDashOffset: 0.0,
                    borderJoinStyle: 'miter',
                    pointBorderColor: "rgba(75,192,192,1)",
                    pointBackgroundColor: "#fff",
                    pointBorderWidth: 1,
                    pointHoverRadius: 5,
                    pointHoverBackgroundColor: "rgba(75,192,192,1)",
                    pointHoverBorderColor: "rgba(220,220,220,1)",
                    pointHoverBorderWidth: 2,
                    pointRadius: 1,
                    pointHitRadius: 10,
                    data: subscriptions, // ALL THE VALUES FOR EACH DATE
                    spanGaps: false,
                }
            ]
        };

        // CREATE NEW CHARTJS LINECHART
        myLineChartSubscriptions = new Chart(transactionsGraph, {
            type: 'bar',
            data: data,
            options: {
                responsive: true,
                legend: {
                    display: false
                }
            }
        });
    };
    createLineGraph();
}])
app.controller("shopTransactionsCtrl", ["sessionStoreFactory", function (sessionStoreFactory) {
    var self = this;

    //end loading gif and display content
    $(".mainContent").css("-webkit-filter", "blur(0px)");
    $(".mainContent").css("filter", "blur(0px)");
    $("#loading").css("display", "none");

    var currentOndo = sessionStoreFactory.load("currentOndo");
    self.data = sessionStoreFactory.loadJSON("Data:" + currentOndo);
    var myLineChartTransactions;

    //create line graph vertical
    var createLineGraph = function () {
        //REMOVES THE CHART IF ALREADY EXIST TO INSURE CORRECT UPDATE
        if (myLineChartTransactions != null) {
            myLineChartTransactions.destroy();
        }

        //TAKES THE DATE LABELS AND CONVERTS TO DD-MM-YYYY
        var labels = [];
        var transactions = [];

        for (var i = 0; i < self.data.dailyDataArray.length; i++) {
            labels.push(self.data.dailyDataArray[i].date);
            transactions.push(self.data.dailyDataArray[i].transactions);
        }

        //GLOBAL CHANGES TO CHART
        Chart.defaults.global.defaultFontColor = '#000';
        Chart.defaults.global.scaleBeginAtZero = true;
        Chart.defaults.global.barBeginAtOrigin = false;

        // GET GRAPH DOM ELEMENT
        var transactionsGraph = document.getElementById("line-transactions-shop-menu");

        //DATA OBJECT
        var data = {
            labels: labels, // ALL THE NAMES OF THE DATES
            datasets: [
                {
                    label: "Antal transaktioner",
                    fill: true,
                    lineTension: 0.1,
                    backgroundColor: "rgba(75,192,192,0.4)",
                    borderColor: "rgba(75,192,192,1)",
                    borderCapStyle: 'butt',
                    borderDash: [],
                    borderDashOffset: 0.0,
                    borderJoinStyle: 'miter',
                    pointBorderColor: "rgba(75,192,192,1)",
                    pointBackgroundColor: "#fff",
                    pointBorderWidth: 1,
                    pointHoverRadius: 5,
                    pointHoverBackgroundColor: "rgba(75,192,192,1)",
                    pointHoverBorderColor: "rgba(220,220,220,1)",
                    pointHoverBorderWidth: 2,
                    pointRadius: 1,
                    pointHitRadius: 10,
                    data: transactions, // ALL THE VALUES FOR EACH DATE
                    spanGaps: false,
                }
            ]
        };

        // CREATE NEW CHARTJS LINECHART
        myLineChartTransactions = new Chart(transactionsGraph, {
            type: 'bar',
            data: data,
            options: {
                responsive: true,
                legend: {
                    display: false
                }
            }
        });
    };
    createLineGraph();
}])
app.controller("shopHistoryCtrl", ["sessionStoreFactory", function (sessionStoreFactory) {
    var self = this;

    self.ondo = sessionStoreFactory.load("currentOndo");
    self.data = sessionStoreFactory.loadJSON("Data:" + self.ondo);

    var myChartShopHistory;
    var allDataForQuarter;
    var weeksArray = [];
    var myLineChartShop;

    var quarterDataBarChart = document.getElementById("bar-transactions-history");

    self.quarters = [];
    self.quartersSelected = [];
    self.quarterSelected = {};
    self.ondoTransactionsData = [];

    //populate quarters array with quarters in history
    for (var i = 0; i < self.data.historyDataArray.length; i++) {
        self.quarters.push(self.data.historyDataArray[i].quarterLabel);
    }

    //function to check how many quarters is selected
    var checkForOptionsSelected = function () {

        var optionsSelected = [];
        var sel = document.getElementById('quarterSelect');
        var opt = sel && sel.options;

        for (var i = 0; i < opt.length; i++) {
            var option = opt[i];
            if (option.selected) {
                optionsSelected.push(option.value || option.text);
            }
        };

        return optionsSelected.length;
    };

    // select function
    $("#quarterSelect").mousedown(function (e) {
        e.preventDefault();

        var select = this;
        var scroll = select.scrollTop;

        e.target.selected = !e.target.selected;

        setTimeout(function () { select.scrollTop = scroll; }, 0);

        $(select).focus();


        //check if any option is selected to enable the get data button
        if (checkForOptionsSelected() > 0) {
            $("#getHistoryDataBtn").removeAttr("disabled");
        } else {
            $("#getHistoryDataBtn").attr("disabled", "disabled");
        }

        //check for options selected
    }).mousemove(function (e) { e.preventDefault() });

    //create chart function
    var createBarChart = function () {
        //if chart exist then destroy
        if (myChartShopHistory != null) {
            myChartShopHistory.destroy();
        }

        var labels = [];
        var points = [];
        var subscriptions = [];
        var transactions = [];


        //add labes and points to two arrays to be used in the chart
        for (var i = 0; i < self.quartersSelected.length; i++) {
            labels.push(allDataForQuarter[i].label);
            points.push(allDataForQuarter[i].points);
            subscriptions.push(allDataForQuarter[i].subscriptions);
            transactions.push(allDataForQuarter[i].transactions);
        }

        //GLOBAL CHANGES TO CHART
        Chart.defaults.global.defaultFontColor = '#000';
        Chart.defaults.global.scaleBeginAtZero = true;
        Chart.defaults.global.barBeginAtOrigin = false;
        Chart.defaults.global.defaultFontFamily = "'Dosis', sans-serif";
        Chart.defaults.global.legend.display = true;
        var barGraphValueColor = "#000";
        var gridColor = "#F3F3F3"

        //create bar chart over transactions
        myChartShopHistory = new Chart(quarterDataBarChart, {
            type: 'bar',
            data: {
                labels: labels.reverse(),
                fontColor: '#FFFFFF',
                datasets: [{
                    label: "Oprettelser",
                    data: subscriptions.reverse(),
                    backgroundColor: '#c74658',
                    borderColor: '#c74658',
                    borderWidth: 1
                },
                {
                    label: "Transaktioner",
                    data: transactions.reverse(),
                    backgroundColor: '#5cabca',
                    borderColor: '#5cabca',
                    borderWidth: 1
                },
                {
                    label: "Point",
                    data: points.reverse(),
                    backgroundColor: '#ec9a1f',
                    borderColor: '#ec9a1f',
                    borderWidth: 1
                }]
            },
            options: {
                tooltips: {
                    enabled: false
                },
                responsive: true,
                scaleShowVerticalLines: false,
                legend: {
                    display: true
                },
                scales: {
                    yAxes: [{
                        id: 'y-axis-0',
                        gridLines: {
                            display: true,
                            lineWidth: 1,
                            color: "rgba(0,0,0,0.30)"
                        },
                        ticks: {
                            showTooltips: false,
                            beginAtZero: true,
                            mirror: false,
                            suggestedMin: 0
                        },
                        afterBuildTicks: function (chart) {

                        }
                    }],
                    xAxes: [{
                        id: 'x-axis-0',
                        gridLines: {
                            display: false
                        },
                        ticks: {
                            beginAtZero: true,
                            autoSkip: false
                        }
                    }]
                },
                hover: {
                    animationDuration: 0
                },
                animation: {
                    onComplete: function () {
                        var chartInstance = this.chart;
                        var ctx = chartInstance.ctx;
                        ctx.textAlign = "center";
                        ctx.fillStyle = barGraphValueColor;
                        Chart.helpers.each(this.data.datasets.forEach(function (dataset, i) {
                            var meta = chartInstance.controller.getDatasetMeta(i);
                            Chart.helpers.each(meta.data.forEach(function (bar, index) {
                                ctx.fillText(dataset.data[index] + "", bar._model.x, bar._model.y - 20);
                            }), this)
                        }), this);
                    }
                }
            }
        });
    };
    createBarChart();

    //get data for quarters selected
    var getData = function () {
        //empty all data for quarter array
        allDataForQuarter = [];

        for (var h = 0; h < self.quartersSelected.length; h++) {

            for (var j = 0; j < self.data.historyDataArray.length; j++) {
                if (self.quartersSelected[h] == self.data.historyDataArray[j].quarterLabel) {
                    allDataForQuarter.push({ label: self.quartersSelected[h], subscriptions: self.data.historyDataArray[j].subscriptions, transactions: self.data.historyDataArray[j].transactions, points: self.data.historyDataArray[j].points });
                }
            }

            //if more than one quarter is selected then insert quarter for shop selector
            if (self.quartersSelected.length > 1) {
                $("#quarterForShopDropDown").css("display", "block");
            } else {
                //quarter and year number from selected quarter
                var quarterno = self.quartersSelected[0].substring(0, 1);
                var quarteryear = self.quartersSelected[0].substring(11, 15);
                self.quarterSelected = { quarterNo: quarterno, quarterYear: quarteryear };

                populateShopTabel(quarterno, quarteryear);
                createLineGraph(quarterno, quarteryear);
            };
        }
        createBarChart();

        //end loading gif and display content
        $(".mainContent").css("-webkit-filter", "blur(0px)");
        $(".mainContent").css("filter", "blur(0px)");
        $("#loading").css("display", "none");
    };

    // get history data function, if cached then retrieve from storage else retrive from api
    self.getHistoryData = function () {

        //start loading gif
        $(".mainContent").css("-webkit-filter", "blur(10px)");
        $(".mainContent").css("filter", "blur(10px)");
        $("#loading").css("display", "block");

        self.quartersSelected = [];
        var sel = document.getElementById('quarterSelect');
        var opt = sel && sel.options

        for (var i = 0; i < opt.length; i++) {
            var option = opt[i];
            if (option.selected) {
                self.quartersSelected.push(option.value || option.text);
            }
        };

        getData();
    };

    //populate shop table function
    var populateShopTabel = function (quarterNo, year) {
        for (var i = 0; i < self.data.historyDataArray.length; i++) {
            if (self.data.historyDataArray[i].quarterLabel == quarterNo + ". Kvartal " + year) {
                for (var j = 0; j < self.data.historyDataArray[i].historyWeekDataArray.length; j++) {
                    weeksArray.push(self.data.historyDataArray[i].historyWeekDataArray[j])
                }
            }
        }
        createLineGraph(quarterNo, year);
    }

    //FUNCTION TO CREATE GRAPH TAKES DATA OBJECT WITH TWO ARRAYS - DATELABELS AND DATA
    var createLineGraph = function (quarterNo, year) {
        //REMOVES THE CHART IF ALREADY EXIST TO INSURE CORRECT UPDATE
        if (myLineChartShop != null) {
            myLineChartShop.destroy();
        }

        //GLOBAL CHANGES TO CHART
        Chart.defaults.global.defaultFontColor = '#000';
        Chart.defaults.global.scaleBeginAtZero = true;
        Chart.defaults.global.barBeginAtOrigin = false;
        Chart.defaults.global.defaultFontFamily = "'Dosis', sans-serif";

        // GET GRAPH DOM ELEMENT
        var transactionsGraph = document.getElementById("line-transactions-shop-history");

        var labels          = [];
        var subscriptions   = [];
        var transactions    = [];
        var activity        = [];

        for (var i = 0; i < weeksArray.length; i++) {
            labels.push(weeksArray[i].weekLabel);
            subscriptions.push(weeksArray[i].subscriptions);
            transactions.push(weeksArray[i].transactions);
            activity.push(weeksArray[i].activity);
        }

        //DATA OBJECT
        var data = {
            labels: labels, // ALL THE NAMES OF THE DATES
            datasets: [
                {
                    label: "Antal transaktioner",
                    fill: true,
                    lineTension: 0.1,
                    backgroundColor: "rgba(97,147,189,0.4)",
                    borderColor: "rgba(97,147,189,1)",
                    borderCapStyle: 'butt',
                    borderDash: [],
                    borderDashOffset: 0.0,
                    borderJoinStyle: 'miter',
                    pointBorderColor: "rgba(75,192,192,1)",
                    pointBackgroundColor: "#fff",
                    pointBorderWidth: 1,
                    pointHoverRadius: 5,
                    pointHoverBackgroundColor: "rgba(75,192,192,1)",
                    pointHoverBorderColor: "rgba(220,220,220,1)",
                    pointHoverBorderWidth: 2,
                    pointRadius: 1,
                    pointHitRadius: 10,
                    data: transactions, // ALL THE VALUES FOR EACH DATE
                    spanGaps: false,
                },
                {
                    label: "Antal oprettelser",
                    fill: true,
                    lineTension: 0.1,
                    backgroundColor: "rgba(13,24,192,0.4)",
                    borderColor: "rgba(13,24,135,1)",
                    borderCapStyle: 'butt',
                    borderDash: [],
                    borderDashOffset: 0.0,
                    borderJoinStyle: 'miter',
                    pointBorderColor: "rgba(75,192,192,1)",
                    pointBackgroundColor: "#fff",
                    pointBorderWidth: 1,
                    pointHoverRadius: 5,
                    pointHoverBackgroundColor: "rgba(75,192,192,1)",
                    pointHoverBorderColor: "rgba(220,220,220,1)",
                    pointHoverBorderWidth: 2,
                    pointRadius: 1,
                    pointHitRadius: 10,
                    data: subscriptions, // ALL THE VALUES FOR EACH DATE
                    spanGaps: false,
                },
                {
                    label: "Aktivitet",
                    fill: true,
                    lineTension: 0.1,
                    backgroundColor: "rgba(133,185,228,0.4)",
                    borderColor: "rgba(133,185,228,1)",
                    borderCapStyle: 'butt',
                    borderDash: [],
                    borderDashOffset: 0.0,
                    borderJoinStyle: 'miter',
                    pointBorderColor: "rgba(75,192,192,1)",
                    pointBackgroundColor: "#fff",
                    pointBorderWidth: 1,
                    pointHoverRadius: 5,
                    pointHoverBackgroundColor: "rgba(75,192,192,1)",
                    pointHoverBorderColor: "rgba(220,220,220,1)",
                    pointHoverBorderWidth: 2,
                    pointRadius: 1,
                    pointHitRadius: 10,
                    data: activity, // ALL THE VALUES FOR EACH DATE
                    spanGaps: false,
                }
            ]
        };

        // CREATE NEW CHARTJS LINECHART
        myLineChartShop = new Chart(transactionsGraph, {
            type: 'line',
            data: data,
            options: {
                responsive: true
            }
        });
    };


    //quarters for shop dropdown click function
    $('#quarterForShopDropDown').on('change', function () {
        //quarter and year number from selected quarter
        var quarterNo = this.value.substring(7, 8);
        var quarterYear = this.value.substring(18, 22);

        self.quarterSelected = { quarterNo: quarterNo, quarterYear: quarterYear };

        populateShopTabel(quarterNo, quarterYear);
    });
}])
app.controller("tradeUnionCtrl", ["sessionStoreFactory", function (sessionStoreFactory) {
    var self = this;

    //if userdata or ondo data is null then redirect to login page
    if (sessionStorage.getItem("userData") == null || sessionStorage.getItem("Data:" + sessionStorage.getItem("currentOndo")) == null) {
        window.location.href = "/";
    }

    //end loading gif and display content
    $(".mainContent").css("-webkit-filter", "blur(0px)");
    $(".mainContent").css("filter", "blur(0px)");
    $("#loading").css("display", "none");

    self.ondo = sessionStoreFactory.load("currentOndo");
    self.data = sessionStoreFactory.loadJSON("Data:" + self.ondo);

    self.transactionsCurrentQuarter = self.data.transactionsQuarter;
    self.subscriptionsCurrentQuarter = self.data.subscriptionsQuarter;
    self.activityCurrentQuarter = self.data.activityQuarter;
    self.pointsCurrentQuarter = self.data.pointsQuarter;
    self.topShops = self.data.topFiveShops;
    self.cardUsersQuarter = self.data.cardUsersQuarter;
    self.cardUsersQuarterPercent = self.data.cardUsersQuarterPercent;
    self.activeCardUsersQuarter = self.data.activeCardUsersQuarter;
    self.activeCardUsersQuarterPercent = self.data.activeCardUsersQuarterPercent;
    self.inactiveCardUsersQuarter = self.data.inactiveCardUsersQuarter;
    self.inactiveCardUsersQuarterPercent = self.data.inactiveCardUsersQuarterPercent;
    self.appUsersQuarter = self.data.appUsersQuarter;
    self.appUsersQuarterPercent = self.data.appUsersQuarterPercent;
    self.cardUsersWithNoClub = self.data.cardUsersWithNoClub;
    self.cardUsersWithNoClubPercent = self.data.cardUsersWithNoClubPercent;

    var myLineChart;
    var modalLineGraph;

    //FUNCTION TO CREATE GRAPH TAKES DATA OBJECT WITH TWO ARRAYS - DATELABELS AND DATA
    var createLineGraph = function () {
        //REMOVES THE CHART IF ALREADY EXIST TO INSURE CORRECT UPDATE
        if (myLineChart != null) {
            myLineChart.destroy();
        }

        //TAKES THE DATE LABELS AND CONVERTS TO DD-MM-YYYY
        var labels = [];
        var subscriptions = [];
        var transactions = [];
        var activity = [];

        for (var i = 0; i < self.data.weeksArrayQuarter.length; i++) {
            labels.push(self.data.weeksArrayQuarter[i].weekLabel);
            subscriptions.push(self.data.weeksArrayQuarter[i].subscriptions);
            transactions.push(self.data.weeksArrayQuarter[i].transactions);
            activity.push(self.data.weeksArrayQuarter[i].activity);
        }

        //GLOBAL CHANGES TO CHART
        Chart.defaults.global.defaultFontColor = '#FFF';
        Chart.defaults.global.scaleBeginAtZero = true;
        Chart.defaults.global.barBeginAtOrigin = false;
        Chart.defaults.global.defaultFontFamily = "'Dosis', sans-serif";
        Chart.defaults.global.legend.display = true;

        // GET GRAPH DOM ELEMENT
        var transactionsGraph = document.getElementById("line-transactions-trade");

        //DATA OBJECT
        var data = {
            labels: labels, // ALL THE NAMES OF THE DATES
            datasets: [
                {
                    label: "Antal transaktioner",
                    fill: true,
                    lineTension: 0.1,
                    backgroundColor: "rgba(97,147,189,0.4)",
                    borderColor: "rgba(97,147,189,1)",
                    borderCapStyle: 'butt',
                    borderDash: [],
                    borderDashOffset: 0.0,
                    borderJoinStyle: 'miter',
                    pointBorderColor: "rgba(75,192,192,1)",
                    pointBackgroundColor: "#fff",
                    pointBorderWidth: 1,
                    pointHoverRadius: 5,
                    pointHoverBackgroundColor: "rgba(75,192,192,1)",
                    pointHoverBorderColor: "rgba(220,220,220,1)",
                    pointHoverBorderWidth: 2,
                    pointRadius: 1,
                    pointHitRadius: 10,
                    data: transactions, // ALL THE VALUES FOR EACH DATE
                    spanGaps: false,
                },
                {
                    label: "Antal oprettelser",
                    fill: true,
                    lineTension: 0.1,
                    backgroundColor: "rgba(13,24,192,0.4)",
                    borderColor: "rgba(13,24,135,1)",
                    borderCapStyle: 'butt',
                    borderDash: [],
                    borderDashOffset: 0.0,
                    borderJoinStyle: 'miter',
                    pointBorderColor: "rgba(75,192,192,1)",
                    pointBackgroundColor: "#fff",
                    pointBorderWidth: 1,
                    pointHoverRadius: 5,
                    pointHoverBackgroundColor: "rgba(75,192,192,1)",
                    pointHoverBorderColor: "rgba(220,220,220,1)",
                    pointHoverBorderWidth: 2,
                    pointRadius: 1,
                    pointHitRadius: 10,
                    data: subscriptions, // ALL THE VALUES FOR EACH DATE
                    spanGaps: false,
                },
                {
                    label: "Aktivitet",
                    fill: true,
                    lineTension: 0.1,
                    backgroundColor: "rgba(133,185,228,0.4)",
                    borderColor: "rgba(133,185,228,1)",
                    borderCapStyle: 'butt',
                    borderDash: [],
                    borderDashOffset: 0.0,
                    borderJoinStyle: 'miter',
                    pointBorderColor: "rgba(75,192,192,1)",
                    pointBackgroundColor: "#fff",
                    pointBorderWidth: 1,
                    pointHoverRadius: 5,
                    pointHoverBackgroundColor: "rgba(75,192,192,1)",
                    pointHoverBorderColor: "rgba(220,220,220,1)",
                    pointHoverBorderWidth: 2,
                    pointRadius: 1,
                    pointHitRadius: 10,
                    data: activity, // ALL THE VALUES FOR EACH DATE
                    spanGaps: false,
                }
            ]
        };

        // CREATE NEW CHARTJS LINECHART
        myLineChart = new Chart(transactionsGraph, {
            type: 'line',
            data: data,
            options: {
                responsive: true
            }
        });
    };

    createLineGraph();
    self.ondoTransactionsData = [];


    self.allOndoDataList = [];

    self.viewOndo = {
        "title": "Indlæser",
        "locality": "her"
    };

    // get ondo data from click event
    self.getAllOndoData = function (event) {
        
        var ondoData;

        for (var i = 0; i < self.data.shops.length; i++) {
            if (event.shop.title == self.data.shops[i].title) { ondoData = self.data.shops[i] }
        }
        self.viewOndo = ondoData;

        var modalGraph = document.getElementById("modalGraph");

        var createLineGraph = function () {
            //REMOVES THE CHART IF ALREADY EXIST TO INSURE CORRECT UPDATE
            if (modalLineGraph != null) {
                modalLineGraph.destroy();
            }

            //TAKES THE DATE LABELS AND CONVERTS TO DD-MM-YYYY
            var labels = [];
            var subscriptions = [];
            var transactions = [];
            var activity = [];

            for (var i = 0; i < ondoData.weeksArray.length; i++) {
                labels.push(ondoData.weeksArray[i].weekLabel);
                subscriptions.push(ondoData.weeksArray[i].subscriptions);
                transactions.push(ondoData.weeksArray[i].transactions);
                activity.push(ondoData.weeksArray[i].activity);
            }

            //GLOBAL CHANGES TO CHART
            Chart.defaults.global.defaultFontColor = '#000';
            Chart.defaults.global.scaleBeginAtZero = true;
            Chart.defaults.global.barBeginAtOrigin = false;
            Chart.defaults.global.defaultFontFamily = "'Dosis', sans-serif";

            // GET GRAPH DOM ELEMENT
            var modalTransactionsGraph = document.getElementById("modal-line-graph");

            //DATA OBJECT
            var graphData = {
                labels: labels, // ALL THE NAMES OF THE DATES
                datasets: [
                    {
                        label: "Antal transaktioner",
                        fill: true,
                        lineTension: 0.1,
                        backgroundColor: "rgba(97,147,189,0.4)",
                        borderColor: "rgba(97,147,189,1)",
                        borderCapStyle: 'butt',
                        borderDash: [],
                        borderDashOffset: 0.0,
                        borderJoinStyle: 'miter',
                        pointBorderColor: "rgba(75,192,192,1)",
                        pointBackgroundColor: "#fff",
                        pointBorderWidth: 1,
                        pointHoverRadius: 5,
                        pointHoverBackgroundColor: "rgba(75,192,192,1)",
                        pointHoverBorderColor: "rgba(220,220,220,1)",
                        pointHoverBorderWidth: 2,
                        pointRadius: 1,
                        pointHitRadius: 10,
                        data: transactions, // ALL THE VALUES FOR EACH DATE
                        spanGaps: false,
                    },
                    {
                        label: "Antal oprettelser",
                        fill: true,
                        lineTension: 0.1,
                        backgroundColor: "rgba(13,24,192,0.4)",
                        borderColor: "rgba(13,24,135,1)",
                        borderCapStyle: 'butt',
                        borderDash: [],
                        borderDashOffset: 0.0,
                        borderJoinStyle: 'miter',
                        pointBorderColor: "rgba(75,192,192,1)",
                        pointBackgroundColor: "#fff",
                        pointBorderWidth: 1,
                        pointHoverRadius: 5,
                        pointHoverBackgroundColor: "rgba(75,192,192,1)",
                        pointHoverBorderColor: "rgba(220,220,220,1)",
                        pointHoverBorderWidth: 2,
                        pointRadius: 1,
                        pointHitRadius: 10,
                        data: subscriptions, // ALL THE VALUES FOR EACH DATE
                        spanGaps: false,
                    },
                    {
                        label: "Aktivitet",
                        fill: true,
                        lineTension: 0.1,
                        backgroundColor: "rgba(133,185,228,0.4)",
                        borderColor: "rgba(133,185,228,1)",
                        borderCapStyle: 'butt',
                        borderDash: [],
                        borderDashOffset: 0.0,
                        borderJoinStyle: 'miter',
                        pointBorderColor: "rgba(75,192,192,1)",
                        pointBackgroundColor: "#fff",
                        pointBorderWidth: 1,
                        pointHoverRadius: 5,
                        pointHoverBackgroundColor: "rgba(75,192,192,1)",
                        pointHoverBorderColor: "rgba(220,220,220,1)",
                        pointHoverBorderWidth: 2,
                        pointRadius: 1,
                        pointHitRadius: 10,
                        data: activity, // ALL THE VALUES FOR EACH DATE
                        spanGaps: false,
                    }
                ]
            };
            // CREATE NEW CHARTJS LINECHART
            modalLineGraph = new Chart(modalTransactionsGraph, {
                type: 'line',
                data: graphData,
                options: {
                    responsive: true
                }
            });
        };
        createLineGraph();
    }
    self.activityCurrentQuarter = self.transactionsCurrentQuarter + self.subscriptionsCurrentQuarter;

}])
app.controller("tradeUnionShopsCtrl", ["$scope", "sessionStoreFactory", function ($scope, sessionStoreFactory) {
    var self = this;

    //end loading gif and display content
    $(".mainContent").css("-webkit-filter", "blur(0px)");
    $(".mainContent").css("filter", "blur(0px)");
    $("#loading").css("display", "none");

    var currentOndo                     = sessionStoreFactory.load("currentOndo");
    self.data                           = sessionStoreFactory.loadJSON("Data:" + currentOndo);

    self.pointsCurrentQuarter           = self.data.pointsQuarter;
    self.pointsCurrentWeek              = self.data.pointsWeekShops;
    self.transactionsCurrentQuarter     = self.data.transactionsQuarter;
    self.transactionsCurrentWeek        = self.data.transactionsWeekShops;
    self.subscriptionsCurrentQuarter    = self.data.subscriptionsQuarterShops;
    self.subscriptionsCurrentWeek       = self.data.subscriptionsWeekShops;
    self.activityCurrentQuarter         = self.data.activityQuarterShops;
    self.activityCurrentWeek            = self.data.activityWeekShops;
    self.ondoTransactionsData           = self.data.shops;
    self.allOndoDataList                = [];


    //Filter variables;
    self.categoryInput                  = "";
    self.categoryDropDown;

    var modalLineGraph;

    //add default filter
    self.categories                     = [{ title: "Alle kategorier", value: "" }];


    //Adds the unique categories to another array which has objects of title, value so that there is the possibility to choose all 
    for (var i = 0; i < self.data.shops.length; i++) {
        var category = self.data.shops[i].category;
        if (self.categories.indexOf(category) == -1) {
            self.categories.push({ title: self.data.shops[i].category, value: self.data.shops[i].category });
        }
    }

    self.viewOndo = {
        "title": "Indlæser",
        "locality": ""
    };

    //sort function for table
    $scope.propertyName = 'pointsQuarter';
    $scope.reverse = false;

    $scope.sortBy = function (propertyName) {
        $scope.reverse = ($scope.propertyName === propertyName) ? !$scope.reverse : false;
        $scope.propertyName = propertyName;
    };

    //Get information about shop for modal
    self.getAllOndoData = function (event) {

        self.viewOndo = event.ondo;

        var modalGraph = document.getElementById("modalGraph");

        var createLineGraph = function (dateTransactions) {
            //REMOVES THE CHART IF ALREADY EXIST TO INSURE CORRECT UPDATE
            if (modalLineGraph != null) {
                modalLineGraph.destroy();
            }

            //TAKES THE DATE LABELS AND CONVERTS TO DD-MM-YYYY
            var labels = [];
            var transactions = [];
            var subscriptions = [];
            var activity = [];

            for (var i = 0; i < self.viewOndo.weeksArray.length; i++) {
                labels.push(self.viewOndo.weeksArray[i].weekLabel);
                transactions.push(self.viewOndo.weeksArray[i].transactions);
                subscriptions.push(self.viewOndo.weeksArray[i].subscriptions);
                activity.push(self.viewOndo.weeksArray[i].activity);
            }

            //GLOBAL CHANGES TO CHART
            Chart.defaults.global.defaultFontColor = '#000';
            Chart.defaults.global.scaleBeginAtZero = true;
            Chart.defaults.global.barBeginAtOrigin = false;
            Chart.defaults.global.defaultFontFamily = "'Dosis', sans-serif";

            // GET GRAPH DOM ELEMENT
            var modalTransactionsGraph = document.getElementById("modal-line-graph");

            //DATA OBJECT
            var graphData = {
                labels: labels, // ALL THE NAMES OF THE DATES
                datasets: [
                    {
                        label: "Antal transaktioner",
                        fill: true,
                        lineTension: 0.1,
                        backgroundColor: "rgba(97,147,189,0.4)",
                        borderColor: "rgba(97,147,189,1)",
                        borderCapStyle: 'butt',
                        borderDash: [],
                        borderDashOffset: 0.0,
                        borderJoinStyle: 'miter',
                        pointBorderColor: "rgba(75,192,192,1)",
                        pointBackgroundColor: "#fff",
                        pointBorderWidth: 1,
                        pointHoverRadius: 5,
                        pointHoverBackgroundColor: "rgba(75,192,192,1)",
                        pointHoverBorderColor: "rgba(220,220,220,1)",
                        pointHoverBorderWidth: 2,
                        pointRadius: 1,
                        pointHitRadius: 10,
                        data: transactions, // ALL THE VALUES FOR EACH DATE
                        spanGaps: false,
                    },
                    {
                        label: "Antal oprettelser",
                        fill: true,
                        lineTension: 0.1,
                        backgroundColor: "rgba(13,24,192,0.4)",
                        borderColor: "rgba(13,24,135,1)",
                        borderCapStyle: 'butt',
                        borderDash: [],
                        borderDashOffset: 0.0,
                        borderJoinStyle: 'miter',
                        pointBorderColor: "rgba(75,192,192,1)",
                        pointBackgroundColor: "#fff",
                        pointBorderWidth: 1,
                        pointHoverRadius: 5,
                        pointHoverBackgroundColor: "rgba(75,192,192,1)",
                        pointHoverBorderColor: "rgba(220,220,220,1)",
                        pointHoverBorderWidth: 2,
                        pointRadius: 1,
                        pointHitRadius: 10,
                        data: subscriptions, // ALL THE VALUES FOR EACH DATE
                        spanGaps: false,
                    },
                    {
                        label: "Aktivitet",
                        fill: true,
                        lineTension: 0.1,
                        backgroundColor: "rgba(133,185,228,0.4)",
                        borderColor: "rgba(133,185,228,1)",
                        borderCapStyle: 'butt',
                        borderDash: [],
                        borderDashOffset: 0.0,
                        borderJoinStyle: 'miter',
                        pointBorderColor: "rgba(75,192,192,1)",
                        pointBackgroundColor: "#fff",
                        pointBorderWidth: 1,
                        pointHoverRadius: 5,
                        pointHoverBackgroundColor: "rgba(75,192,192,1)",
                        pointHoverBorderColor: "rgba(220,220,220,1)",
                        pointHoverBorderWidth: 2,
                        pointRadius: 1,
                        pointHitRadius: 10,
                        data: activity, // ALL THE VALUES FOR EACH DATE
                        spanGaps: false,
                    }
                ]
            };
            // CREATE NEW CHARTJS LINECHART
            modalLineGraph = new Chart(modalTransactionsGraph, {
                type: 'line',
                data: graphData,
                options: {
                    responsive: true
                }
            });
        };
        createLineGraph();

        //end loading gif and display content
        $(".mainContent").css("-webkit-filter", "blur(0px)");
        $(".mainContent").css("filter", "blur(0px)");
        $("#loading").css("display", "none");

    }

    //print table function
    self.printDataShop = function () {
        var divToPrint = document.getElementById("tradeunionShopTableSection");
        newWin = window.open("");
        newWin.document.write(divToPrint.outerHTML);
        newWin.print();
        newWin.close();
    };
}])
app.controller("tradeUnionSClubsCtrl", ["$scope", "sessionStoreFactory", function ($scope, sessionStoreFactory) {
    var self = this;

    var ondo = sessionStoreFactory.load("currentOndo");
    self.data = sessionStoreFactory.loadJSON("Data:" + ondo);

    self.points = self.data.pointsToClubs;
    self.unassignedPoints = self.data.pointsNoClubs;
    self.activeUsers = self.data.activeCardUsersQuarterInCity;
    self.inactiveUsers = self.data.inactiveCardUsersQuarterInCity;
    self.subscriptions = self.data.subscriptionsQuarterClubs;
    self.clubCount = self.data.clubs.length;
    self.appUsers = self.data.appUsersQuarterInCity;
    self.users = self.data.cardUsersQuarterInCity;
    self.unassignedPoints = self.data.pointsNoClubs;
    self.clubArray = self.data.clubs;

    //Variable to search in table
    self.search = "";

    //sort function for table
    $scope.propertyName = 'points';
    $scope.reverse = false;

    $scope.sortBy = function (propertyName) {
        $scope.reverse = ($scope.propertyName === propertyName) ? !$scope.reverse : false;
        $scope.propertyName = propertyName;
    };

    //print table function
    self.printDataClub = function () {
        var divToPrint = document.getElementById("tradeunionClubTableSection");
        newWin = window.open("");
        newWin.document.write(divToPrint.outerHTML);
        newWin.print();
        newWin.close();
    };
}])
app.controller("tradeUnionSubscriptionsCtrl", ["sessionStoreFactory", function (sessionStoreFactory) {
    var self = this;

    var ondo = sessionStoreFactory.load("currentOndo");
    self.data = sessionStoreFactory.loadJSON("Data:" + ondo);
    self.period = "Dette kvartal";

    //end loading gif and display content
    $(".mainContent").css("-webkit-filter", "blur(0px)");
    $(".mainContent").css("filter", "blur(0px)");
    $("#loading").css("display", "none");

    var subscriptionsShopsBarChart = document.getElementById("bar-subscriptions-shops");
    var subscriptionsShopsBarChartSmall = document.getElementById("bar-subscriptions-shops-small");

    //GLOBAL CHANGES TO CHART
    Chart.defaults.global.defaultFontColor = '#000';
    Chart.defaults.global.scaleBeginAtZero = true;
    Chart.defaults.global.barBeginAtOrigin = false;

    var createBarChart = function () {
        var labels = [];
        var subscriptions = [];

        //add labes and points to two arrays to be used in the chart
        for (var i = 0; i < self.data.shops.length; i++) {
            labels.push(self.data.shops[i].title);
            subscriptions.push(self.data.shops[i].subscriptions);
        }

        //create bar chart over transactions
        myChartSubscriptions = new Chart(subscriptionsShopsBarChart, {
            type: 'bar',
            data: {
                labels: labels,
                fontColor: '#FFFFFF',
                datasets: [{
                    data: subscriptions,
                    backgroundColor: '#27b371',
                    borderColor: '#27b371',
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                scaleShowVerticalLines: false,
                legend: {
                    display: false
                },
                hover: {
                    mode: 'x-axis'
                },
                tooltips: {
                    mode: 'x-axis'
                },
                scales: {
                    yAxes: [{
                        id: 'y-axis-0',
                        gridLines: {
                            display: true,
                            lineWidth: 1,
                            color: "rgba(0,0,0,0.30)"
                        },
                        ticks: {
                            beginAtZero: true,
                            mirror: false,
                            suggestedMin: 0
                        },
                        afterBuildTicks: function (chart) {

                        }
                    }],
                    xAxes: [{
                        id: 'x-axis-0',
                        gridLines: {
                            display: false
                        },
                        ticks: {
                            beginAtZero: true,
                            autoSkip: false
                        }
                    }]
                }
            }
        });

        //create bar chart over transactions
        myChartSubscriptions = new Chart(subscriptionsShopsBarChartSmall, {
            type: 'horizontalBar',
            data: {
                labels: labels,
                fontColor: '#FFFFFF',
                datasets: [{
                    data: subscriptions,
                    backgroundColor: '#27b371',
                    borderColor: '#27b371',
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                scaleShowVerticalLines: false,
                legend: {
                    display: false
                },
                hover: {
                    mode: 'x-axis'
                },
                tooltips: {
                    mode: 'x-axis'
                },
                scales: {
                    yAxes: [{
                        id: 'y-axis-0',
                        gridLines: {
                            display: true,
                            lineWidth: 1,
                            color: "rgba(0,0,0,0.30)"
                        },
                        ticks: {
                            beginAtZero: true,
                            mirror: false,
                            suggestedMin: 0
                        },
                        afterBuildTicks: function (chart) {

                        }
                    }],
                    xAxes: [{
                        id: 'x-axis-0',
                        gridLines: {
                            display: false
                        },
                        ticks: {
                            beginAtZero: true,
                            autoSkip: false
                        }
                    }]
                }
            }
        });
    };

    createBarChart();
}])
app.controller("tradeUnionTransactionsCtrl", ["sessionStoreFactory", function (sessionStoreFactory) {
    var self = this;

    var ondo = sessionStoreFactory.load("currentOndo");
    self.data = sessionStoreFactory.loadJSON("Data:" + ondo);
    self.period = "Dette kvartal";

    //end loading gif and display content
    $(".mainContent").css("-webkit-filter", "blur(0px)");
    $(".mainContent").css("filter", "blur(0px)");
    $("#loading").css("display", "none");

    var transactionsShopsBarChart = document.getElementById("bar-transactions-shops");
    var transactionsShopsBarChartSmall = document.getElementById("bar-transactions-shops-sm");

    //GLOBAL CHANGES TO CHART
    Chart.defaults.global.defaultFontColor = '#000';
    Chart.defaults.global.scaleBeginAtZero = true;
    Chart.defaults.global.barBeginAtOrigin = false;

    var createBarChart = function () {
        var labels = [];
        var transactions = [];

        //add labes and points to two arrays to be used in the chart
        for (var i = 0; i < self.data.shops.length; i++) {
            labels.push(self.data.shops[i].title);
            transactions.push(self.data.shops[i].transactions);
        }

        //create bar chart over transactions
        myChartSubscriptions = new Chart(transactionsShopsBarChart, {
            type: 'bar',
            data: {
                labels: labels,
                fontColor: '#FFFFFF',
                datasets: [{
                    data: transactions,
                    backgroundColor: '#27b371',
                    borderColor: '#27b371',
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                scaleShowVerticalLines: false,
                legend: {
                    display: false
                },
                hover: {
                    mode: 'x-axis'
                },
                tooltips: {
                    mode: 'x-axis'
                },
                scales: {
                    yAxes: [{
                        id: 'y-axis-0',
                        gridLines: {
                            display: true,
                            lineWidth: 1,
                            color: "rgba(0,0,0,0.30)"
                        },
                        ticks: {
                            beginAtZero: true,
                            mirror: false,
                            suggestedMin: 0
                        },
                        afterBuildTicks: function (chart) {

                        }
                    }],
                    xAxes: [{
                        id: 'x-axis-0',
                        gridLines: {
                            display: false
                        },
                        ticks: {
                            beginAtZero: true,
                            autoSkip: false
                        }
                    }]
                }
            }
        });

        //create bar chart over transactions
        myChartSubscriptions = new Chart(transactionsShopsBarChartSmall, {
            type: 'horizontalBar',
            data: {
                labels: labels,
                fontColor: '#FFFFFF',
                datasets: [{
                    data: transactions,
                    backgroundColor: '#27b371',
                    borderColor: '#27b371',
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                scaleShowVerticalLines: false,
                legend: {
                    display: false
                },
                hover: {
                    mode: 'x-axis'
                },
                tooltips: {
                    mode: 'x-axis'
                },
                scales: {
                    yAxes: [{
                        id: 'y-axis-0',
                        gridLines: {
                            display: true,
                            lineWidth: 1,
                            color: "rgba(0,0,0,0.30)"
                        },
                        ticks: {
                            beginAtZero: true,
                            mirror: false,
                            suggestedMin: 0
                        },
                        afterBuildTicks: function (chart) {

                        }
                    }],
                    xAxes: [{
                        id: 'x-axis-0',
                        gridLines: {
                            display: false
                        },
                        ticks: {
                            beginAtZero: true,
                            autoSkip: false
                        }
                    }]
                }
            }
        });
    };

    createBarChart();
}])
app.controller("tradeUnionHistoryCtrl", ["$scope", "sessionStoreFactory", function ($scope, sessionStoreFactory) {
    var self = this;

    var myChartHistory;
    var modalLineGraphHistory;

    var allDataForQuarter = [];
    var shopDataFroQuarter = [];
    var allTransactionsInPeriod = [];
    var quarterDataBarChart = document.getElementById("bar-transactions-history");
    var uniqueCategoriesArray = [];

    var ondo = sessionStoreFactory.load("currentOndo");
    self.data = sessionStoreFactory.loadJSON("Data:" + ondo);


    self.quarter = "";
    self.quarters = [];
    self.quartersSelected = [];
    self.quarterSelected = {};
    self.categories = [{ title: "Alle kategorier", value: "" }];
    self.ondoTransactionsData = [];
    self.categoryDropDown = [];
    self.allOndoDataList = [];

    //Filter variables;
    self.categoryInput = "";
    self.categoryDropDown;

    //populate quarters select array
    for (var i = 0; i < self.data.history.length; i++) {
        self.quarters.push(self.data.history[i].quarterLabel);
    }

    //check for selected items in quarter selector
    var checkForOptionsSelected = function () {

        var optionsSelected = [];
        var sel = document.getElementById('quarterSelect');
        var opt = sel && sel.options

        //populate array with quarters selected
        for (var i = 0; i < opt.length; i++) {
            var option = opt[i];

            if (option.selected) {
                optionsSelected.push(option.value || option.text);
            }
        };
        return optionsSelected.length;
    };

    //selector function for multiselect quarters select
    $("#quarterSelect").mousedown(function (e) {
        e.preventDefault();

        var select = this;
        var scroll = select.scrollTop;

        e.target.selected = !e.target.selected;

        setTimeout(function () { select.scrollTop = scroll; }, 0);

        $(select).focus();


        //check if any option is selected to enable the get data button
        if (checkForOptionsSelected() > 0) {
            $("#getHistoryDataBtn").removeAttr("disabled");
        } else {
            $("#getHistoryDataBtn").attr("disabled", "disabled");
        }

        //check for options selected
    }).mousemove(function (e) { e.preventDefault() });

    //create chart function
    var createBarChart = function () {
        //if chart exist then destroy
        if (myChartHistory != null) {
            myChartHistory.destroy();
        }

        var labels = [];
        var points = [];
        var subscriptions = [];
        var transactions = [];


        //add labes and points to two arrays to be used in the chart
        for (var i = 0; i < self.quartersSelected.length; i++) {
            labels.push(allDataForQuarter[i].label);
            points.push(allDataForQuarter[i].points);
            subscriptions.push(allDataForQuarter[i].subscriptions);
            transactions.push(allDataForQuarter[i].transactions);
        }

        //GLOBAL CHANGES TO CHART
        Chart.defaults.global.defaultFontColor = '#000';
        Chart.defaults.global.scaleBeginAtZero = true;
        Chart.defaults.global.barBeginAtOrigin = false;
        Chart.defaults.global.defaultFontFamily = "'Dosis', sans-serif";
        Chart.defaults.global.legend.display = true;
        var barGraphValueColor = "#000";
        var gridColor = "#F3F3F3"

        //create bar chart over transactions
        myChartHistory = new Chart(quarterDataBarChart, {
            type: 'bar',
            data: {
                labels: labels.reverse(),
                fontColor: '#FFFFFF',
                datasets: [{
                    label: "Oprettelser",
                    data: subscriptions.reverse(),
                    backgroundColor: '#c74658',
                    borderColor: '#c74658',
                    borderWidth: 1
                },
                {
                    label: "Transaktioner",
                    data: transactions.reverse(),
                    backgroundColor: '#5cabca',
                    borderColor: '#5cabca',
                    borderWidth: 1
                },
                {
                    label: "Point",
                    data: points.reverse(),
                    backgroundColor: '#ec9a1f',
                    borderColor: '#ec9a1f',
                    borderWidth: 1
                }]
            },
            options: {
                tooltips: {
                    enabled: false
                },
                responsive: true,
                scaleShowVerticalLines: false,
                legend: {
                    display: true
                },
                hover: {
                    mode: 'x-axis'
                },
                tooltips: {
                    mode: 'x-axis'
                },
                scales: {
                    yAxes: [{
                        id: 'y-axis-0',
                        gridLines: {
                            display: true,
                            lineWidth: 1,
                            color: "rgba(0,0,0,0.30)"
                        },
                        ticks: {
                            showTooltips: false,
                            beginAtZero: true,
                            mirror: false,
                            suggestedMin: 0
                        },
                        afterBuildTicks: function (chart) {

                        }
                    }],
                    xAxes: [{
                        id: 'x-axis-0',
                        gridLines: {
                            display: false
                        },
                        ticks: {
                            beginAtZero: true,
                            autoSkip: false
                        }
                    }]
                },
                hover: {
                    animationDuration: 0
                },
                animation: {
                    onComplete: function () {
                        var chartInstance = this.chart;
                        var ctx = chartInstance.ctx;
                        ctx.textAlign = "center";
                        ctx.fillStyle = barGraphValueColor;
                        Chart.helpers.each(this.data.datasets.forEach(function (dataset, i) {
                            var meta = chartInstance.controller.getDatasetMeta(i);
                            Chart.helpers.each(meta.data.forEach(function (bar, index) {
                                ctx.fillText(dataset.data[index] + "", bar._model.x, bar._model.y - 20);
                            }), this)
                        }), this);
                    }
                }
            }
        });
    };
    createBarChart();

    //get data for quarters selected
    var getData = function (ondoId, fromDate, toDate) {
        //empty all data for quarter array
        allDataForQuarter = [];

        for (var h = 0; h < self.quartersSelected.length; h++) {
            //data variables
            var transactions = 0;
            var subscriptions = 0;
            var points = 0;

            //empty categories array
            self.categories = [{ title: "Alle kategorier", value: "" }];

            //quarter and year number from selected quarter
            var quarterNo = self.quartersSelected[h].substring(0, 1);
            var quarterYear = self.quartersSelected[h].substring(11, 15);

            for (var i = 0; i < self.data.shops.length; i++) {
                //Adds categories to an array where category is unique.
                if (uniqueCategoriesArray.indexOf(self.data.shops[i].category) === -1) {
                    uniqueCategoriesArray.push(self.data.shops[i].category);
                }

                for (var j = 0; j < self.data.history.length; j++) {
                    var quarterLabel = quarterNo + ". Kvartal " + quarterYear;
                    if (self.data.history[j].quarterLabel == quarterLabel) {
                        subscriptions = self.data.history[j].subscriptions;
                        transactions = self.data.history[j].transactions;
                        points = self.data.history[j].points;
                    }
                }
            }
            //Adds the unique categories to another array which has objects of title, value so that there is the possibility to choose all 
            for (var i = 0; i < uniqueCategoriesArray.length; i++) {
                self.categories.push({ title: uniqueCategoriesArray[i], value: uniqueCategoriesArray[i] });
            }
            allDataForQuarter.push({ label: self.quartersSelected[h], subscriptions: subscriptions, transactions: transactions, points: points });

            //if more than one quarter is selected then insert quarter for shop selector
            if (self.quartersSelected.length > 1) {
                $("#quarterForShopDropDown").css("display", "block");
            } else {
                //quarter and year number from selected quarter
                var quarterno = self.quartersSelected[0].substring(0, 1);
                var quarteryear = self.quartersSelected[0].substring(11, 15);

                populateShopTabel(quarterno, quarteryear);
            };
        }
        createBarChart();

        //end loading gif and display content
        $(".mainContent").css("-webkit-filter", "blur(0px)");
        $(".mainContent").css("filter", "blur(0px)");
        $("#loading").css("display", "none");
    };

    // get history data function, if cached then retrieve from storage else retrive from api
    self.getHistoryData = function () {

        //start loading gif
        $(".mainContent").css("-webkit-filter", "blur(10px)");
        $(".mainContent").css("filter", "blur(10px)");
        $("#loading").css("display", "block");

        self.quartersSelected = [];
        var sel = document.getElementById('quarterSelect');
        var opt = sel && sel.options
        for (var i = 0; i < opt.length; i++) {
            var option = opt[i];

            if (option.selected) {
                self.quartersSelected.push(option.value || option.text);
            }
        };

        //empty categories array
        self.categories = [{ title: "Alle kategorier", value: "" }];

        //empty all data for quarter array
        allDataForQuarter = [];

        for (var h = 0; h < self.quartersSelected.length; h++) {
            //data variables
            var transactions = 0;
            var subscriptions = 0;
            var points = 0;

            //quarter and year number from selected quarter
            var quarterNo = self.quartersSelected[h].substring(0, 1);
            var quarterYear = self.quartersSelected[h].substring(11, 15);

            for (var i = 0; i < self.data.shops.length; i++) {

                //Adds categories to an array where category is unique.
                if (uniqueCategoriesArray.indexOf(self.data.shops[i].category) === -1) {
                    uniqueCategoriesArray.push(self.data.shops[i].category);
                }

                for (var j = 0; j < self.data.history.length; j++) {
                    var quarterLabel = quarterNo + ". Kvartal " + quarterYear;
                    if (self.data.history[j].quarterLabel == quarterLabel) {
                        subscriptions = self.data.history[j].subscriptions;
                        transactions = self.data.history[j].transactions;
                        points = self.data.history[j].points;
                    }
                }
                
            }
            //Adds the unique categories to another array which has objects of title, value so that there is the possibility to choose all 
            for (var i = 0; i < uniqueCategoriesArray.length; i++) {
                self.categories.push({ title: uniqueCategoriesArray[i], value: uniqueCategoriesArray[i] });
            }
            allDataForQuarter.push({ label: self.quartersSelected[h], subscriptions: subscriptions, transactions: transactions, points: points });

            //if more than one quarter is selected then insert quarter for shop selector
            if (self.quartersSelected.length > 1) {
                $("#quarterForShopDropDown").css("display", "block");
            } else {
                //quarter and year number from selected quarter
                var quarterno = self.quartersSelected[0].substring(0, 1);
                var quarteryear = self.quartersSelected[0].substring(11, 15);

                populateShopTabel(quarterno, quarteryear);
            };

        }
        createBarChart();

        //end loading gif and display content
        $(".mainContent").css("-webkit-filter", "blur(0px)");
        $(".mainContent").css("filter", "blur(0px)");
        $("#loading").css("display", "none");
    };


    //populate shop table function
    var populateShopTabel = function (quarterNo, year) {
        //empty list before population
        self.ondoTransactionsData = [];

        var quarterLabel = quarterNo + ". Kvartal " + year;

        for (var i = 0; i < self.data.history.length; i++) {
            var quarterData = [];
            if (self.data.history[i].quarterLabel) {
                quarterData = self.data.history[i].shops;
            }
            for (var j = 0; j < quarterData.length; j++) {
                self.ondoTransactionsData.push(quarterData[j]);
            }
        }
    }

    //Get information about shop for modal
    self.getAllOndoData = function (event) {

        //if quarterNo and quarteryear is not set then set from self.quarterSelected
        if (self.quarterSelected.quarterNo == null || self.quarterSelected.quarterYear == null) {
            self.quarterSelected.quarterNo = self.quartersSelected[0].substring(0, 1)
            self.quarterSelected.quarterYear = self.quartersSelected[0].substring(11, 15);
        }

        //get ondo title from click event
        var ondoId = event.ondo.title;

        var quarterNo = self.quarterSelected.quarterNo;
        var quarterYear = self.quarterSelected.quarterYear;

        self.allOndoDataList.push(event.ondo);
        self.viewOndo = event.ondo;

        var createLineGraph = function () {
            
            //REMOVES THE CHART IF ALREADY EXIST TO INSURE CORRECT UPDATE
            if (modalLineGraphHistory != null) {
                console.log("Destroy")
                modalLineGraphHistory.destroy();
            }

            //TAKES THE DATE LABELS AND CONVERTS TO DD-MM-YYYY
            var labels = [];
            var transactions = [];
            var subscriptions = [];
            var activity = [];

            for (var i = 0; i < self.viewOndo.weeksArray.length; i++) {
                labels.push(self.viewOndo.weeksArray[i].weekLabel);
                transactions.push(self.viewOndo.weeksArray[i].transactions);
                subscriptions.push(self.viewOndo.weeksArray[i].subscriptions);
                activity.push(self.viewOndo.weeksArray[i].activity);
            }

            //GLOBAL CHANGES TO CHART
            Chart.defaults.global.defaultFontColor = '#000';
            Chart.defaults.global.scaleBeginAtZero = true;
            Chart.defaults.global.barBeginAtOrigin = false;
            Chart.defaults.global.defaultFontFamily = "'Dosis', sans-serif";

            // GET GRAPH DOM ELEMENT
            var modalTransactionsGraph = document.getElementById("modal-line-graph-history");

            //DATA OBJECT
            var graphData = {
                labels: labels, // ALL THE NAMES OF THE DATES
                datasets: [
                    {
                        label: "Antal transaktioner",
                        fill: true,
                        lineTension: 0.1,
                        backgroundColor: "rgba(97,147,189,0.4)",
                        borderColor: "rgba(97,147,189,1)",
                        borderCapStyle: 'butt',
                        borderDash: [],
                        borderDashOffset: 0.0,
                        borderJoinStyle: 'miter',
                        pointBorderColor: "rgba(75,192,192,1)",
                        pointBackgroundColor: "#fff",
                        pointBorderWidth: 1,
                        pointHoverRadius: 5,
                        pointHoverBackgroundColor: "rgba(75,192,192,1)",
                        pointHoverBorderColor: "rgba(220,220,220,1)",
                        pointHoverBorderWidth: 2,
                        pointRadius: 1,
                        pointHitRadius: 10,
                        data: transactions, // ALL THE VALUES FOR EACH DATE
                        spanGaps: false,
                    },
                    {
                        label: "Antal oprettelser",
                        fill: true,
                        lineTension: 0.1,
                        backgroundColor: "rgba(13,24,192,0.4)",
                        borderColor: "rgba(13,24,135,1)",
                        borderCapStyle: 'butt',
                        borderDash: [],
                        borderDashOffset: 0.0,
                        borderJoinStyle: 'miter',
                        pointBorderColor: "rgba(75,192,192,1)",
                        pointBackgroundColor: "#fff",
                        pointBorderWidth: 1,
                        pointHoverRadius: 5,
                        pointHoverBackgroundColor: "rgba(75,192,192,1)",
                        pointHoverBorderColor: "rgba(220,220,220,1)",
                        pointHoverBorderWidth: 2,
                        pointRadius: 1,
                        pointHitRadius: 10,
                        data: subscriptions, // ALL THE VALUES FOR EACH DATE
                        spanGaps: false,
                    },
                    {
                        label: "Aktivitet",
                        fill: true,
                        lineTension: 0.1,
                        backgroundColor: "rgba(133,185,228,0.4)",
                        borderColor: "rgba(133,185,228,1)",
                        borderCapStyle: 'butt',
                        borderDash: [],
                        borderDashOffset: 0.0,
                        borderJoinStyle: 'miter',
                        pointBorderColor: "rgba(75,192,192,1)",
                        pointBackgroundColor: "#fff",
                        pointBorderWidth: 1,
                        pointHoverRadius: 5,
                        pointHoverBackgroundColor: "rgba(75,192,192,1)",
                        pointHoverBorderColor: "rgba(220,220,220,1)",
                        pointHoverBorderWidth: 2,
                        pointRadius: 1,
                        pointHitRadius: 10,
                        data: activity, // ALL THE VALUES FOR EACH DATE
                        spanGaps: false,
                    }
                ]
            };
            // CREATE NEW CHARTJS LINECHART
            modalLineGraphHistory = new Chart(modalTransactionsGraph, {
                type: 'line',
                data: graphData,
                options: {
                    responsive: true
                }
            });
        };

        createLineGraph();

        //end loading gif and display content
        $(".mainContent").css("-webkit-filter", "blur(0px)");
        $(".mainContent").css("filter", "blur(0px)");
        $("#loading").css("display", "none");

    }

    //quarters for shop dropdown click function
    $('#quarterForShopDropDown').on('change', function () {
        console.log(this);

        //quarter and year number from selected quarter
        var quarterNo = this.value.substring(7, 8);
        var quarterYear = this.value.substring(18, 22);

        self.quarterSelected = { quarterNo: quarterNo, quarterYear: quarterYear };

        populateShopTabel(quarterNo, quarterYear);
    });

    //sort function for table
    $scope.propertyName = 'pointsQuarter';
    $scope.reverse = false;

    $scope.sortBy = function (propertyName) {
        $scope.reverse = ($scope.propertyName === propertyName) ? !$scope.reverse : false;
        $scope.propertyName = propertyName;
    };
}])
app.controller("tradeUnionUserHistoryCtrl", ["$scope", "sessionStoreFactory", function ($scope, sessionStoreFactory) {
    var self = this;

    var ondo = sessionStoreFactory.load("currentOndo");
    self.data = sessionStoreFactory.loadJSON("Data:" + ondo);

    self.userInformationArray = []
    $scope.period = [];
    self.periodLength = 0;
    self.categoryDropDown;
    self.userArray = [];

    var ondoId = sessionStorage.getItem("currentOndo");
    var labelsArray = [];

    var userInformationLine = {};
    userInformationLine.activeUsers = [];
    userInformationLine.inactiveUsers = [];
    userInformationLine.appUsers = [];
    userInformationLine.userWithoutClub = [];

    var userInformationBar = {};
    userInformationBar.users = [];
    userInformationBar.activeUsers = [];
    userInformationBar.inactiveUSers = [];
    userInformationBar.appUsers = [];
    userInformationBar.usersWithoutClub = [];
    var userHistoryLineChart = document.getElementById("line-users-history");
    var myChartLineUserHistory;
    var userHistoryBarChart = document.getElementById("bar-users-history");
    var myChartBarUserHistory;

    var information = [];

    // populate arrays with data
    for (var i = 0; i < self.data.userHistory.length; i++) {
        labelsArray.push(self.data.userHistory[i].quarterLabel);

        userInformationLine.activeUsers.push(self.data.userHistory[i].activeCardUsers);
        userInformationLine.inactiveUsers.push(self.data.userHistory[i].inactiveCardUsers);
        userInformationLine.appUsers.push(self.data.userHistory[i].appUsers);
        userInformationLine.userWithoutClub.push(self.data.userHistory[i].cardUsersWithNoClub);

        userInformationBar.users.push(self.data.userHistory[i].cardUsers);
        userInformationBar.activeUsers.push(self.data.userHistory[i].activeCardUsers);
        userInformationBar.inactiveUSers.push(self.data.userHistory[i].inactiveCardUsers);
        userInformationBar.appUsers.push(self.data.userHistory[i].appUsers);
        userInformationBar.usersWithoutClub.push(self.data.userHistory[i].cardUsersWithNoClub);

        information.push({ title: "Total antal kort brugere", number: self.data.userHistory[i].cardUsers, percentage: 100 });
        information.push({ title: "Antal aktive kort brugere", number: self.data.userHistory[i].activeCardUsers, percentage: self.data.userHistory[i].activeCardUsersPercent });
        information.push({ title: "Antal inaktive kort brugere", number: self.data.userHistory[i].inactiveCardUsers, percentage: self.data.userHistory[i].inactiveCardUsersPercent });
        information.push({ title: "Antal app brugere med kort", number: self.data.userHistory[i].appUsers, percentage: self.data.userHistory[i].appUsersPercent });
        information.push({ title: "Antal kort brugere uden valgt klub", number: self.data.userHistory[i].cardUsersWithNoClub, percentage: self.data.userHistory[i].cardUsersWithNoClubPercent });
        self.userArray.push(information);

        $scope.period.push({ title: self.data.userHistory[i].quarterLabel, value: self.data.userHistory[i].quarterLabel });
    }
    self.periodLength = $scope.period.length - 1;

    $("#userHistoryDropdown").on('change', function (event) {
        console.log(this);
    })


    var createLineGraph = function () {
        if (myChartLineUserHistory != null) {
            myChartLineUserHistory.destroy();
        }

        //create line chart over user history
        myChartLineUserHistory = new Chart(userHistoryLineChart, {
            type: 'line',
            data: {
                labels: labelsArray,
                fontColor: '#FFFFFF',
                datasets: [{
                    label: "Aktive brugere",
                    data: userInformationLine.activeUsers.reverse(),
                    fill: true,
                    lineTension: 0.1,
                    backgroundColor: "rgba(82,180,109,0.4)",
                    borderColor: "rgba(82,180,109, 1)",
                    borderCapStyle: 'butt',
                    borderDash: [],
                    borderDashOffset: 0.0,
                    borderJoinStyle: 'miter',
                    pointBorderColor: "rgba(75,192,192,1)",
                    pointBackgroundColor: "#fff",
                    pointBorderWidth: 1,
                    pointHoverRadius: 5,
                    pointHoverBackgroundColor: "rgba(75,192,192,1)",
                    pointHoverBorderColor: "rgba(220,220,220,1)",
                    pointHoverBorderWidth: 2,
                    pointRadius: 1,
                    pointHitRadius: 10,
                    spanGapsspanGaps: false,
                },
                {
                    label: "Inaktive brugere",
                    data: userInformationLine.inactiveUsers.reverse(),
                    fill: true,
                    lineTension: 0.1,
                    backgroundColor: "rgba(92,171,202,0.4)",
                    borderColor: "rgba(92,171,202,1)",
                    borderCapStyle: 'butt',
                    borderDash: [],
                    borderDashOffset: 0.0,
                    borderJoinStyle: 'miter',
                    pointBorderColor: "rgba(75,192,192,1)",
                    pointBackgroundColor: "#fff",
                    pointBorderWidth: 1,
                    pointHoverRadius: 5,
                    pointHoverBackgroundColor: "rgba(75,192,192,1)",
                    pointHoverBorderColor: "rgba(220,220,220,1)",
                    pointHoverBorderWidth: 2,
                    pointRadius: 1,
                    pointHitRadius: 10,
                    spanGapsspanGaps: false,
                },
                {
                    label: "App brugere",
                    data: userInformationLine.appUsers.reverse(),
                    fill: true,
                    lineTension: 0.1,
                    backgroundColor: "rgba(236,154,31,0.4)",
                    borderColor: "rgba(236,154,31,1)",
                    borderCapStyle: 'butt',
                    borderDash: [],
                    borderDashOffset: 0.0,
                    borderJoinStyle: 'miter',
                    pointBorderColor: "rgba(75,192,192,1)",
                    pointBackgroundColor: "#fff",
                    pointBorderWidth: 1,
                    pointHoverRadius: 5,
                    pointHoverBackgroundColor: "rgba(75,192,192,1)",
                    pointHoverBorderColor: "rgba(220,220,220,1)",
                    pointHoverBorderWidth: 2,
                    pointRadius: 1,
                    pointHitRadius: 10,
                    spanGapsspanGaps: false,
                },
                {
                    label: "Brugere uden klub",
                    data: userInformationLine.userWithoutClub.reverse(),
                    fill: true,
                    lineTension: 0.1,
                    backgroundColor: "rgba(19,42,207,0.4)",
                    borderColor: "rgba(19,42,207,1)",
                    borderCapStyle: 'butt',
                    borderDash: [],
                    borderDashOffset: 0.0,
                    borderJoinStyle: 'miter',
                    pointBorderColor: "rgba(75,192,192,1)",
                    pointBackgroundColor: "#fff",
                    pointBorderWidth: 1,
                    pointHoverRadius: 5,
                    pointHoverBackgroundColor: "rgba(75,192,192,1)",
                    pointHoverBorderColor: "rgba(220,220,220,1)",
                    pointHoverBorderWidth: 2,
                    pointRadius: 1,
                    pointHitRadius: 10,
                    spanGapsspanGaps: false,
                }]
            },
            options: {
                responsive: true
            }
        });
    }

    var createBarGraph = function () {
        if (myChartBarUserHistory != null) {
            myChartBarUserHistory.destroy();
        }

        //create bar chart over user history
        myChartBarUserHistory = new Chart(userHistoryBarChart, {
            type: 'bar',
            data: {
                labels: labelsArray,
                fontColor: '#FFFFFF',
                datasets: [{
                    label: "Brugere",
                    data: userInformationBar.users.reverse(),
                    backgroundColor: '#c74658',
                    borderColor: '#c74658',
                    borderWidth: 1
                },
                {
                    label: "Aktive brugere",
                    data: userInformationBar.activeUsers.reverse(),
                    backgroundColor: '#52b46d',
                    borderColor: '#52b46d',
                    borderWidth: 1
                },
                {
                    label: "Inaktive brugere",
                    data: userInformationBar.inactiveUSers.reverse(),
                    backgroundColor: '#5cabca',
                    borderColor: '#5cabca',
                    borderWidth: 1
                },
                {
                    label: "App brugere",
                    data: userInformationBar.appUsers.reverse(),
                    backgroundColor: '#ec9a1f',
                    borderColor: '#ec9a1f',
                    borderWidth: 1
                },
                {
                    label: "Brugere uden klub",
                    data: userInformationBar.usersWithoutClub.reverse(),
                    backgroundColor: '#132acf',
                    borderColor: '#132acf',
                    borderWidth: 1
                }]
            },
            options: {
                tooltips: {
                    enabled: false
                },
                responsive: true,
                scaleShowVerticalLines: false,
                legend: {
                    display: true
                },
                tooltips: {
                    mode: 'x-axis'
                },
                scales: {
                    yAxes: [{
                        id: 'y-axis-0',
                        gridLines: {
                            display: true,
                            lineWidth: 1,
                            color: "rgba(0,0,0,0.30)"
                        },
                        ticks: {
                            showTooltips: false,
                            beginAtZero: true,
                            mirror: false,
                            suggestedMin: 0
                        },
                        afterBuildTicks: function (chart) {

                        }
                    }],
                    xAxes: [{
                        id: 'x-axis-0',
                        gridLines: {
                            display: false
                        },
                        ticks: {
                            beginAtZero: true,
                            autoSkip: false
                        }
                    }]
                },
                hover: {
                    animationDuration: 0
                }
            }
        });
    }
    createLineGraph();
    createBarGraph();
}])
app.factory('connectionUrlFactory', [function () {
    return {
        //change this variable to connect to external server or change local host port number
        //Localhost
        getConnectionUrl: "http://localhost:59031"
        //Test
        //getConnectionUrl: "https://ondostatisticstest.azurewebsites.net"
        //Production
        //getConnectionUrl: "https://ondostatisticsredis20161214022747.azurewebsites.net"
    };
}])
app.factory('fetchDataFactory', ["$http", "connectionUrlFactory", "dateFactory", "sessionStoreFactory", function ($http, connectionUrlFactory, dateFactory, sessionStoreFactory) {
    return {
        shopData: {
            getInitialData: function (ondoId, redirectToView, redirectToChoose) {
                //check if data is cached else retrieve data and redirect
                if (sessionStoreFactory.loadJSON("Data:" + ondoId) != null) {
                    //set current ondo
                    sessionStoreFactory.save('currentOndo', ondoId);
                    //check for redirect
                    if (redirectToView) { window.location.href = "#/shop"; }
                    if (redirectToChoose) { window.location.href = "#/choose"; }
                } else {
                    $http({
                        method: 'GET',
                        dataType: 'json',
                        url: connectionUrlFactory.getConnectionUrl + "/api/shop/" + ondoId
                    }).success(function (data, status, headers, config) {

                        //set current ondo
                        sessionStoreFactory.save('currentOndo', ondoId);
                        //store the initial transactions
                        sessionStoreFactory.saveJSON("Data:" + ondoId, data);
                        //store shop data
                        sessionStoreFactory.saveJSON(ondoId, { "ondoId": data.ondoId, "title": data.title, "profilePicture": data.profilePicture });
                        //check for redirect
                        if (redirectToView) { window.location.href = "#/shop"; }
                        if (redirectToChoose) { window.location.href = "#/choose"; }
                    });
                }
            }
        },
        tradeUnionData: {
            getInitialData: function (ondoId, redirectToView, redirectToChoose) {
                //check if data is cached else retrieve data and redirect
                if (sessionStoreFactory.loadJSON("Data:" + ondoId) != null) {
                    //set current ondo
                    sessionStoreFactory.save('currentOndo', ondoId);
                    //check for redirect
                    if (redirectToView) { window.location.href = "#/tradeunion"; }
                    if (redirectToChoose) { window.location.href = "#/choose"; }
                } else {
                    $http({
                        method: 'GET',
                        dataType: 'json',
                        url: connectionUrlFactory.getConnectionUrl + "/api/tradeunion/" + ondoId
                    }).success(function (data, status, headers, config) {
                        //set current ondo
                        sessionStoreFactory.save('currentOndo', ondoId);
                        //store the initial transactions
                        sessionStoreFactory.saveJSON("Data:" + ondoId, data);
                        //store shop data
                        sessionStoreFactory.saveJSON(ondoId, { "ondoId": data.ondoId, "title": data.title, "profilePicture": data.profilePicture });
                        //check for redirect
                        if (redirectToView) { window.location.href = "#/tradeunion"; }
                        if (redirectToChoose) { window.location.href = "#/choose"; }
                    });
                }
            }
        },
        clubData: {
            getInitialData: function (ondoId, redirectToView, redirectToChoose) {
                //check if data is cached else retrieve data and redirect
                if (sessionStoreFactory.loadJSON("Data:" + ondoId) != null) {
                    //set current ondo
                    sessionStoreFactory.save('currentOndo', ondoId);
                    //check for redirect
                    if (redirectToView) { window.location.href = "#/club"; }
                    if (redirectToChoose) { window.location.href = "#/choose"; }
                } else {
                    $http({
                        method: 'GET',
                        dataType: 'json',
                        url: connectionUrlFactory.getConnectionUrl + "/api/club/" + ondoId
                    }).success(function (data, status, headers, config) {
                        //set current ondo
                        sessionStoreFactory.save('currentOndo', ondoId);
                        //store the initial transactions
                        sessionStoreFactory.saveJSON("Data:" + ondoId, data);
                        //store shop data
                        sessionStoreFactory.saveJSON(ondoId, { "ondoId": data.ondoId, "title": data.title, "profilePicture": data.profilePicture });
                        //check for redirect
                        if (redirectToView) { window.location.href = "#/club"; }
                        if (redirectToChoose) { window.location.href = "#/choose"; }
                    });
                }
            }
        }
    };
}])
app.factory('sessionStoreFactory', [function () {
    return {
        save: function (key, value) { sessionStorage.setItem(key, value) },
        load: function (key) { return sessionStorage.getItem(key) },
        saveJSON: function (key, value) { sessionStorage.setItem(key, JSON.stringify(value)) },
        loadJSON: function (key) { return JSON.parse(sessionStorage.getItem(key)) }
    }
}])
app.factory('dateFactory', [function () {
    return {
        getDaysArrayForQuarter: function (quarter, year) {
            var daysInQuarterArr = [];
            var today = this.getDkTime();
            var day = 1000 * 60 * 60 * 24;

            //use date strings to remove clock difference
            var startDateStr = this.getQuartersStartDate(quarter, year);
            var todayStr = today.getFullYear() + "-" + (today.getMonth() + 1) + "-" + today.getDate();

            var dateFrom = new Date(startDateStr).getTime();
            var dateTo = new Date(todayStr).getTime();

            var days = Math.floor(Math.abs((dateTo - dateFrom) / day));

            //push date and increase with one day
            for (var i = 0; i <= days; i++) {
                daysInQuarterArr.push(new Date(dateFrom));
                dateFrom += day;
            }

            //reverse the array to ensure the dates are traversed in ascending order
            return daysInQuarterArr.reverse();
        },
        getWeeksArrayForQuarter: function (quarter, year) {
            var weeksArray = [];
            //get startdate of quarter and create date, if not stated set quarter to current quarter
            var startDate = quarter == null ? new Date(moment().startOf('quarter').format('MM-DD-YYYY')) : this.getQuartersStartDate(quarter, year).split('-')[1] + "-" + this.getQuartersStartDate(quarter, year).split('-')[2] + "-" + this.getQuartersStartDate(quarter, year).split('-')[0];

            //get end date, if not stated then set enddate to today
            var endDate = quarter == null ? this.getDkTime() : this.getQuartersEndDate(quarter, year).split('-')[1] + "-" + this.getQuartersEndDate(quarter, year).split('-')[2] + "-" + this.getQuartersEndDate(quarter, year).split('-')[0];

            //get start week of current quarter
            var startWeek = this.getWeekNumberFromDate(startDate);
            //get current week
            var endWeek = this.getWeekNumberFromDate(endDate);

            //if quarter ends in week one then set end week to week 52 to ensure correct graphs
            var isWeekOne = false;
            if (endWeek == 1) {
                isWeekOne = true;
                endWeek = 52;
            }

            //count number of weeks
            var numberOfWeeks;
            if (startWeek > endWeek) {
                numberOfWeeks = endWeek
            } else {
                numberOfWeeks = endWeek - startWeek;
            }

            //fill array with weeknumbers
            for (var i = 0; i <= numberOfWeeks; i++) {
                weeksArray.push(startWeek);
                //check if startweek is week 52 then set to week 1 and continue
                if (startWeek > 52) {
                    startWeek = 1;
                    continue;
                }
                startWeek++;
            }
            if (isWeekOne) {
                weeksArray.push(1);
            }
            return weeksArray;
        },
        getDkTime: function () {
            var d = new Date();
            var date = new Date(d.getUTCFullYear(), d.getUTCMonth(), d.getUTCDate(), d.getUTCHours(), d.getUTCMinutes(), d.getUTCSeconds());

            var dkHoursToAdd = 1;
            var summerTimeDate;
            var winterTimeDate;

            for (var i = 24; i <= 31; i++) {
                // check this years dates from may 24 - 31 to determine which is a sunday
                var d = new Date("May " + i + ", " + date.getFullYear());
                if (d.getDay() === 0) {
                    summerTimeDate = d;
                }
            }
            for (var j = 24; j <= 31; j++) {
                // check this years dates from may 24 - 31 to determine which is a sunday
                var dt = new Date("October " + j + ", " + date.getFullYear());
                if (dt.getDay() === 0) {
                    winterTimeDate = dt;
                }
            }

            //check if todays date is in the summer- or wintertime period
            if (date.getTime() >= summerTimeDate.getTime() && date.getTime() <= winterTimeDate.getTime()) {
                dkHoursToAdd = 2;
            }

            //return the current DK time
            date.setUTCHours(date.getUTCHours() + dkHoursToAdd);

            return date;
        },
        getCurrentWeek: function () {
            //get Dk time
            var d = this.getDkTime();
            //return momentjs iso week from Dk time
            return moment(d, "MM-DD-YYYY").isoWeek();
        },
        getWeekNumberFromDate: function (date) {
            return moment(date, "MM-DD-YYYY").isoWeek();
        },
        getCurrentQuarter: function () {
            var d = this.getDkTime();
            var m = d.getMonth() + 1;
            var q;
            if (m <= 3) {
                q = 1;
            } else if (m > 3 && m <= 6) {
                q = 2;
            } else if (m > 6 && m <= 9) {
                q = 3;
            } else {
                q = 4;
            }
            return q;
        },
        getQuarterFromDate: function (date) {
            var d = date;
            var m = d.getMonth() + 1;
            var q;
            if (m <= 3) {
                q = 1;
            } else if (m > 3 && m <= 6) {
                q = 2;
            } else if (m > 6 && m <= 9) {
                q = 3;
            } else {
                q = 4;
            }
            return q;
        },
        getQuartersStartDate: function (q, year) {
            var quarter = q + "";
            //returns a string representation of the start date of the quarter format YYYY/MM/DD
            switch (quarter) {
                case "1":
                    return year + "-1-1";
                case "2":
                    return year + "-4-1";
                case "3":
                    return year + "-7-1";
                case "4":
                    return year + "-10-1";
            }
        },
        getQuartersEndDate: function (q, year) {
            var quarter = q + "";
            //returns a string representation of the start date of the quarter format YYYY/MM/DD
            switch (quarter) {
                case "1":
                    return year + "-3-31";
                case "2":
                    return year + "-6-30";
                case "3":
                    return year + "-9-30";
                case "4":
                    return year + "-12-31";
            }
        },
        getWeekBeforeQuarterStart: function (q, year) {
            var quarter = q + "";
            switch (quarter) {
                case "1":
                    return year - 1 + "-12-24";
                case "2":
                    return year + "-3-24";
                case "3":
                    return year + "-6-23";
                case "4":
                    return year + "-9-23";
            }
        }
    };
}]);