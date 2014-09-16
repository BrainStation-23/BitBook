(function (app) {
    app.factory("signalRConnectionService", function () {
        return {
            getSignalRConnection: function () {
                var conn = $.connection.signalRNotification;
                $.connection.hub.logging = false;
                $.connection.hub.start();
                return conn;
            }
        }
    });
})(_$.app);