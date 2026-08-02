using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/apps/">Mastodon API Documentation</see>
public interface IApps {
    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/apps/#create">Mastodon API Documentation</see>
    [Post("/api/v1/apps")]
    Task<CredentialApplication> Create(
        [AliasAs("client_name")] string clientName,
        [AliasAs("redirect_uris")] IEnumerable<string> redirectUris,
        [AliasAs("scopes")] string? scopes = null,
        [AliasAs("website")] string? website = null
    );

    /// <summary>
    /// Version: 2.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/apps/#verify_credentials">Mastodon API Documentation</see>
    [Get("/api/v1/apps/verify_credentials")]
    Task<Application> VerifyCredentials();
}
