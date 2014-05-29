(function(app) {
    app.directive("cdTopbar", function() {
        return {
            restrict: "A",
            templateUrl: "Templates/Partial/Topbar.html"
        };
    });
})(_$.app);