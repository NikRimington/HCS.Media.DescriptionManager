using HCS.Media.DescriptionManager.Core;
using HCS.Media.DescriptionManager.Core.Models;
using HCS.Media.DescriptionManager.Core.Startup;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;

namespace HCS.Media.DescriptionManager
{
    internal class Composer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.AddNotificationHandler<ServerVariablesParsingNotification, VariableParserNotificationHandler>();
            builder.AddNotificationHandler<TreeNodesRenderingNotification, MediaTreeRenderingNotificationHandler>();

            builder.Services.AddScoped<IDescriptionManagerService, DescriptionManagerService>();
            builder.Services.AddOptions<OptionsModel>()
                .Bind(builder.Config.GetSection(OptionsModel.Key));

            builder.ManifestFilters().Append<ManifestFilter>();

            
        }
    }
}
