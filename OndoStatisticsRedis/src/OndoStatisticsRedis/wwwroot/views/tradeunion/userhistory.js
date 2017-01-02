angular.module('tradeUnionUserHistoryView', [])
.controller('tradeUnionUserHistoryCtrl', ['$http', 'connectionUrlFactory', '$scope', 'sessionStoreFactory', function ($http, connectionUrlFactory, $scope, sessionStoreFactory) {
    var self = this;

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

    //get data for quarters selected
    var getData = function () {
        $http({
            method: 'GET',
            dataType: 'json',
            url: connectionUrlFactory.getConnectionUrl + "/api/tradeunion/userhistory/" + ondoId
        }).success(function (data, status, headers, config) {
            sessionStoreFactory.saveJSON("userHistory", data);

            for (var i = 0; i < data.length; i++) {
                labelsArray.push(data[i].quarter + ". kvartal " + data[i].year);
                self.userInformationArray.push({ time: { year: data[i].year, quarter: data[i].quarter }, users: data[i].users, activeUsers: data[i].activeUsers, inactiveUsers: data[i].inactiveUsers, appUsers: data[i].appUsers, usersWithoutClub: data[i].usersWithoutClub });
            }


            if (self.userInformationArray.length == 1) {
                var label;

                if (labelsArray[0] == "1. kvartal " + data[0].year) {
                    label = "4. kvartal " + (data[0].year - 1);
                } else if (labelsArray[0] == "2. kvartal " + data[0].year) {
                    label = "3. kvartal " + data[0].year;
                } else if (labelsArray[0] == "3. kvartal " + data[0].year) {
                    label = "2. kvartal " + data[0].year;
                } else if (labelsArray[0] == "4. kvartal " + data[0].year) {
                    label = "1. kvartal " + data[0].year;
                }
                $scope.period.push({ title: labelsArray[0], value: 0 });
                labelsArray.push(label);
                labelsArray = labelsArray.reverse();

                userInformationLine.activeUsers.push(0);
                userInformationLine.inactiveUsers.push(0);
                userInformationLine.appUsers.push(0);
                userInformationLine.userWithoutClub.push(0);

                userInformationBar.users.push(0);
                userInformationBar.activeUsers.push(0);
                userInformationBar.inactiveUSers.push(0);
                userInformationBar.appUsers.push(0);
                userInformationBar.usersWithoutClub.push(0);

                userInformationLine.activeUsers.push(Math.round(((self.userInformationArray[0].activeUsers / self.userInformationArray[0].users) * 100)));
                userInformationLine.inactiveUsers.push(Math.round(((self.userInformationArray[0].inactiveUsers / self.userInformationArray[0].users) * 100)));
                userInformationLine.appUsers.push(Math.round(((self.userInformationArray[0].appUsers / self.userInformationArray[0].users) * 100)));
                userInformationLine.userWithoutClub.push(Math.round(((self.userInformationArray[0].usersWithoutClub / self.userInformationArray[0].users) * 100)));

                userInformationBar.users.push(self.userInformationArray[0].users);
                userInformationBar.activeUsers.push(self.userInformationArray[0].activeUsers);
                userInformationBar.inactiveUSers.push(self.userInformationArray[0].inactiveUsers);
                userInformationBar.appUsers.push(self.userInformationArray[0].appUsers);
                userInformationBar.usersWithoutClub.push(self.userInformationArray[0].usersWithoutClub);

                userInformationBar.users = userInformationBar.users.reverse();
                userInformationBar.activeUsers = userInformationBar.activeUsers.reverse();
                userInformationBar.inactiveUSers = userInformationBar.inactiveUSers.reverse();
                userInformationBar.appUsers = userInformationBar.appUsers.reverse();
                userInformationBar.usersWithoutClub = userInformationBar.usersWithoutClub.reverse();

                userInformationLine.activeUsers = userInformationLine.activeUsers.reverse();
                userInformationLine.inactiveUsers = userInformationLine.inactiveUsers.reverse();
                userInformationLine.appUsers = userInformationLine.appUsers.reverse();
                userInformationLine.userWithoutClub = userInformationLine.userWithoutClub.reverse();

                var information = [];
                information.push({ title: "Total antal kort brugere", number: self.userInformationArray[0].users, percentage: 100 });
                information.push({ title: "Antal aktive kort brugere", number: self.userInformationArray[0].activeUsers, percentage: Math.round(((self.userInformationArray[0].activeUsers / self.userInformationArray[0].users) * 100)) });
                information.push({ title: "Antal inaktive kort brugere", number: self.userInformationArray[0].inactiveUsers, percentage: Math.round(((self.userInformationArray[0].inactiveUsers / self.userInformationArray[0].users) * 100)) });
                information.push({ title: "Antal app brugere med kort", number: self.userInformationArray[0].appUsers, percentage: Math.round(((self.userInformationArray[0].appUsers / self.userInformationArray[0].users) * 100)) });
                information.push({ title: "Antal kort brugere uden valgt klub", number: self.userInformationArray[0].usersWithoutClub, percentage: Math.round(((self.userInformationArray[0].usersWithoutClub / self.userInformationArray[0].users) * 100)) });
                self.userArray.push(information);
            } else {

                for (var i = 0; i < self.userInformationArray.length; i++) {
                    $scope.period.push({ title: labelsArray[i], value: i });

                    var information = [];

                    information.push({ title: "Total antal kort brugere", number: self.userInformationArray[i].users, percentage: 100 });
                    information.push({ title: "Antal aktive kort brugere", number: self.userInformationArray[i].activeUsers, percentage: Math.round(((self.userInformationArray[i].activeUsers / self.userInformationArray[i].users) * 100)) });
                    information.push({ title: "Antal inaktive kort brugere", number: self.userInformationArray[i].inactiveUsers, percentage: Math.round(((self.userInformationArray[i].inactiveUsers / self.userInformationArray[i].users) * 100)) });
                    information.push({ title: "Antal app brugere med kort", number: self.userInformationArray[i].appUsers, percentage: Math.round(((self.userInformationArray[i].appUsers / self.userInformationArray[i].users) * 100)) });
                    information.push({ title: "Antal kort brugere uden valgt klub", number: self.userInformationArray[i].usersWithoutClub, percentage: Math.round(((self.userInformationArray[i].usersWithoutClub / self.userInformationArray[i].users) * 100)) });
                    self.userArray.push(information);

                    self.periodLength = $scope.period.length - 1;
                }
                if (self.userInformationArray.length > 4) {
                    var tempeorayLabelsArray = labelsArray.reverse();
                    labelsArray = [];
                    labelsArray.push(tempeorayLabelsArray[3]);
                    labelsArray.push(tempeorayLabelsArray[2]);
                    labelsArray.push(tempeorayLabelsArray[1]);
                    labelsArray.push(tempeorayLabelsArray[0]);

                    for (var i = self.userInformationArray.length -1; i > (self.userInformationArray.length - 5); i--) {

                        userInformationLine.activeUsers.push(Math.round(((self.userInformationArray[i].activeUsers / self.userInformationArray[i].users) * 100)));
                        userInformationLine.inactiveUsers.push(Math.round(((self.userInformationArray[i].inactiveUsers / self.userInformationArray[i].users) * 100)));
                        userInformationLine.appUsers.push(Math.round(((self.userInformationArray[i].appUsers / self.userInformationArray[i].users) * 100)));
                        userInformationLine.userWithoutClub.push(Math.round(((self.userInformationArray[i].usersWithoutClub / self.userInformationArray[i].users) * 100)));

                        userInformationBar.users.push(self.userInformationArray[i].users);
                        userInformationBar.activeUsers.push(self.userInformationArray[i].activeUsers);
                        userInformationBar.inactiveUSers.push(self.userInformationArray[i].inactiveUsers);
                        userInformationBar.appUsers.push(self.userInformationArray[i].appUsers);
                        userInformationBar.usersWithoutClub.push(self.userInformationArray[i].usersWithoutClub);
                    }

                } else {
                    for (var i = 0; i < self.userInformationArray.length; i++) {
                        userInformationLine.activeUsers.push(Math.round(((self.userInformationArray[i].activeUsers / self.userInformationArray[i].users) * 100)));
                        userInformationLine.inactiveUsers.push(Math.round(((self.userInformationArray[i].inactiveUsers / self.userInformationArray[i].users) * 100)));
                        userInformationLine.appUsers.push(Math.round(((self.userInformationArray[i].appUsers / self.userInformationArray[i].users) * 100)));
                        userInformationLine.userWithoutClub.push(Math.round(((self.userInformationArray[i].usersWithoutClub / self.userInformationArray[i].users) * 100)));

                        userInformationBar.users.push(self.userInformationArray[i].users);
                        userInformationBar.activeUsers.push(self.userInformationArray[i].activeUsers);
                        userInformationBar.inactiveUSers.push(self.userInformationArray[i].inactiveUsers);
                        userInformationBar.appUsers.push(self.userInformationArray[i].appUsers);
                        userInformationBar.usersWithoutClub.push(self.userInformationArray[i].usersWithoutClub);
                    }
                    userInformationBar.users = userInformationBar.users.reverse();
                    userInformationBar.activeUsers = userInformationBar.activeUsers.reverse();
                    userInformationBar.inactiveUSers = userInformationBar.inactiveUSers.reverse();
                    userInformationBar.appUsers = userInformationBar.appUsers.reverse();
                    userInformationBar.usersWithoutClub = userInformationBar.usersWithoutClub.reverse();

                    userInformationLine.activeUsers = userInformationLine.activeUsers.reverse();
                    userInformationLine.inactiveUsers = userInformationLine.inactiveUsers.reverse();
                    userInformationLine.appUsers = userInformationLine.appUsers.reverse();
                    userInformationLine.userWithoutClub = userInformationLine.userWithoutClub.reverse();
                }
            }
            $scope.period = $scope.period.reverse();
            createLineGraph();
            createBarGraph();
        });
    };
    getData();
}])