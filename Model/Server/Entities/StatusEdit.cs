using System.Text.Json.Serialization;

namespace Model.Server.Entities;

public sealed class StatusEdit {
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    [JsonPropertyName("spoiler_text")]
    public string SpoilerText { get; set; } = string.Empty;

    [JsonPropertyName("sensitive")]
    public bool Sensitive { get; set; } = false;

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;

    [JsonPropertyName("account")]
    public Account Account { get; set; } = new();

    [JsonPropertyName("poll")]
    public StatusEditPoll? Poll { get; set; } = null;

    [JsonPropertyName("media_attachments")]
    public List<MediaAttachment> MediaAttachments { get; set; } = [];

    [JsonPropertyName("emojis")]
    public List<CustomEmoji> Emojis { get; set; } = [];

    [JsonPropertyName("quote")]
    public Quote? Quote { get; set; } = null;
}

public sealed class StatusEditPoll {
    [JsonPropertyName("options")]
    public List<StatusEditPollOption> Options { get; set; } = [];
}

public sealed class StatusEditPollOption {
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;
}
