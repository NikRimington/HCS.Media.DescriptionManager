using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;

namespace HCS.Media.AltDescriptionManager.Core.Startup;

public class HcsMediaComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.AddNotificationHandler<ServerVariablesParsingNotification, VariableParserNotificationHandler>();
    }
}
