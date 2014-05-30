"use strict";

(function(app) {
    app.controller("HomeCtrl", [
        "$scope", "identityService", "apiService", function ($scope, identityService, apiService) {
            $scope.init = function() {
                if (!identityService.isLoggedIn()) {
                    $scope.redirectToLogin();
                } else {
                    //apiService.get("/api/posts/").success(function (result) {
                    //    if (result) {
                    //        $scope.posts = result;
                    //    }
                    //});
                }
            }();

            $scope.addPost = function (post) {
                $scope.addPostSubmitted = true;
                if ($scope.AddPostForm.$valid) {
                    apiService.post("api/posts", post).success(function (result) {
                        console.log(result);
                    });
                }
            };
        }
    ]);
})(_$.app);