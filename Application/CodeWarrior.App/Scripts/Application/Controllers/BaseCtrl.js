"use strict";

(function(app) {
    app.controller("BaseCtrl", [
        "$scope", "$location", "identityService", "apiService", function($scope, $location, identityService, apiService) {
            $scope.logout = function() {
                identityService.logout().success(function() {
                    identityService.clearAccessToken();
                    identityService.clearAuthorizedUserData();
                    $location.path("/account/login");
                });
            };

            $scope.search = function() {
                if (identityService.isLoggedIn()) {
                    var config = {
                        params: {
                            q: $scope.searchKey
                        }
                    };
                    apiService.get("/api/users/", config).success(function(result) {
                        $location.path("/users/searchResult");
                    }).error(function(error) {
                        console.log(error);
                    });
                } else {
                    $location.path("/account/login");
                }
            };
        }
    ]);
})(_$.app);