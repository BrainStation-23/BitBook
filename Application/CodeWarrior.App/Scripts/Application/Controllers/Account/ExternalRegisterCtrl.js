"use strict";

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
                        headers: identityService.getSecurityHeaders($scope.externalRegisterInfo.fragment.access_token)
                    };
                    apiService.post("/api/Account/RegisterExternal", { userName: $scope.externalRegisterInfo.data.userName }, config).success(function() {
                        sessionStorage["state"] = $scope.externalRegisterInfo.fragment.state;
                        identityService.archiveSessionStorageToLocalStorage();
                        window.location = $scope.externalRegisterInfo.loginUrl;
                    }).error(function (result) {
                        console.log(result);
                    });
                }
            };
        }
    ]);
})(_$.app);