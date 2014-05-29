﻿"use strict";

(function(app) {
    app.controller("ExternalRegisterCtrl", [
        "$scope", "$location", "identityService", "apiService", function($scope, $location, identityService, apiService) {
            $scope.init = function() {
                $scope.externalRegisterInfo = JSON.parse(sessionStorage.getItem("ExternalRegister"));
                if (!$scope.externalRegisterInfo) {
                    $location.path("/");
                }
            }();

            $scope.registerExternal = function() {
                if ($scope.externalRegisterInfo) {
                    var config = {
                        headers: identityService.getAuthorizedHeaders($scope.externalRegisterInfo.fragment.access_token)
                    };

                    apiService.post("/api/Account/RegisterExternal", { userName: $scope.externalRegisterInfo.data.userName }, config).success(function() {
                        sessionStorage["state"] = $scope.externalRegisterInfo.fragment.state;
                        identityService.archiveSessionStorageToLocalStorage();
                        sessionStorage.removeItem("ExternalRegister");
                        window.location = $scope.externalRegisterInfo.loginUrl;
                    }).error(function(result) {
                        if (result.modelState) {
                            $scope.externalRegisterErrors = _.flatten(_.map(result.modelState, function(items) {
                                return items;
                            }));
                        }
                    });
                }
            };
        }
    ]);
})(_$.app);