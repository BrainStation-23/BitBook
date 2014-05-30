"use strict";

(function(app) {
    app.controller("QuestionListCtrl", [
        "$scope", "signalRConnectionService", function($scope, signalRConnectionService) {
            var signalRConnection = signalRConnectionService.getSignalRConnection();
            console.log(signalRConnection);
            $scope.newQuestion = 0;
            $scope.questions = [];
            signalRConnection.client.newQuestionAdded = function onNewMessage(question,userName) {
                $scope.newQuestion = ++$scope.newQuestion;
                var newQuestion = {
                    title: question,
                    userName:userName
                };
                $scope.questions.push(newQuestion);
                $scope.$apply();
            };

            $scope.clickInQuestion = function(question) {
                signalRConnection.server.sendMessageByUserId(question.userName);
            };
        }
    ]);
})(_$.app);