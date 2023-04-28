namespace HCS.Media.DescriptionManager.Core.Models;

public class OptionsModel
{    
    public const string Key = "Hcs:Media";

    public Dictionary<string,string> DescriptionManager { get; set; } = new();
    
}
