namespace HCS.Media.DescriptionManager.Core;

public interface IDescriptionManagerService
{
    Task<List<object>> GetMediaWithMissingDescriptions();
    Task<bool> SaveDescription(Guid mediaId, string description);
}

