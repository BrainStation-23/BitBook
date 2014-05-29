"use strict";

(function(app) {
    app.controller("RegisterCtrl", [
        "$scope", "$location", "identityService", "notifierService", function ($scope, $location, identityService, notifierService) {
            $scope.init = function() {
                if (identityService.isLoggedIn()) {
                    $location.path("/");
                }
            }();
            $scope.register = function(user) {
                identityService.register(user).success(function() {
                    identityService.login(user).success(function (data) {
                        if (data.userName && data.access_token) {
                            identityService.setAccessToken(data.access_token);
                            identityService.setAuthorizedUserData(data);
                            $location.path("/");
                        }
                    });
                }).error(function (error) {
                    var data = {
                        responseType: "error",
                        message: error.message
                    };
                    notifierService.notify(data);
                });
            };
        }
    ]);
})(_$.app);