﻿"use strict";

(function(app) {
    app.controller("LoginCtrl", [
        "$scope", "identityService", "notifierService", "$location", function($scope, identityService, notifierService, $location) {
            $scope.loginProviders = [];
            $scope.init = function() {
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
                $scope.loginFormSubmitted = true;
                if ($scope.LoginForm.$valid) {
                    identityService.login(user).success(function(data) {
                        if (data.userName && data.access_token) {
                            identityService.setAccessToken(data.access_token);
                            identityService.setAuthorizedUserData(data);
                            $location.path("/");
                        }
                    }).error(function(error) {
                        if (error.error_description) {
                            notifierService.notify({ responseType: "error", message: error.error_description });
                        }
                    });
                }
            };

            $scope.externalLogin = function(loginProvider) {

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