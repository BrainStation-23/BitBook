"use strict";

(function (app) {
    app.controller("PostAddCtrl", [
        "$scope", "identityService", "$location", function ($scope, identityService, $location) {
            $scope.init = function () {
                if (!identityService.isLoggedIn()) {
                    $location.path("/");
                } 
            }();

            $scope.addPost = function (post) {
                $scope.addPostSubmitted = true;
                if ($scope.AddPostForm.$valid) {
                    console.log(post);
                }
            };
        }
    ]);
})(_$.app);