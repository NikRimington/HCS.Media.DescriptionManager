namespace HCS.Media.AltDescriptionManager.Core;

public class OptionsModel
{    
    public const string Key = "Hcs:Media";

    public Dictionary<string,string> AltDescriptionManager { get; set; } = new();
    
}
