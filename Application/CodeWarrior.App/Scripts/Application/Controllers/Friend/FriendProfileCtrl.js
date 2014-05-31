"use strict";

(function(app) {
    app.controller("FriendProfileCtrl", [
        "$scope", "$location", "identityService", "apiService", "notifierService", "$routeParams", "$rootScope",
        function ($scope, $location, identityService, apiService, notifierService, $routeParams, $rootScope) {
            $scope.init = function() {
                if (!identityService.isLoggedIn()) {
                    $scope.redirectToLogin();
                } else {
                    if ($rootScope.authenticatedUser.id == $routeParams.id) {
                        $location.path("/account/profile");
                    } else {
                        var config = {
                            headers: identityService.getSecurityHeaders(),
                            params: {
                                id: $routeParams.id
                            }
                        };
                        apiService.get("/api/profile", config).success(function (data) {
                            $scope.user = data;
                            notifierService.notify({ responseType: "success", message: "Profile data fetched successfully." });
                        });
                    }
                }
            }();
        }
    ]);
})(_$.app);