using Model.Server.Entities.Enumerations;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/Appeal/">Mastodon API Documentation</see>
public sealed class Appeal {
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("state")]
    public AppealState State { get; set; } = default;
}
