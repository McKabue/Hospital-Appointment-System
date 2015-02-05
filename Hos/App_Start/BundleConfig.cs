using System.Web;
using System.Web.Optimization;

namespace Hos
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/otherScripts").Include(
                     // "~/Scripts/bootstrap-select.js",
                      "~/Scripts/select2.min.js",
                      "~/Scripts/knockout-3.2.0.js",
                      "~/Scripts/Hos.Main.js",
                      "~/Scripts/Hos.UX.js",
                      "~/Scripts/jquery.cookie.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                     // "~/Content/bootstrap-select.css",
                      "~/Content/css/select2.css",
                      "~/Content/site.css"));
        }
    }
}
