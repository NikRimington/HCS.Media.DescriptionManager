using Umbraco.Cms.Core.Manifest;

namespace HCS.Media.DescriptionManager
{
    internal class ManifestFilter : IManifestFilter
    {
        private const string Base = "/App_Plugins/HCS.Media.DescriptionManager/";
        public void Filter(List<PackageManifest> manifests)
        {
            var assembly = typeof(ManifestFilter).Assembly;

            manifests.Add(new PackageManifest
            {
                PackageName = "HCS.Media.DescriptionManager",
                Version = assembly.GetName()?.Version?.ToString(3) ?? "0.1.0",
                AllowPackageTelemetry = true,
                Scripts = new string[] {
                    Base + "HCS.Media.DescriptionManagerController.js"
                },
                Stylesheets = new string[]
                {
                    Base + "HCS.Media.DescriptionManager.css"
                }
            });
        }
    }
}
