"use strict";

var _$ = {};

(function() {
    var parseQueryString = function(queryString) {
        var data = {},
            pairs,
            pair,
            separatorIndex,
            escapedKey,
            escapedValue,
            key,
            value;

        if (queryString === null) {
            return data;
        }

        pairs = queryString.split("&");

        for (var i = 0; i < pairs.length; i++) {
            pair = pairs[i];
            separatorIndex = pair.indexOf("=");

            if (separatorIndex === -1) {
                escapedKey = pair;
                escapedValue = null;
            } else {
                escapedKey = pair.substr(0, separatorIndex);
                escapedValue = pair.substr(separatorIndex + 1);
            }

            key = decodeURIComponent(escapedKey);
            value = decodeURIComponent(escapedValue);

            data[key] = value;
        }

        return data;
    }

    var getFragment = function() {
        if (window.location.hash.indexOf("#") === 0) {
            return parseQueryString(window.location.hash.substr(1));
        } else {
            return {};
        }
    };

    var cleanUpLocation = function() {
        window.location.hash = "";

        if (typeof (history.pushState) !== "undefined") {
            history.pushState("", document.title, location.pathname);
        }
    };

    var app =  _$.app = angular.module("codeWarriorApp", ["ngRoute", "ngResource"]);

    app.config([
        "$routeProvider", function($routeProvider) {
            $routeProvider.when("/", {
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
                    })
                .otherwise({ redirectTo: "/" });
        }
    ]);
    app.run(function ($rootScope, $timeout, identityService, $location) {
        var fired = false;
        $rootScope.$on("$locationChangeStart", function(event, next, current) {

            var fragment = getFragment(),
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
                cleanUpLocation();
                $location.path("/account/login");
            } else if (typeof (fragment.access_token) !== "undefined") {
                cleanUpLocation();
                identityService.getUserInfo(fragment.access_token).success(function(data) {
                    if (typeof (data.userName) !== "undefined" && typeof (data.hasRegistered) !== "undefined" && typeof (data.loginProvider) !== "undefined") {
                        if (data.hasRegistered) {
                            data.AccessToken = fragment.access_token;
                            identityService.setAuthorizedUserData(data);
                            identityService.setAccessToken(fragment.access_token);
                            $location.path("/");
                        } else if (typeof (sessionStorage["loginUrl"]) !== "undefined") {
                            loginUrl = sessionStorage["loginUrl"];
                            sessionStorage.removeItem("loginUrl");
                            $location.path("/account/externalRegister");
                            var externalRegister = {
                                data: data,
                                fragment: fragment,
                                loginUrl: loginUrl
                            };
                            sessionStorage.setItem("ExternalRegister", JSON.stringify(externalRegister));
                        } else {
                            $location.path("/account/login");
                        }
                    } else {
                        $location.path("/account/login");
                    }
                }).error(function() {
                    $location.path("/account/login");
                });
            } else {
                if (sessionStorage["accessToken"] || localStorage["accessToken"]) {
                    identityService.getUserInfo().success(function(result) {
                        if (result.userName) {
                            identityService.setAuthorizedUserData(result);
                        } else {
                            $location.path("/account/login");
                        }
                    });
                }
            }
        });
    });
})();