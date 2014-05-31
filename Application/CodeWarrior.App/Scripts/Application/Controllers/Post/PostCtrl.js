"use strict";

(function (app) {
    app.controller("PostCtrl", [
        "$scope", "identityService", "apiService", "notifierService", "$rootScope", "signalRConnectionService", function ($scope, identityService, apiService, notifierService, $rootScope, signalRConnectionService) {
            $scope.posts = [];
            var signalRConnection = signalRConnectionService.getSignalRConnection();
            var userFriends = [];
            $scope.init = function () {
                if (!identityService.isLoggedIn()) {
                    $scope.redirectToLogin();
                } else {
                    $scope.postFetchInProgresss = true;
                    var config = {
                        headers: identityService.getSecurityHeaders()
                    };
                    apiService.get("/api/posts/", config).success(function (result) {
                        $scope.postFetchInProgresss = false;
                        if (result) {
                            $scope.posts = result;
                        }
                    });
                    apiService.get("/api/friends/", config).success(function (result) {
                        result.forEach(function(item) {
                            userFriends.push(item.id);
                        });
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
                        post.message = "";
                        signalRConnection.server.newPostAdded(result, userFriends);
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

                    var notificationMessage = post.postedBy.firstName + " " + post.postedBy.lastName + " likes your post";

                    if (!$scope.isPostedBySameObject(post)) {
                        signalRConnection.server.likeInMyPost(notificationMessage, post.postedBy.id);
                    }

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
            
            $scope.addComment = function (post) {
                post.newComment = post.newComment || {};
                post.newComment.postId = post.id;
                var config = {
                    headers: identityService.getSecurityHeaders()
                };
                var newComment = post.newComment;
                apiService.post("/api/comments", newComment, config).success(function (result) {
                    notifierService.notify({responseType: "success", message: "Comment posted successfully!"});
                    newComment.description = "";
                    post.comments.push(result);

                    var notificationMessage = post.postedBy.firstName + " " + post.postedBy.lastName + " comments in your post";
                   
                    if (!$scope.isPostedBySameObject(post)) {
                        signalRConnection.server.commentInMyPost(notificationMessage, post.postedBy.id);
                    }

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

            $scope.isPostedBySameObject = function (post) {
                return post.postedBy.id == $rootScope.authenticatedUser.id;
            };

            $scope.removePost = function (post,index) {

                if (!confirm("Are you sure?"))
                    return;

                var config = {
                    headers: identityService.getSecurityHeaders(),
                    params: { id: post.id }
                };

                apiService.remove("/api/posts", config).success(function (result) {
                    $scope.posts.splice(index, 1);
                    notifierService.notify({responseType:"success",message:"Post removed successfully."});
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

            signalRConnection.client.updateMyFeed = function (post, users) {
                console.log(post);
                if (users.indexOf($rootScope.authenticatedUser.id) >= 0) {
                    $scope.posts.splice(0, 0, post);
                    $scope.$apply();
                }
            };
        }
    ]);
})(_$.app);