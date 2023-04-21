namespace HCS.Media.AltDescriptionManager.Core.Models;

public class OptionsModel
{    
    public const string Key = "Hcs:Media";

    public Dictionary<string,string> AltDescriptionManager { get; set; } = new();
    
}
