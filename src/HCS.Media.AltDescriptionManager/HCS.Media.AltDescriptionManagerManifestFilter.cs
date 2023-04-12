using Umbraco.Cms.Core.Manifest;

namespace HCS.Media.AltDescriptionManager
{
    internal class ManifestFilter : IManifestFilter
    {
        private const string Base = "/App_Plugins/HCS.Media.AltDescriptionManager/";
        public void Filter(List<PackageManifest> manifests)
        {
            var assembly = typeof(ManifestFilter).Assembly;

            manifests.Add(new PackageManifest
            {
                PackageName = "HCS.Media.AltDescriptionManager",
                Version = assembly.GetName()?.Version?.ToString(3) ?? "0.1.0",
                AllowPackageTelemetry = true,
                Scripts = new string[] {
                    Base + "HCS.Media.AltDescriptionManagerController.js"
                },
                Stylesheets = new string[]
                {
                    Base + "HCS.Media.AltDescriptionManager.css"
                }
            });
        }
    }
}
