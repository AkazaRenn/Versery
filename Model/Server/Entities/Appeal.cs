using System.Text.Json.Serialization;
using Model.Server.Entities.Enumerations;

namespace Model.Server.Entities;
/// <summary>
/// Represents an appeal against a moderation action.
/// Version: 4.3.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Appeal/">Mastodon API Documentation</see>
public sealed class Appeal {
    /// <summary>
    /// Text of the appeal from the moderated account to the moderators.
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// State of the appeal.
    /// </summary>
    [JsonPropertyName("state")]
    public AppealState State { get; set; } = default;
}
