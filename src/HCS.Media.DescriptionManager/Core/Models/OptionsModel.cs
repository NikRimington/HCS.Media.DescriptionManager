namespace HCS.Media.DescriptionManager.Core.Models;

public class OptionsModel
{    
    public OptionsModel()
    {
        DescriptionManager = new Dictionary<string, string>{
            {Umbraco.Cms.Core.Constants.Conventions.MediaTypes.Image, "altDescription"},
            {Umbraco.Cms.Core.Constants.Conventions.MediaTypes.VectorGraphicsAlias, "description"},
            {Umbraco.Cms.Core.Constants.Conventions.MediaTypes.VideoAlias, "title"}
        };
    }
    public const string Key = "Hcs:Media";

    public Dictionary<string,string> DescriptionManager { get; set; }
    
}
