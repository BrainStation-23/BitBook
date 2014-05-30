﻿"use strict";

(function(app) {
    app.controller("ProfileEditCtrl", [
        "$scope", "identityService", "notifierService", "apiService", "$rootScope", "$fileUploader", function($scope, identityService, notifierService, apiService, $rootScope, $fileUploader) {
            $scope.init = function() {
                if (!identityService.isLoggedIn()) {
                    $scope.redirectToLogin();
                } else {
                    var config = {
                        headers: identityService.getSecurityHeaders()
                    };
                    apiService.get("/api/profile", config).success(function(data) {
                        $scope.user = data;
                        $scope.user.email = data.userName;
                        notifierService.notify({ responseType: "success", message: "Profile data fetched successfully." });
                    });
                }
            }();

            $scope.update = function(user) {
                $scope.profileEditFormSubmitted = true;
                if ($scope.ProfileEditForm.$valid) {
                    var config = {
                        headers: identityService.getSecurityHeaders()
                    };
                    user.userName = user.email;
                    apiService.put("/api/profile/", user, config).success(function() {
                        $rootScope.authenticatedUser.userName = user.email;
                        notifierService.notify({ responseType: "success", message: "Profile data updated successfully." });
                    }).error(function(error) {
                        if (error.modelState) {
                            $scope.localRegisterErrors = _.flatten(_.map(error.modelState, function(items) {
                                return items;
                            }));
                        } else {
                            var data = {
                                responseType: "error",
                                message: error.message
                            };
                            notifierService.notify(data);
                        }
                    });
                }
            };

            $scope.uploader = $fileUploader.create({
                scope: $scope,
                url: "/api/profile",
                filters: [
                    function(item) {
                        return true;
                    }
                ],
                headers: {
                    "Accept": 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken')
                }
            });
        }
    ]);
})(_$.app);