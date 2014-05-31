using System.Web.Optimization;

namespace CodeWarrior.App
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/library").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.signalR-2.0.3.js",
                "~/Scripts/underscore.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js",
                "~/Scripts/toastr.js",
                "~/Scripts/angular.js",
                "~/Scripts/angular-route.js",
                "~/Scripts/angular-resource.js"));

            bundles.Add(new ScriptBundle("~/bundles/application").Include(
                "~/Scripts/Application/app.js",
                "~/Scripts/Application/Services/apiService.js",
                "~/Scripts/Application/Services/identityService.js",
                "~/Scripts/Application/Services/utilityService.js",
                "~/Scripts/Application/Services/notifierService.js",
                "~/Scripts/Application/Services/signalRConnectionService.js",
                "~/Scripts/Application/Services/friendService.js",
                "~/Scripts/Application/Directives/topbarDirective.js",
                "~/Scripts/Application/Directives/ExternalLoginList.js",
                "~/Scripts/Application/Controllers/BaseCtrl.js",
                "~/Scripts/Application/Controllers/Post/PostCtrl.js",
                "~/Scripts/Application/Controllers/Account/RegisterCtrl.js",
                "~/Scripts/Application/Controllers/Account/ExternalRegisterCtrl.js",
                "~/Scripts/Application/Controllers/Account/LoginCtrl.js",
                "~/Scripts/Application/Controllers/Account/ProfileCtrl.js",
                "~/Scripts/Application/Controllers/Account/ProfileEditCtrl.js",
                "~/Scripts/Application/Controllers/Account/PasswordManageCtrl.js",
                "~/Scripts/Application/Controllers/Account/AccountFriendListCtrl.js",
                "~/Scripts/Application/Controllers/Friend/FriendProfileCtrl.js",
                "~/Scripts/Application/Controllers/Account/PendingFriendRequestCtrl.js",
                "~/Scripts/Application/Controllers/Question/QuestionAddCtrl.js",
                "~/Scripts/Application/Controllers/Question/QuestionListCtrl.js",
                "~/Scripts/Application/Controllers/Post/PostAddCtrl.js",
                "~/Scripts/Application/Controllers/Search/UserSearchCtrl.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/toastr.css",
                "~/Content/font-awesome.css",
                "~/Content/site.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}