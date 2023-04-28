namespace HCS.Media.DescriptionManager.Core.Models;

public class SaveRequestModel
{
    public int GroupIndex {get;set;}
    public int Index {get;set;}
    public Guid MediaId {get;set;}
    public string? Description {get;set;}
}

