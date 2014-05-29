"use strict";

(function(app) {
    app.controller("LoginCtrl", [
        "$scope", "identityService", "notifierService", "$location", function ($scope, identityService, notifierService, $location) {
            $scope.loginProviders = [];
            $scope.init = function () {
                if (identityService.isLoggedIn()) {
                    $location.path("/");
                }
                identityService.getExternalLogins().then(function(result) {
                    if (result.data) {
                        $scope.loginProviders = result.data;
                    }
                });
            }();
            $scope.login = function (user) {
                if (user && user.userName && user.password) {
                    identityService.login(user).then(function (data) {
                        if (data.userName && data.access_token) {
                            identityService.setAccessToken(data.access_token);
                            identityService.setAuthorizedUserData(data);
                            $location.path("/");
                        }
                    });
                } else {
                    var notification = {
                        responseType: "error",
                        message: "Invalid username or password."
                    };
                    notifierService.notify(notification);
                }
            };

            $scope.externalLogin = function (loginProvider) {

                sessionStorage["state"] = loginProvider.state;
                sessionStorage["loginUrl"] = loginProvider.url;
                // IE doesn't reliably persist sessionStorage when navigating to another URL. Move sessionStorage temporarily
                // to localStorage to work around this problem.
                identityService.archiveSessionStorageToLocalStorage();
                window.location = loginProvider.url;
            };
        }
    ]);
})(_$.app);