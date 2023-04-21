using HCS.Media.AltDescriptionManager.Core.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace HCS.Media.AltDescriptionManager.Core;

public class AltDescriptionManagerService : IAltDescriptionManagerService
{
    private readonly ILogger<AltDescriptionManagerService> _logger;
    private readonly IUmbracoContextAccessor _umbracoContextAccessor;
    private readonly IOptions<OptionsModel> _options;

    public AltDescriptionManagerService(ILogger<AltDescriptionManagerService> logger, 
        IUmbracoContextAccessor umbracoContextAccessor, IOptions<OptionsModel> options)
    {
        _logger = logger;
        _umbracoContextAccessor = umbracoContextAccessor;
        _options = options;
    }
    
    public List<object> GetMediaWithMissingDescriptions()
    {
        var res = new List<object>();
        if (!_umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext) || umbracoContext?.Media == null) return res;
        if(_options.Value.AltDescriptionManager.Count == 0) return res;
        
        foreach(var item in umbracoContext.Media.GetAtRoot(false))
        {
            ProcessMediaItem(res, item);
        }

        return res;

    }

    private void ProcessMediaItem(List<object> res, IPublishedContent item)
    {

        if(!_options.Value.AltDescriptionManager.TryGetValue(item.ContentType.Alias, out var propertyAlias) 
            || string.IsNullOrWhiteSpace(propertyAlias)) return;
        
        if(item.HasValue(propertyAlias)) return;

        res.Add(new {
            key = item.Key,
            name = item.Name,
            url = item.Url()
        });

    }
}

