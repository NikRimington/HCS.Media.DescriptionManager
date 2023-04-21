namespace HCS.Media.AltDescriptionManager.Core.Models;

public class SaveRequestModel
{
    public int Index {get;set;}
    public Guid MediaId {get;set;}
    public string? Description {get;set;}
}

