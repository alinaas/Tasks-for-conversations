using System.Web.Optimization;

namespace WcfAssignment
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js",
						"~/Scripts/jquery-ui-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/bootstrap.css",
					  "~/Content/site.css",
					  "~/Content/themes/base/theme.css",
					  "~/Content/themes/base/datepicker.css"));

			bundles.Add(new ScriptBundle("~/bundles/matchbasicdata.js").Include(
						"~/Scripts/matchbasicdata.js"));
		}
	}
}
