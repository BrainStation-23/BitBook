"use strict";

(function(app) {
    app.controller("PasswordManageCtrl", [
        "$scope", "$location", "identityService", "notifierService", "apiService", function($scope, $location, identityService, notifierService, apiService) {
            $scope.init = function() {
                if (!identityService.isLoggedIn()) {
                    $scope.redirectToLogin();
                } else {
                    var config = {
                        headers: identityService.getSecurityHeaders()
                    };
                    apiService.get("/api/account/HasPassword", config).success(function (hasPassword) {
                        $scope.hasLocalAccount = hasPassword === "true";
                    });
                }
            }();

            $scope.changePassword = function(password) {
                $scope.managePasswordFormSubmitted = true;
                if ($scope.ManagePasswordForm.$valid) {
                    var config = {
                        headers: identityService.getSecurityHeaders()
                    };
                    apiService.post("/api/Account/ChangePassword", password, config).success(function () {
                        notifierService.notify({
                            responseType: "success",
                            message: "Password changed successfully."
                        });
                        $scope.changePasswordErrors = [];
                    }).error(function(error) {
                        if (error.modelState) {
                            $scope.changePasswordErrors = _.flatten(_.map(error.modelState, function(items) {
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