using HCS.Media.DescriptionManager.Core.Exceptions;
using HCS.Media.DescriptionManager.Core.Models;
using Microsoft.AspNetCore.Mvc;
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

    public DescriptionManagerController(ILogger<DescriptionManagerController> logger, IDescriptionManagerService DescriptionManagerService)
    {
        _logger = logger;
        _DescriptionManagerService = DescriptionManagerService;
    }

    [HttpGet]
    public async Task<object> Fetch()
    {
        var items = await _DescriptionManagerService.GetMediaWithMissingDescriptions();
        var total = items.Count;
        var page = items.Take(25).ToList();
        return new {
            total = total,
            pageSize = page.Count,
            items = page.Chunk(3).ToArray()
        };
    }


    [HttpPut]
    public async Task<object> Save(SaveRequestModel model)
    {
        if((model?.Description).IsNullOrWhiteSpace())
            return new BadRequestObjectResult(new { message = "No description" });

        if(await _DescriptionManagerService.SaveDescription(model.MediaId, model.Description))
            return new {
                group = model.GroupIndex,
                index = model.Index
            };

        throw new HttpResponseException(500, new {
            group = model.GroupIndex,
            index = model.Index
        });
    }

    [HttpGet]
    public object Ping()
    {
        return Ok();
    }
}

