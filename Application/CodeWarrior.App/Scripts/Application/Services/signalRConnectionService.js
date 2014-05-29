(function (app) {
    app.factory("signalRConnectionService", function () {
        return {
            getSignalRConnection: function (accessToken) {
                var conn = $.connection.signalRNotification;
                $.connection.hub.qs = { "token": accessToken };
                $.connection.hub.logging = true;
                $.connection.hub.start();
                return conn;
            }
        }
    });
})(_$.app);