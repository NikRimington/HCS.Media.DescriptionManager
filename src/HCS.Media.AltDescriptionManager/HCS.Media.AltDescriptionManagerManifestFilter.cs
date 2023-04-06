using Umbraco.Cms.Core.Manifest;

namespace HCS.Media.AltDescriptionManager
{
    internal class ManifestFilter : IManifestFilter
    {
        public void Filter(List<PackageManifest> manifests)
        {
            var assembly = typeof(ManifestFilter).Assembly;

            manifests.Add(new PackageManifest
            {
                PackageName = "HCS.Media.AltDescriptionManager",
                Version = assembly.GetName()?.Version?.ToString(3) ?? "0.1.0",
                AllowPackageTelemetry = true,
                Scripts = new string[] {
                    // "files here"
                },
                Stylesheets = new string[]
                {
                    "/App_Plugins/HCS.Media.AltDescriptionManager/HCS.Media.AltDescriptionManager.css"
                }
            });
        }
    }
}
