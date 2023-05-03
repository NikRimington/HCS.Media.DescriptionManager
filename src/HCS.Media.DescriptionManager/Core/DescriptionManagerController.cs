using HCS.Media.DescriptionManager.Core.Exceptions;
using HCS.Media.DescriptionManager.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Web.BackOffice.Controllers;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Extensions;

namespace HCS.Media.DescriptionManager.Core;

[PluginController(Constants.Area)]
public class DescriptionManagerController : UmbracoAuthorizedApiController
{
    private readonly ILogger<DescriptionManagerController> _logger;
    private readonly IDescriptionManagerService _DescriptionManagerService;
    private readonly IMemoryCache _memoryCache;

    private const string cacheKey = Constants.Area + "mediaItemCache";

    public DescriptionManagerController(ILogger<DescriptionManagerController> logger, IDescriptionManagerService DescriptionManagerService, IMemoryCache memoryCache)
    {
        _logger = logger;
        _DescriptionManagerService = DescriptionManagerService;
        _memoryCache = memoryCache;
    }

    [HttpGet]
    public async Task<object> Fetch()
    {
        

        if (!_memoryCache.TryGetValue(cacheKey, out List<MediaItem> items) || items.Count == 0)
        {
            items = await _DescriptionManagerService.GetMediaWithMissingDescriptions();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

            _memoryCache.Set(cacheKey, items, cacheEntryOptions);
        }
        
        var total = items.Count;
        var page = items.Take(25).ToList();

        return new {
            total = total,
            items = page
        };
    }


    [HttpPut]
    public async Task<object> Save(SaveRequestModel model)
    {
        if((model?.Description).IsNullOrWhiteSpace())
            return new BadRequestObjectResult(new { message = "No description" });

        if(_DescriptionManagerService.SaveDescription(model.MediaId, model.Description))
        {
            await RemoveFromCache(model.MediaId);
            return new {
                index = model.Index
            };
        }

        throw new HttpResponseException(500, new {
            index = model.Index
        });
    }

    private async Task RemoveFromCache(int mediaId)
    {
        if (!_memoryCache.TryGetValue(cacheKey, out List<MediaItem> items))
        {
            items = await _DescriptionManagerService.GetMediaWithMissingDescriptions();
        }
        else
        {
            items = items.Where(i => i.key != mediaId).ToList();
        }

        var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

        _memoryCache.Set(cacheKey, items, cacheEntryOptions);

    }

    [HttpGet]
    public object Ping()
    {
        return Ok();
    }
}

