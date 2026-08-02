using Refit;
using System.Text.Json.Serialization;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/health/">Mastodon API Documentation</see>
public interface IHealth {
    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/health/#get">Mastodon API Documentation</see>
    [Get("/health")]
    Task<HealthStatus> Get();
}

public sealed class HealthStatus {
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
}
