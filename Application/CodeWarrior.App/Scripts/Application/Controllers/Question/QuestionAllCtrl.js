"use strict";

(function (app) {
    app.controller("QuestionAllCtrl", [
        "$scope", "signalRConnectionService", function ($scope, signalRConnectionService) {
            var signalRConnection = signalRConnectionService.getSignalRConnection();
            console.log(signalRConnection);

            signalRConnection.client.newQuestion = function onNewMessage(question) {
                $scope.questions.push(question);
                $scope.$apply();
                console.log(question);
            };
        }
    ]);
})(_$.app);