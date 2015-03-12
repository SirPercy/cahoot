using System.Web;
using System.Web.Optimization;

namespace cahoot
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/content/css").Include(
            "~/content/bootstrap-min.css",
            "~/content/site.css",
            "~/content/pagedlist.css"));

            bundles.Add(new ScriptBundle("~/scripts/js").Include(
                "~/scripts/bootstrap-min.js",
                "~/scripts/bootstrap-datepicker.js",
                "~/scripts/global.js"
                ));
        }
    }
}