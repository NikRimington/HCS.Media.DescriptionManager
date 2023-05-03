using HCS.Media.DescriptionManager.Core.Models;

namespace HCS.Media.DescriptionManager.Core;

public interface IDescriptionManagerService
{
    Task<List<MediaItem>> GetMediaWithMissingDescriptions();
    bool SaveDescription(int mediaId, string description);
}

