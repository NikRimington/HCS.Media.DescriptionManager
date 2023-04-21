using HCS.Media.AltDescriptionManager.Core.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace HCS.Media.AltDescriptionManager.Core;

public class MediaTreeRenderingNotificationHandler : INotificationHandler<TreeNodesRenderingNotification>
{
    private readonly ILogger<MediaTreeRenderingNotificationHandler> logger;
    private readonly IUmbracoContextAccessor umbracoContextAccessor;
    private readonly OptionsModel options;

    public MediaTreeRenderingNotificationHandler(ILogger<MediaTreeRenderingNotificationHandler> logger, IUmbracoContextAccessor umbracoContextAccessor, IOptions<OptionsModel> options)
    {
        this.logger = logger;
        this.umbracoContextAccessor = umbracoContextAccessor;
        this.options = options.Value;
    }

    public void Handle(TreeNodesRenderingNotification notification)
    {
        if (notification.TreeAlias != Umbraco.Cms.Core.Constants.Trees.Media) return;
        if (!umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext) || umbracoContext?.Media == null) return;

        foreach (var node in notification.Nodes)
        {
            if (node?.Id == null || (int.TryParse(node.Id.ToString(), out var nodeId) && nodeId <= 0)) continue;

            if (!node.AdditionalData.TryGetValue("contentType", out var contentType) || 
                (!(contentType is string typeAlias) || string.IsNullOrWhiteSpace(typeAlias) )) continue;

            if(!options.AltDescriptionManager.TryGetValue(typeAlias, out var propertyAlias) || string.IsNullOrWhiteSpace(propertyAlias)) continue;

            if (umbracoContext.Media.GetById(nodeId) is IPublishedContent mediaItem)
            {                
                node.CssClasses.Add($"hcs-description-check {(mediaItem.HasValue(propertyAlias) ? "__missing" : "__found")}");
            }
        }
    }
}
