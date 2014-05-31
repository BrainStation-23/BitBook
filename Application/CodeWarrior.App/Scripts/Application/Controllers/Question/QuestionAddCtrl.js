"use strict";

(function(app) {
    app.controller("QuestionAddCtrl", [
        "$scope", "signalRConnectionService", "identityService", function($scope, signalRConnectionService, identityService) {

            var userName = "";
            var signalRConnection;
            $scope.userNotification = 0;

            signalRConnection = signalRConnectionService.getSignalRConnection();
            console.log(signalRConnection);

            $scope.addQuestion = function(question) {
                signalRConnection.server.addQuestionNotification(question.title, userName);
            };
            identityService.getUserInfo().success(function(result) {
                userName = result.userName;
            });
            signalRConnection.client.sendUserNotification = function (message,name) {
                if (userName == name) {
                    $scope.userNotification = ++$scope.userNotification;
                    $scope.$apply();
                }
            };
        }
    ]);
})(_$.app);