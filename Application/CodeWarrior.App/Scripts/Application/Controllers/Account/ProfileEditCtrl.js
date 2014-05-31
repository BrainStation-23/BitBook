"use strict";

(function(app) {
    app.controller("ProfileEditCtrl", [
        "$scope", "identityService", "notifierService", "apiService", "$rootScope", function ($scope, identityService, notifierService, apiService, $rootScope) {
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
            };
            $scope.init();

            $scope.update = function(user) {
                $scope.profileEditFormSubmitted = true;
                if ($scope.ProfileEditForm.$valid) {
                    $scope.profileEditInProgresss = true;
                    var config = {
                        headers: identityService.getSecurityHeaders()
                    };
                    user.userName = user.email;
                    apiService.put("/api/profile/", user, config).success(function() {
                        $scope.profileEditInProgresss = false;
                        $rootScope.authenticatedUser.userName = user.email;
                        notifierService.notify({ responseType: "success", message: "Profile data updated successfully." });
                    }).error(function(error) {
                        $scope.profileEditInProgresss = false;
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

            window.uploadAvatar = function() {
                var files = $('input[name="avatar"]').get(0).files;
                var data = new FormData();
                data.append("avatar", files[0]);

                $.ajax({
                    type: "POST",
                    url: "/api/Profile/",
                    contentType: false,
                    processData: false,
                    headers: {
                        Authorization: "Bearer " + identityService.getAccessToken()
                    },
                    data: data,
                    success: function(results) {
                        notifierService.notify({ responseType: "success", message: "Profile picture uploaded successfully" });
                        $scope.init();
                    }
                });
            };
        }
    ]);
})(_$.app);