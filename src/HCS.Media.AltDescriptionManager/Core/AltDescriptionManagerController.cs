using HCS.Media.AltDescriptionManager.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Web.BackOffice.Controllers;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Extensions;

namespace HCS.Media.AltDescriptionManager.Core;

[PluginController(Constants.Area)]
public class AltDescriptionManagerController : UmbracoAuthorizedApiController
{
    private readonly ILogger<AltDescriptionManagerController> _logger;
    private readonly IAltDescriptionManagerService _altDescriptionManagerService;

    public AltDescriptionManagerController(ILogger<AltDescriptionManagerController> logger, IAltDescriptionManagerService altDescriptionManagerService)
    {
        _logger = logger;
        _altDescriptionManagerService = altDescriptionManagerService;
    }

    [HttpGet]
    public async Task<object> Fetch()
    {
        return new {
            total = 50,
            items = new [] {
                new {
                    key = Guid.NewGuid(),
                    name = "abc",
                    url = "https://placedog.net/1447/?random"
                },
                new {
                    key = Guid.NewGuid(),
                    name = "def",
                    url = "https://placedog.net/1337/?random"
                }
            }
        };
    }


    [HttpPost]
    public async Task<object> Save(SaveRequestModel model)
    {
        if((model?.Description).IsNullOrWhiteSpace())
            return new BadRequestObjectResult(new { message = "No description" });

        return new {
            id = model.Index
        };
    }

    [HttpGet]
    public object Ping()
    {
        return Ok();
    }
}

