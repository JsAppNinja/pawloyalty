using System.Web;
using System.Web.Optimization;

namespace Paw.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                //"~/Content/assets/global/plugins/jquery.min.js",
                "~/Scripts/jquery.validate*",
                "~/Scripts/jquery.unobtrusive-ajax.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.js"));
                      //"~/Scripts/respond.js",
                      //"~/Scripts/jquery.auto-complete-custom.js"));



            bundles.Add(new StyleBundle("~/content/assets/global/css/bundle").Include(
                // Required (These don't run as bundle because of relative links)
                        //"~/Content/assets/global/plugins/font-awesome/css/font-awesome.css",
                        //"~/Content/assets/global/plugins/simple-line-icons/simple-line-icons.css",
                        //"~/Content/assets/global/plugins/bootstrap/css/bootstrap.css",
                        //"~/Content/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.css",

                // Global Styles
                        //"~/Content/assets/global/css/components.css",
                        //"~/Content/assets/global/css/plugins.css",

                        //"~/assets/global/plugins/fullcalendar/fullcalendar.css",
                        //"~/Content/jquery.auto-complete.css",

                // Theme Layout Sytles
                        //"~/Content/assets/layouts/layout4/css/layout.css",
                        //"~/Content/assets/layouts/layout4/css/themes/light.css",
                        //"~/Content/assets/layouts/layout4/css/custom.css",

                        // controls
                        "~/Content/styles/fm.selectator.css",
                        "~/Content/styles/chosen.css",

                        // Custom
                        "~/Content/styles/site.css"
                        ));

            bundles.Add(new StyleBundle("~/bundles/scripts").Include(
                //BEGIN CORE PLUGINS -->
                    "~/Content/assets/global/plugins/jquery.js",
                    "~/Content/assets/global/plugins/bootstrap/js/bootstrap.js",
                    "~/Content/assets/global/plugins/js.cookie.js",
                    "~/Content/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.js",
                    "~/Content/assets/global/plugins/jquery.blockui.js",
                    "~/Content/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.js",
                    "~/Scripts/js-cookie/js.cookie-2.2.0.min.js",

                //BEGIN PAGE LEVEL PLUGINS-- >
                    "~/Content/assets/global/plugins/moment.js",
                    // "~/Content/assets/global/plugins/fullcalendar/fullcalendar.js",
                    "~/Content/assets/global/plugins/jquery-ui/jquery-ui.js",

                //BEGIN THEME GLOBAL SCRIPTS-- >
                    "~/Content/assets/global/scripts/app.js",
                    "~/Scripts/paw.js",

                //BEGIN PAGE LEVEL SCRIPTS-- >
                    "~/Content/assets/apps/scripts/calendar.js",

                    //BEGIN THEME LAYOUT SCRIPTS-- >
                    //"~/Content/assets/layouts/layout4/scripts/layout.js",
                    "~/Scripts/layout.js",
                    "~/Content/assets/layouts/layout4/scripts/demo.js",
                    "~/Content/assets/layouts/global/scripts/quick-sidebar.js",
                    "~/Content/assets/layouts/global/scripts/quick-nav.js",
                    "~/Scripts/fm.selectator.js",
                    "~/Scripts/chosen.jquery.js"));

        }
    }
}
