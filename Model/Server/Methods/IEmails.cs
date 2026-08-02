using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/emails/">Mastodon API Documentation</see>
public interface IEmails {
    /// <summary>
    /// Version: 3.4.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/emails/#confirmation">Mastodon API Documentation</see>
    [Post("/api/v1/emails/confirmations")]
    Task Confirmation([AliasAs("email")] string? email = null);
}
