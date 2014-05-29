"use strict";

(function (app) {
    app.controller("QuestionAddCtrl", [
        "$scope", "signalRConnectionService", "identityService", function ($scope, signalRConnectionService, identityService) {
            
            var userName = "";
            var signalRConnection;
            $scope.userNotification = 0;
            identityService.getUserInfo().success(function (result) {
                userName = result.userName;
                console.log(result.userName);
                signalRConnection = signalRConnectionService.getSignalRConnection(result.accessToken);
                console.log(signalRConnection);
            });

            $scope.addQuestion = function (question) {
                signalRConnection.server.addQuestionNotification(question.title, userName);
            };
            signalRConnection.client.sendUserNotification = function (message) {
                $scope.userNotification = ++$scope.userNotification;
                console.log(message);
                $scope.$apply();
            };
        }
    ]);
})(_$.app);