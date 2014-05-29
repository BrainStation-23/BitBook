"use strict";

(function (app) {
    app.controller("QuestionAllCtrl", [
        "$scope", "signalRConnectionService", "identityService", function ($scope, signalRConnectionService, identityService) {
            var signalRConnection;
            $scope.newQuestion = 0;
            $scope.questions = [];

            identityService.getUserInfo().success(function (result) {
                signalRConnection = signalRConnectionService.getSignalRConnection(result.accessToken);
                console.log(signalRConnection);
            });

            signalRConnection.client.newQuestionAdded = function onNewMessage(title, userName) {
                var newQuestion = {
                    title: title,
                    userName: userName
                };
                $scope.newQuestion = ++$scope.newQuestion;
                $scope.questions.push(newQuestion);
                $scope.$apply();
                console.log(newQuestion);
            };
            $scope.clickInQuestion = function(question) {
                signalRConnection.server.sendMessageByUserId(question.userName);
            };
        }
    ]);
})(_$.app);