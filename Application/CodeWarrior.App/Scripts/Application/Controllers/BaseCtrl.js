"use strict";

(function(app) {
    app.controller("BaseCtrl", [
        "$scope", "$location", "identityService", "apiService", function($scope, $location, identityService, apiService) {
            $scope.redirectToLogin = function() {
                $location.path("/account/login");
            };

            $scope.redirectToHome = function () {
                $location.path("/home");
            };

            $scope.logout = function() {
                identityService.logout().success(function() {
                    identityService.clearAccessToken();
                    identityService.clearAuthorizedUserData();
                    $scope.redirectToLogin();
                });
            };

            $scope.search = function (searchKey) {
                if (identityService.isLoggedIn()) {
                    //var config = {
                    //    params: {
                    //        q: $scope.searchKey
                    //    }
                    //};
                    $location.path("/search/" + searchKey);
                    //apiService.get("/api/users/", config).success(function(result) {
                    //    $location.path("/users/searchResult");
                    //}).error(function(error) {
                    //    console.log(error);
                    //});
                } else {
                    $scope.redirectToLogin();
                }
            };
        }
    ]);
})(_$.app);