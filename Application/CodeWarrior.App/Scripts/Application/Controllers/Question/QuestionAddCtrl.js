"use strict";

(function(app) {
    app.controller("QuestionAddCtrl", [
        "$scope", "signalRConnectionService", function($scope, signalRConnectionService) {
            var conn = signalRConnectionService.getSignalRConnection();
            console.log(conn);
        }
    ]);
})(_$.app);