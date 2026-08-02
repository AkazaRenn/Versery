using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/Appeal/">Mastodon API Documentation</see>
public sealed class Appeal {
    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Appeal/#text"/>
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Appeal/#state"/>
    [JsonPropertyName("state")]
    public AppealState State { get; set; } = AppealState.Pending;
}

public enum AppealState {
    [JsonStringEnumMemberName("approved")]
    Approved,

    [JsonStringEnumMemberName("rejected")]
    Rejected,

    [JsonStringEnumMemberName("pending")]
    Pending,
}
