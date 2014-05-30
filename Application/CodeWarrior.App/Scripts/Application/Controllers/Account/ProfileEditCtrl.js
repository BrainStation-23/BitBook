"use strict";

(function(app) {
    app.controller("ProfileEditCtrl", [
        "$scope", "identityService", "notifierService", "apiService", function($scope, identityService, notifierService, apiService) {
            $scope.init = function() {
                if (!identityService.isLoggedIn()) {
                    $scope.redirectToLogin();
                } else {
                    identityService.getUserInfo().success(function(data) {
                        $scope.user = data;
                        $scope.user.email = data.userName;
                        notifierService.notify({ responseType: "success", message: "Profile data fetched successfully." });
                    });
                }
            }();

            $scope.update = function(user) {
                $scope.profileEditFormSubmitted = true;
                if ($scope.ProfileEditForm.$valid) {
                    apiService.post("", user);
                }
            };
        }
    ]);
})(_$.app);