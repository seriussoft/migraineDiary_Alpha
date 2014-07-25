using System.Web;
using System.Web.Optimization;
using BundleTransformer.Core.Transformers;
using MigraineDiaryMVC.Helpers;

namespace MigraineDiaryMVC
{
	public class BundleConfig
	{
		#region Core

		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles)
		{
			BundleTable.EnableOptimizations = true;

			RegisterScriptBundles(bundles);
			RegisterContentBundles(bundles);

			var bundless = BundleTable.Bundles.GetRegisteredBundles();

		}

		private static void RegisterContentBundles(BundleCollection bundles)
		{
			bundles.Add(new StyleBundle("~/bundles/Content/css").Include("~/Content/site.css"));//.IncludeDirectory("~/Content/specificCss", "*.css", true));
			
			bundles.Add(new StyleBundle("~/bundles/ContentThemes/chosen").Include("~/Content/themes/chosen/chosen.css"));

			AddLessBundles(bundles);

			RegisterJQueryContentBundles(bundles);
			RegisterKendoContentBundles(bundles);
			RegisterBootstrapContentBundles(bundles);
		}

		private static void AddLessBundles(BundleCollection bundles)
		{
			//bundles.UseCdn = true;

			var nullBuilder = new BundleTransformer.Core.Builders.NullBuilder();
			var cssTransformer = new BundleTransformer.Core.Transformers.CssTransformer();
			
			//var lessBundle = new LessBundle("~/bundles/less").IncludeDirectory("~/Content/specificLess", "*.less", false);
			var lessBundle = new RelaxedBundle("~/bundles/less")
														//.Include("~/Content/specificLess/ErrorsAndWarnings.less")
														.IncludeDirectory("~/Content/specificLess/", "*.less");

			lessBundle.Builder = nullBuilder;
			lessBundle.Transforms.Add(cssTransformer);
			

			bundles.Add(lessBundle);
		}

		private static void RegisterScriptBundles(BundleCollection bundles)
		{
			RegisterJQueryScriptBundles(bundles);

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			RegisterModernizrAndDefaultScriptBundles(bundles);

			RegisterKendoScriptBundles(bundles);

			RegisterBootstrapScriptBundles(bundles);

      RegisterGenericScriptBundles(bundles);
		}

		

		#endregion	Core

		#region Register Helper Methods

		private static void RegisterModernizrAndDefaultScriptBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));
			bundles.Add(new ScriptBundle("~/bundles/chosenjquery").Include("~/Content/themes/chosen/chosen.jquery.js"));
			bundles.Add(new ScriptBundle("~/bundles/chosenjquerymin").Include("~/Content/themes/chosen/chosen.jquery.min.js"));
		}

		#region JQuery
		private static void RegisterJQueryScriptBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
									"~/Scripts/jquery-{version}.js", new CssRewriteUrlTransform()));

			bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
									"~/Scripts/jquery-ui-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
									"~/Scripts/jquery.unobtrusive*",
									"~/Scripts/jquery.validate*"));
		}

		private static void RegisterJQueryContentBundles(BundleCollection bundles)
		{
			bundles.Add(new StyleBundle("~/bundles/ContentThemes/base/css").Include(
									"~/Content/themes/base/jquery.ui.core.css",
									"~/Content/themes/base/jquery.ui.resizable.css",
									"~/Content/themes/base/jquery.ui.selectable.css",
									"~/Content/themes/base/jquery.ui.accordion.css",
									"~/Content/themes/base/jquery.ui.autocomplete.css",
									"~/Content/themes/base/jquery.ui.button.css",
									"~/Content/themes/base/jquery.ui.dialog.css",
									"~/Content/themes/base/jquery.ui.slider.css",
									"~/Content/themes/base/jquery.ui.tabs.css",
									"~/Content/themes/base/jquery.ui.datepicker.css",
									"~/Content/themes/base/jquery.ui.progressbar.css",
									"~/Content/themes/base/jquery.ui.theme.css"));
		} 
		#endregion

		#region Kendo
		private static void RegisterKendoScriptBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/scripts/kendo").Include("~/Scripts/kendo/2012.3.1315/kendo.web.*", "~/Scripts/kendo/2012.3.1315/kendo.aspnetmvc.*", "~/Scripts/kendo/2012.3.1315/kendo.web.min.js"));
		}

		private static void RegisterKendoContentBundles(BundleCollection bundles)
		{
			bundles.Add(new StyleBundle("~/bundles/kendoContent")
				.Include
				(
					//"~/Content/kendo/2012.3.1315/kendo.common.*"
					"~/Content/kendo/2012.3.1315/kendo.common.min.css",
					"~/Content/kendo/2012.3.1315/kendo.metro.min.css",
					"~/Content/kendo/2012.3.1315/kendo.default.min.css"
					//,"~/Content/kendo/2012.3.1315/kendo.default.*"
					//,"~/Content/kendo/2012.3.1315/kendo.blueopal.*"
				));

			//bundles.Add(new ScriptBundle("~bundles/kendo").Include("~/Scripts/Kendo/kendo.web.*", "~/Scripts/Kendo/kendo.aspnetmvc.*"));
			//bundles.Add(new StyleBundle("~/Content/kendo").Include("~/Content/themes/Kendo/kendo.common.*", "~/Content/themes/Kendo/kendo.default.*"));

			//bundles.IgnoreList.Clear();

			//bundles.IgnoreList.Ignore("*.intellisense.js");
			//bundles.IgnoreList.Ignore("*-vsdoc.js");
			//bundles.IgnoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
		} 
		#endregion

		#region Bootstrap

		private static void RegisterBootstrapScriptBundles(BundleCollection bundles)
		{
			bundles.Add
			(
				new ScriptBundle("~/bundles/scripts/twitter-bootstrap")
					.IncludeDirectory("~/Scripts/bootstrap/", "*.min.js")
					//.Include("~/Scripts/bootstrap/gcal.js")
			);
		}

		private static void RegisterBootstrapContentBundles(BundleCollection bundles)
		{
			bundles.Add
			(
				new StyleBundle("~/bundles/content/twitter-bootstrap")
					.IncludeDirectory("~/Content/bootstrap", "*.min.css")
			);
		}

		#endregion	Bootstrap

    #region Generic

    private static void RegisterGenericScriptBundles(BundleCollection bundles)
    {
      bundles.Add
      (
        new ScriptBundle("~/bundles/scripts/generic")
            .IncludeDirectory("~/Scripts/Generic", "*.js")
      );
    }

    #endregion  Generic

    #endregion	Register Helper Methods
  }
}