using System.Web.Optimization;

namespace HydrusSharp
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/styles/common").Include(
                "~/Core/Styles/bootstrap.min.css"
            ));

            // ScriptBundle doesn't support Bootstrap 5
            // https://stackoverflow.com/a/68712782
            bundles.Add(new Bundle("~/scripts/bootstrap").Include(
                "~/Core/Scripts/Common/bootstrap.bundle.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/scripts/common").Include(
                "~/Core/Scripts/Common/HttpRequester.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/scripts/index").Include(
                "~/Core/Scripts/Providers/ClientProvider.js",
                "~/Core/Scripts/ViewModels/NamedJsonDump.js",
                "~/Core/Scripts/ViewModels/HashedJsonDump.js",
                "~/Core/Scripts/ViewModels/GuiSessionContainerPageSingle.js",
                "~/Core/Scripts/ViewModels/GuiSessionContainerPageNotebook.js",
                "~/Core/Scripts/ViewModels/Query/MediaSort.js",
                "~/Core/Scripts/ViewModels/Query/MediaCollect.js",
                "~/Core/Scripts/ViewModels/Query/SearchPredicate.js",
                "~/Core/Scripts/ViewModels/Query/FileSearchContext.js",
                "~/Core/Scripts/ViewModels/ManagementController.js",
                "~/Core/Scripts/Index.js"
            ));
        }
    }
}
