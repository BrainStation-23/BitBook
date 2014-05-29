(function (app) {
    app.factory("signalRConnectionService", function () {
        return {
            getSignalRConnection: function () {
                var conn = $.connection.signalRNotification;
                console.log(conn);
                $.connection.hub.logging = true;
                $.connection.hub.start();
                return conn;
            }
        }
    });
})(_$.app);