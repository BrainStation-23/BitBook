"use strict";

(function(app) {
    app.controller("BaseCtrl", [
        "$scope", "$location", "identityService", "apiService", "signalRConnectionService", "notifierService", "$rootScope", function ($scope, $location, identityService, apiService, signalRConnectionService, notifierService, $rootScope) {
            $scope.redirectToLogin = function() {
                $location.path("/account/login");
            };

            $scope.redirectToHome = function () {
                $location.path("/home");
            };

            $scope.logout = function() {
                identityService.logout().success(function() {
                    identityService.clearAccessToken();
                    identityService.clearAuthorizedUserData();
                    $scope.redirectToLogin();
                });
            };

            $scope.viewFrienRequest = function() {
                $location.path("/friendRequest/");
            };

            $scope.search = function (searchKey) {
                if (identityService.isLoggedIn()) {
                    $location.path("/search/" + searchKey);
                } else {
                    $scope.redirectToLogin();
                }
            };
            var signalRConnection = signalRConnectionService.getSignalRConnection();
            signalRConnection.client.myNotification = function (message, userId) {
                if (userId == $rootScope.authenticatedUser.id) {
                    notifierService.notify({
                        responseType: "success",
                        message: message
                    });
                    console.log(message);
                    $scope.$apply();
                }
                console.log(name);
            };
        }
    ]);
})(_$.app);