using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/preferences/">Mastodon API Documentation</see>
public interface IPreferences {
    /// <summary>
    /// Version: 4.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/preferences/#get"/>
    [Get("/api/v1/preferences")]
    Task<Preferences> Get();
}