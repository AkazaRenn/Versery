using Model.Server.Entities;
using Model.Server.Entities.Admin;
using Model.Server.Methods;
using Model.Server.Methods.Admin;
using Refit;
using System.Globalization;
using System.Reflection;

namespace Model.Server;

internal sealed class UrlParameterFormatter: IUrlParameterFormatter {
    public string? Format(object? parameterValue, ICustomAttributeProvider attributeProvider, Type type) {
        if (parameterValue is null) {
            return null;
        }

        return parameterValue switch {
            Entities.Admin.DomainBlockSeverity x => x.ToJsonString(),
            Entities.Admin.IpBlockSeverity x => x.ToJsonString(),
            Entities.Admin.CohortFrequency x => x.ToJsonString(),

            Entities.FeatureApprovalPolicy x => x.ToJsonString(),
            Entities.MediaAttachmentMetaFocus x => $"{x?.X ?? 0},{x?.Y ?? 0}",
            Entities.NotificationPolicyFilter x => x.ToJsonString(),
            Entities.NotificationType x => x.ToJsonString(),
            Entities.ReportCategory x => x.ToJsonString(),
            Entities.StatusVisibility x => x.ToJsonString(),

            Methods.Admin.AccountsOrigin x => x.ToJsonString(),
            Methods.Admin.AccountsStatus x => x.ToJsonString(),
            Methods.Admin.AccountsPermissions x => x.ToJsonString(),
            Methods.Admin.AccountActionType x => x.ToJsonString(),
            Methods.Admin.DimensionKey x => x.ToJsonString(),
            Methods.Admin.MeasureKey x => x.ToJsonString(),

            Methods.ListsRepliesPolicy x => x.ToJsonString(),
            Methods.PushPolicy x => x.ToJsonString(),
            Methods.SearchType x => x.ToJsonString(),
            Methods.StreamingStream x => x.ToJsonString(),
            Methods.StreamingType x => x.ToJsonString(),

            CultureInfo x => x.TwoLetterISOLanguageName,
            DateOnly x => x.ToString("yyyy-MM-dd"),
            DateTime x => x.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'"),
            Uri x => x.OriginalString,
            _ => parameterValue.ToString()
        };
    }
}
