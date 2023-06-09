using HCS.Media.DescriptionManager.Core.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace HCS.Media.DescriptionManager.Core;

public class DescriptionManagerService : IDescriptionManagerService
{
    private readonly ILogger<DescriptionManagerService> _logger;
    private readonly IUmbracoContextAccessor _umbracoContextAccessor;
    private readonly IOptions<OptionsModel> _options;
    private readonly IMediaService _mediaService;
    private readonly IBackOfficeSecurityAccessor _backOfficeSecurity;

    public DescriptionManagerService(ILogger<DescriptionManagerService> logger, 
        IUmbracoContextAccessor umbracoContextAccessor, IOptions<OptionsModel> options, 
        IMediaService mediaService, IBackOfficeSecurityAccessor backOfficeSecurity)
    {
        _logger = logger;
        _umbracoContextAccessor = umbracoContextAccessor;
        _options = options;
        _mediaService = mediaService;
        _backOfficeSecurity = backOfficeSecurity;
    }
    
    public async Task<List<MediaItem>> GetMediaWithMissingDescriptions()
    {
        var res = new List<MediaItem>();
        if (!_umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext) || umbracoContext?.Media == null) return res;
        if(_options.Value.DescriptionManager.Count == 0) return res;
        
        foreach(var item in umbracoContext.Media.GetAtRoot(false))
        {
            ProcessMediaItem(res, item);
        }

        return await Task.FromResult(res);

    }

    public bool SaveDescription(int key, string description)
    {
        try
        {           
        
            var item = _mediaService.GetById(key);
            if(item == null) return false;

            if(_options.Value.DescriptionManager.TryGetValue(item.ContentType.Alias, out var propertyAlias))
            {
                item.SetValue(propertyAlias, description);

                var result = _mediaService.Save(item, _backOfficeSecurity.BackOfficeSecurity?.CurrentUser?.Id ?? -1 );

                return result.Success;

            }

        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, "DescriptionManager - unable to update description for media item {key}", key);
        }

        return false;
    }

    private void ProcessMediaItem(List<MediaItem> res, IPublishedContent item)
    {
        
        if(_options.Value.DescriptionManager.TryGetValue(item.ContentType.Alias, out var propertyAlias) 
            && !string.IsNullOrWhiteSpace(propertyAlias)) 
        
            if(item.ContentType.PropertyTypes.Any(p => p.Alias == propertyAlias) && !item.HasValue(propertyAlias)) 

                res.Add(new MediaItem {
                    key = item.Id,
                    name = item.Name,
                    url = item.Url() + "?height=250"
                });
        
        if(item.Children != null && item.Children.Any())

            foreach(var child in item.Children)
                ProcessMediaItem(res, child);

    }
}

