"use strict";

(function(app) {
    app.controller("HomeCtrl", [
        "$scope", "identityService", "apiService", "notifierService", function ($scope, identityService, apiService, notifierService) {
            $scope.posts = [];
            $scope.hasAnyNewsFeed = false;
            $scope.toggleLikeText = "Like";

            $scope.init = function () {
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
                    var config = {
                        headers: identityService.getSecurityHeaders()
                    };
                    console.log(identityService.getSecurityHeaders());
                    apiService.post("/api/posts", post, config).success(function (result) {
                        $scope.posts.splice(0, 0, result);
                        $scope.hasAnyNewsFeed = true;
                        console.log(result);
                    }).error(function (error) {
                        if (error.modelState) {
                            $scope.postCreateErrors = _.flatten(_.map(error.modelState, function (items) {
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

            $scope.toggleLike = function (postId) {

                if ($scope.toggleLikeText == "Like") {
                    apiService.post("/api/like", postId, config).success(function (result) {
                        $scope.toggleLikeText = "Unlike";
                        console.log(result);
                    }).error(function (error) {
                        if (error.modelState) {
                            $scope.postCreateErrors = _.flatten(_.map(error.modelState, function (items) {
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
                } else {
                    var unlikeConfig = {
                        headers: identityService.getSecurityHeaders(),
                        data: postId
                    };
                    apiService.remove("/api/like", unlikeConfig).success(function (result) {
                        $scope.toggleLikeText = "Like";
                        console.log(result);
                    }).error(function (error) {
                        if (error.modelState) {
                            $scope.postCreateErrors = _.flatten(_.map(error.modelState, function (items) {
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