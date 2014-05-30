"use strict";

(function(app) {
    app.controller("ProfileEditCtrl", [
        "$scope", "identityService", "notifierService", "apiService", "$rootScope", function($scope, identityService, notifierService, apiService, $rootScope) {
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
                    apiService.post("/api/Account/ManageUserProfile", user, config).success(function() {
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
        }
    ]);
})(_$.app);