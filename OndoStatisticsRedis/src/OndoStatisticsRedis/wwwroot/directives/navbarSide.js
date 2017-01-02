angular.module('navbarSideDirective', [])
    .controller('navBarSideCtrl', ['$scope', '$http', 'connectionUrlFactory', 'dateFactory', 'sessionStoreFactory', 'fetchDataFactory', function ($scope, $http, connectionUrlFactory, dateFactory, sessionStoreFactory, fetchDataFactory) {
        var self = this;

        //if userdata is null then redirect to login page
        if (sessionStorage.getItem("userData") == null) {
            window.location.href = "/";
        }

        //get the users ondo list
        var userOndoList = sessionStoreFactory.loadJSON("userData");
        var currentOndo = sessionStorage.getItem("currentOndo");
        var currentOndoData = JSON.parse(sessionStorage.getItem(currentOndo));

        //retrieve basic ondo information from sessionStorage
        self.ondoTitle = currentOndoData.title == null ? "Ingen titel" : currentOndoData.title;
        self.ondoProfilePicture = currentOndoData.profilePicture;
        self.ondoLocality = currentOndoData.locality;
        $scope.hideInfoOnMobileToggle = { bool: false }; // object to show and hide shop info in navbar toggle

        //initialize ondoList
        self.ondoList = [];

        //add profilepicture to view
        $("#profilePictureShop").html('<img id="profilePicture" src="' + self.ondoProfilePicture + '" alt="Profile picture" />');

        //active link function
        var activeLink = function () {
            //set background color of the a tag for active link
            switch (document.location.hash) {
                case "#/shop":
                    $($("#sideNavBarList")[0].children[0].children[0]).css("background-color", "rgb(105, 104, 104)");
                    break;
                case "#/shop/subscriptions":
                    $($("#sideNavBarList")[0].children[1].children[0]).css("background-color", "rgb(105, 104, 104)");
                    break;
                case "#/shop/transactions":
                    $($("#sideNavBarList")[0].children[2].children[0]).css("background-color", "rgb(105, 104, 104)");
                    break;
                case "#/shop/history":
                    $($("#sideNavBarList")[0].children[3].children[0]).css("background-color", "rgb(105, 104, 104)");
                    break;
                    
            }
        }
        activeLink();
        
        //Ondo dropdown change event
        $("#ondoDropDownElement").on('change', function () {
            //turn on loading gif
            $(".mainContent").css("-webkit-filter", "blur(10px)");
            $(".mainContent").css("filter", "blur(10px)");
            $("#loading").css("display", "block");

            //get ondoId of ondo selected
            var ondoId = $("#ondoDropDownElement option:selected").attr('value');
            // get type of ondo selected
            var type;
            //run through the list of ondos to find the ondo chosen and retrieve its type
            for (var k = 0; k < userOndoList.length; k++) {
                if (userOndoList[k].ondoId == ondoId) {
                    type = userOndoList[k].type;
                };
            };

            //fetch data and redirect
            if      (type == 1) { fetchDataFactory.tradeUnionData.getInitialData(ondoId); }
            else if (type == 2) { fetchDataFactory.shopData.getInitialData(ondoId); }
        });

        //if users ondo list is greater than one then fill dropdown with all ondos
        var populateDropDown = function () {
            if (userOndoList.length > 1) {
                for (var i = 0; i < userOndoList.length; i++) {
                    if (userOndoList[i].type == 0) { continue } // if the ondo is admin then continue loop
                    //ondo object from session storage
                    var obj = sessionStoreFactory.loadJSON(userOndoList[i].ondoId);
                    if (userOndoList[i].ondoId != null && obj.title != null && obj.type != 3) {
                        self.ondoList.push({
                            "ondoId": userOndoList[i].ondoId,
                            "title": obj.title == null ? "Ingen titel" : obj.title
                        });
                    }
                }
                ////set the default ondo
                self.ondoChosen = { "ondoId": parseInt(sessionStorage.getItem("currentOndo")), "title": JSON.parse(sessionStorage.getItem(sessionStorage.getItem("currentOndo"))).title };
            } else {
                //if there is only one ondo in the list then remove the dropdown selector
                var node = document.getElementById("ondoDropDownElement");
                node.innerHTML = "";
            }
        };
        populateDropDown();

        //toggle function to change icon and change navbar layout
        self.toggle = function () {
            //IF FA-BARS THEN TOGGLE IS OFF
            if (document.getElementById("sideNavBarToggle").className == "fa fa-bars") {
                $scope.hideInfoOnMobileToggle.bool = true;
                document.getElementById("sideNavBarToggle").className = "fa fa-times";
                document.getElementById("navBar").className = "sideNavBar-toggle";
            } else {
                $scope.hideInfoOnMobileToggle.bool = false;
                document.getElementById("sideNavBarToggle").className = "fa fa-bars";
                document.getElementById("navBar").className = "sideNavBar";
            }
        }

        //logout function to clear session user
        self.logout = function () {
            //remove the session object
            sessionStorage.removeItem("userData");
            //redirect to loginpage
            window.location.href = "/";
        }
    }])
.directive('navbarSide', function () {
    return {
        templateUrl: 'directives/navbarSide.html',
        restrict: 'EA'
    }
});