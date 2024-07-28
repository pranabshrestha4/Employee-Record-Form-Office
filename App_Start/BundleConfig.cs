using System.Web;
using System.Web.Optimization;

namespace EmployeeRecordFourm
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/Content/styles.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                   "~/Content/style1.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                   "~/Content/style2.css"));
        }
    }
}
