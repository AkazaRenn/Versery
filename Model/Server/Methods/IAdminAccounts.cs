using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

public interface IAdminAccounts {
    [Get("/api/v2/admin/accounts")]
    Task<List<AdminAccount>> GetAccounts(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null,
        [AliasAs("origin")] string? origin = null,
        [AliasAs("status")] string? status = null,
        [AliasAs("permissions")] string? permissions = null,
        [AliasAs("invited_by")] string? invitedBy = null,
        [AliasAs("username")] string? username = null,
        [AliasAs("display_name")] string? displayName = null,
        [AliasAs("by_domain")] string? byDomain = null,
        [AliasAs("email")] string? email = null,
        [AliasAs("ip")] string? userIp = null);
}
