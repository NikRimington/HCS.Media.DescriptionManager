using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;

namespace HCS.Media.AltDescriptionManager.Core;

public class AltDescriptionManagerComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.AddOptions<OptionsModel>()
            .Bind(builder.Config.GetSection(OptionsModel.Key));

        builder.AddNotificationHandler<TreeNodesRenderingNotification, MediaTreeRenderingNotificationHandler>();
    }
}
