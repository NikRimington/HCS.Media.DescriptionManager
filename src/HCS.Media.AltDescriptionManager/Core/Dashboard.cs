using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Dashboards;
using Umbraco.Extensions;

namespace HCS.Media.AltDescriptionManager.Core;

[Weight(20)]
public class Dashboard : IDashboard
{
    public string[] Sections => new[] { Umbraco.Cms.Core.Constants.Applications.Media };

    public IAccessRule[] AccessRules => new[] {
        new AccessRule {Type = AccessRuleType.Grant, Value = Umbraco.Cms.Core.Constants.Security.AdminGroupAlias},
        new AccessRule {Type = AccessRuleType.Grant, Value = Umbraco.Cms.Core.Constants.Security.EditorGroupAlias}
    };

    public string Alias => "HCS.Media.AltDescriptionManager";

    public string View => "/App_Plugins/HCS.Media.AltDescriptionManager/dashboard.html";

}
