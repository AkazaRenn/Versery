using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/StatusEdit/">Mastodon API Documentation</see>
public sealed class StatusEdit {
    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/StatusEdit/#content"/>
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/StatusEdit/#spoiler_text"/>
    [JsonPropertyName("spoiler_text")]
    public string SpoilerText { get; set; } = string.Empty;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/StatusEdit/#sensitive"/>
    [JsonPropertyName("sensitive")]
    public bool Sensitive { get; set; } = false;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/StatusEdit/#created_at"/>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = default;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/StatusEdit/#account"/>
    [JsonPropertyName("account")]
    public Account Account { get; set; } = new();

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/StatusEdit/#poll"/>
    [JsonPropertyName("poll")]
    public StatusEditPoll? Poll { get; set; } = null;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/StatusEdit/#media_attachments"/>
    [JsonPropertyName("media_attachments")]
    public List<MediaAttachment> MediaAttachments { get; set; } = [];

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/StatusEdit/#emojis"/>
    [JsonPropertyName("emojis")]
    public List<CustomEmoji> Emojis { get; set; } = [];

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/StatusEdit/#quote"/>
    [JsonPropertyName("quote")]
    public Quote? Quote { get; set; } = null;
}

public sealed class StatusEditPoll {
    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/StatusEdit/#poll-options"/>
    [JsonPropertyName("options")]
    public List<StatusEditPollOption> Options { get; set; } = [];
}

public sealed class StatusEditPollOption {
    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/StatusEdit/#poll-options-title"/>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;
}
