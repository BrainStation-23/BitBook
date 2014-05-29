"use strict";

(function(app) {
    app.controller("BaseCtrl", [
        "$scope", "$location", "identityService", function($scope, $location, identityService) {
            $scope.logout = function() {
                identityService.logout().success(function() {
                    identityService.clearAccessToken();
                    identityService.clearAuthorizedUserData();
                    $location.path("/account/login");
                });
            };
        }
    ]);
})(_$.app);