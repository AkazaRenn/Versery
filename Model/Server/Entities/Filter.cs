using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/Filter/">Mastodon API Documentation</see>
public sealed class Filter {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Filter/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Filter/#title"/>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Filter/#context"/>
    [JsonPropertyName("context")]
    public List<FilterContext> Context { get; set; } = [];

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Filter/#expires_at"/>
    [JsonPropertyName("expires_at")]
    public DateTime? ExpiresAt { get; set; } = null;

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Filter/#filter_action"/>
    [JsonPropertyName("filter_action")]
    public FilterAction FilterAction { get; set; } = FilterAction.Warn;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Filter/#keywords"/>
    [JsonPropertyName("keywords")]
    public List<FilterKeyword>? Keywords { get; set; } = null;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Filter/#statuses"/>
    [JsonPropertyName("statuses")]
    public List<FilterStatus>? Statuses { get; set; } = null;
}



public enum FilterAction {
    [JsonStringEnumMemberName("warn")]
    Warn,

    [JsonStringEnumMemberName("hide")]
    Hide,

    [JsonStringEnumMemberName("blur")]
    Blur,
}

public enum FilterContext {
    [JsonStringEnumMemberName("home")]
    Home,

    [JsonStringEnumMemberName("notifications")]
    Notifications,

    [JsonStringEnumMemberName("public")]
    Public,

    [JsonStringEnumMemberName("thread")]
    Thread,

    [JsonStringEnumMemberName("account")]
    Account,
}
