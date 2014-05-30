﻿"use strict";

var _$ = _$ || {};

(function() {

    var app = _$.app = angular.module("bitBookApp", ["ngRoute", "ngResource"]);

    app.config([
        "$routeProvider", "$locationProvider", function($routeProvider) {
            $routeProvider.when("/", {
                    redirectTo: "/account/login"
                }).when(
                    "/home", {
                        templateUrl: "Templates/Home/Index.html",
                        controller: "HomeCtrl"
                    }).when(
                    "/account/login", {
                        templateUrl: "Templates/Account/Login.html",
                        controller: "LoginCtrl"
                    }).when(
                    "/account/register", {
                        templateUrl: "Templates/Account/Register.html",
                        controller: "RegisterCtrl"
                    }).when(
                    "/account/externalRegister", {
                        templateUrl: "Templates/Account/ExternalRegister.html",
                        controller: "ExternalRegisterCtrl"
                    }).when(
                    "/account/profile", {
                        templateUrl: "Templates/Account/Profile.html",
                        controller: "ProfileCtrl"
                    }).when(
                    "/account/profile/edit", {
                        templateUrl: "Templates/Account/Edit.html",
                        controller: "ProfileEditCtrl"
                    }).when(
                    "/questions/add", {
                        templateUrl: "Templates/Question/Add.html",
                        controller: "QuestionAddCtrl"
                    }).when(
                    "/questions/allQuestions", {
                        templateUrl: "Templates/Question/all.html",
                        controller: "QuestionListCtrl"
                    }).when(
                    "/posts/add", {
                        templateUrl: "Templates/Post/Add.html",
                        controller: "PostAddCtrl"
                    })
                .otherwise({ redirectTo: "/" });
        }
    ]);
    app.run([
        "$rootScope", "$timeout", "$location", "identityService", "utilityService",
        function($rootScope, $timeout, $location, identityService, utilityService) {
            var fired = false;
            $rootScope.$on("$locationChangeStart", function(event) {

                var fragment = utilityService.getFragment(),
                    externalAccessToken,
                    externalError,
                    loginUrl;

                if (fragment["/access_token"]) {
                    fragment.access_token = fragment["/access_token"];
                    event.preventDefault();
                }

                if (fired) return;
                fired = true;
                $timeout(function() { fired = false; }, 10);

                identityService.restoreSessionStorageFromLocalStorage();
                identityService.verifyStateMatch(fragment);

                if (typeof (fragment.error) !== "undefined") {
                    utilityService.cleanUpLocation();
                    $scope.redirectToLogin();

                } else if (typeof (fragment.access_token) !== "undefined") {

                    utilityService.cleanUpLocation();
                    identityService.getUserInfo(fragment.access_token).success(function (data) {

                        if (typeof (data.userName) !== "undefined" && typeof (data.hasRegistered) !== "undefined" && typeof (data.loginProvider) !== "undefined") {
                            if (data.hasRegistered) {

                                identityService.setAuthorizedUserData(data);
                                identityService.setAccessToken(fragment.access_token, false);
                                $scope.redirectToHome();

                            } else if (typeof (sessionStorage["loginUrl"]) !== "undefined") {

                                loginUrl = sessionStorage["loginUrl"];
                                sessionStorage.removeItem("loginUrl");

                                var externalRegister = {
                                    data: data,
                                    fragment: fragment,
                                    loginUrl: loginUrl
                                };

                                sessionStorage.setItem("ExternalRegister", JSON.stringify(externalRegister));
                                $location.path("/account/externalRegister");
                            } else {
                                $scope.redirectToLogin();
                            }
                        } else {
                            $scope.redirectToLogin();
                        }
                    }).error(function() {
                        $scope.redirectToLogin();
                    });
                } else {
                    if (sessionStorage["accessToken"] || localStorage["accessToken"]) {
                        identityService.getUserInfo().success(function(result) {
                            if (result.userName) {
                                identityService.setAuthorizedUserData(result);
                            } else {
                                $scope.redirectToLogin();
                            }
                        });
                    }
                }
            });
        }
    ]);

})();