using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/custom_emojis/">Mastodon API Documentation</see>
public interface ICustomEmojis {
    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/custom_emojis/#get">Mastodon API Documentation</see>
    [Get("/api/v1/custom_emojis")]
    Task<List<CustomEmoji>> Get();
}
