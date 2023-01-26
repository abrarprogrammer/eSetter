using System.Web;
using System.Web.Optimization;

namespace eSetter
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-{version}.js",
                "~/Scripts/knockout.validation.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/sammy-{version}.js",
                "~/Scripts/app/common.js",
                "~/Scripts/app/app.datamodel.js",
                "~/Scripts/app/app.viewmodel.js",
                "~/Scripts/app/home.viewmodel.js",
                "~/Scripts/app/_run.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/lightbox").Include(
                "~/Scripts/lightbox.js"));

            bundles.Add(new ScriptBundle("~/bundles/all").Include(
                "~/Scripts/jquery_002.js",
                "~/Scripts/fluidvids.js",
                "~/Scripts/switcher.js",
                "~/Scripts/jquery_003.js",
                "~/Scripts/mailing-list.js",
                "~/Scripts/jquery.ihavecookies.js",
                "~/Scripts/scripts.js",
                "~/Scripts/jquery.prettyPhoto.js",
                "~/Scripts/bootstrap-datepicker.min.js",
                "~/Scripts/bootstrap-datepicker3.min.js",
                "~/Scripts/bootstrap-datepicker.mk.min.js",
                "~/Scripts/session.js",
                "~/Scripts/jquery.cookie.js",
                "~/Scripts/typed.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/wide.css",
                 "~/Content/blue.css",
                 "~/Content/responsive.css",
                 "~/Content/prettyPhoto.css",
                 "~/Content/nivo-slider.css",
                 "~/Content/default.css",
                 "~/Content/switcher.css",
                 "~/Content/font-awesome.css",
                 "~/Content/css.css",
                 "~/Content/css_002.css",
                 "~/Content/bootstrap.css",
                 "~/Content/bootstrap-datepicker.css"));

            bundles.Add(new StyleBundle("~/Content/reset").Include("~/Content/reset.css"));
            bundles.Add(new StyleBundle("~/Content/lightbox").Include("~/Content/lightbox.css"));

        }
    }
}
