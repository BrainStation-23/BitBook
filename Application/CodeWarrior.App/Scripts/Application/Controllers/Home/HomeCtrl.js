"use strict";

(function (app) {
    app.controller("HomeCtrl", [
        "$scope", "identityService", "apiService", "notifierService", function ($scope, identityService, apiService, notifierService) {
            $scope.posts = [];

            $scope.init = function () {
                if (!identityService.isLoggedIn()) {
                    $scope.redirectToLogin();
                } else {
                    var config = {
                        headers: identityService.getSecurityHeaders()
                    };
                    apiService.get("/api/posts/", config).success(function (result) {
                        if (result) {
                            $scope.posts = result;
                            console.log($scope.posts);
                        }
                    });
                }
            }();

            $scope.addPost = function (post) {
                $scope.addPostSubmitted = true;
                if ($scope.AddPostForm.$valid) {
                    var config = {
                        headers: identityService.getSecurityHeaders()
                    };
                    apiService.post("/api/posts", post, config).success(function (result) {
                        console.log(result);
                        $scope.posts.splice(0, 0, result);
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

            $scope.toggleLike = function (post) {
                var config = {
                    headers: identityService.getSecurityHeaders(),
                    params: { id: post.id }
                };

                if (post.likedByMe) {

                    post.likedByMe = false;
                    post.likeCount--;

                    apiService.remove("/api/like", config).success(function (result) {
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

                    post.likedByMe = true;
                    post.likeCount++;

                    apiService.post("/api/like", {}, config).success(function (result) {
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

            $scope.addComment = function (post, newComment) {
               
                newComment.postId = post.id;
                var config = {
                    headers: identityService.getSecurityHeaders()
                };

                apiService.post("/api/comments", newComment, config).success(function (result) {
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
            };
        }
    ]);
})(_$.app);