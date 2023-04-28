using Microsoft.AspNetCore.Routing;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Extensions;

namespace HCS.Media.DescriptionManager.Core.Startup;

public class VariableParserNotificationHandler : Umbraco.Cms.Core.Events.INotificationHandler<ServerVariablesParsingNotification>
{
    private readonly LinkGenerator _linkGenerator;

    public VariableParserNotificationHandler(LinkGenerator linkGenerator)
    {
        _linkGenerator = linkGenerator;
    }

    public void Handle(ServerVariablesParsingNotification notification)
    {
        var variables = notification.ServerVariables;

        if(!variables.TryGetValue(Constants.Area, out var mediaObject))
            mediaObject = new Dictionary<string, object>();

        if(mediaObject is not Dictionary<string, object> typedMediaObject)
            typedMediaObject = new Dictionary<string, object>();

        if(!typedMediaObject.ContainsKey(Constants.DescriptionApi))
            typedMediaObject.Add(Constants.DescriptionApi,
                _linkGenerator.GetUmbracoApiServiceBaseUrl<DescriptionManagerController>(c => c.Ping()) ?? string.Empty
            );

        variables[Constants.Area] = typedMediaObject;
    }
}
