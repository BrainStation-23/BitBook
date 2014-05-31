"use strict";

(function (app) {
    app.controller("HomeCtrl", [
        "$scope", "identityService", "apiService", "notifierService", function ($scope, identityService, apiService, notifierService) {
            $scope.posts = [];
            $scope.newComment = {};

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
                        $scope.posts.splice(0, 0, result);
                    });
                } else {
                    notifierService.notify({
                        responseType: "error",
                        message: "Invalid input!"
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
                        var data = {
                            responseType: "error"
                        };
                        if (error.modelState) {
                            $scope.postCreateErrors = _.flatten(_.map(error.modelState, function (items) {
                                return items;
                            }));
                            data.message = $scope.postCreateErrors[0];
                        } else {
                            data.message = error.message;
                        }
                        notifierService.notify(data);
                    });
                } else {

                    post.likedByMe = true;
                    post.likeCount++;

                    apiService.post("/api/like", {}, config).success(function (result) {
                    }).error(function (error) {
                        var data = {
                            responseType: "error"
                        };
                        if (error.modelState) {
                            $scope.postCreateErrors = _.flatten(_.map(error.modelState, function (items) {
                                return items;
                            }));
                            data.message = $scope.postCreateErrors[0];
                        } else {
                            data.message = error.message;
                        }
                        notifierService.notify(data);
                    });
                }
            };
            
            $scope.addComment = function (post, newComment) {

                newComment.postId = post.id;
                var config = {
                    headers: identityService.getSecurityHeaders()
                };

                apiService.post("/api/comments", newComment, config).success(function (result) {
                    $scope.newComment.description = "";
                    post.comments.push(result);
                    console.log(result);
                }).error(function (error) {
                    var data = {
                        responseType: "error"
                    };
                    if (error.modelState) {
                        $scope.postCreateErrors = _.flatten(_.map(error.modelState, function (items) {
                            return items;
                        }));
                        data.message = $scope.postCreateErrors[0];
                    } else {
                        data.message = error.message;
                    }
                    notifierService.notify(data);
                });
            };
        }
    ]);
})(_$.app);