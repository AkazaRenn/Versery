using System.Globalization;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/ScheduledStatus/">Mastodon API Documentation</see>
public sealed class ScheduledStatus {
    /// <summary>
    /// Version: 2.7.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/ScheduledStatus/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 2.7.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/ScheduledStatus/#scheduled_at"/>
    [JsonPropertyName("scheduled_at")]
    public DateTime ScheduledAt { get; set; } = default;

    /// <summary>
    /// Version: 2.7.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/ScheduledStatus/#params"/>
    [JsonPropertyName("params")]
    public ScheduledStatusParams Params { get; set; } = new();

    /// <summary>
    /// Version: 2.7.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/ScheduledStatus/#media_attachments"/>
    [JsonPropertyName("media_attachments")]
    public List<MediaAttachment> MediaAttachments { get; set; } = [];
}

public sealed class ScheduledStatusParams {
    /// <summary>
    /// Version: 2.7.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/ScheduledStatus/#params-text"/>
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/ScheduledStatus/#params-poll"/>
    [JsonPropertyName("poll")]
    public ScheduledStatusPoll? Poll { get; set; } = null;

    /// <summary>
    /// Version: 2.7.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/ScheduledStatus/#params-media_ids"/>
    [JsonPropertyName("media_ids")]
    public List<string>? MediaIds { get; set; } = null;

    /// <summary>
    /// Version: 2.7.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/ScheduledStatus/#params-sensitive"/>
    [JsonPropertyName("sensitive")]
    public bool? Sensitive { get; set; } = null;

    /// <summary>
    /// Version: 2.7.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/ScheduledStatus/#params-spoiler_text"/>
    [JsonPropertyName("spoiler_text")]
    public string? SpoilerText { get; set; } = null;

    /// <summary>
    /// Version: 2.7.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/ScheduledStatus/#params-visibility"/>
    [JsonPropertyName("visibility")]
    public StatusVisibility? Visibility { get; set; } = null;

    /// <summary>
    /// Version: 2.7.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/ScheduledStatus/#params-in_reply_to_id"/>
    [JsonPropertyName("in_reply_to_id")]
    public string? InReplyToId { get; set; } = null;

    /// <summary>
    /// Version: 2.7.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/ScheduledStatus/#params-language"/>
    [JsonPropertyName("language")]
    public CultureInfo? Language { get; set; } = null;

    /// <summary>
    /// Version: 2.7.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/ScheduledStatus/#params-application_id"/>
    [JsonPropertyName("application_id")]
    public int ApplicationId { get; set; } = 0;

    /// <summary>
    /// Version: 2.7.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/ScheduledStatus/#params-scheduled_at"/>
    [JsonPropertyName("scheduled_at")]
    public object? ScheduledAt { get; set; } = null;

    /// <summary>
    /// Version: 2.7.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/ScheduledStatus/#params-idempotency"/>
    [JsonPropertyName("idempotency")]
    public string? Idempotency { get; set; } = null;

    /// <summary>
    /// Version: 4.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/ScheduledStatus/#params-quoted_status_id"/>
    [JsonPropertyName("quoted_status_id")]
    public string? QuotedStatusId { get; set; } = null;

    /// <summary>
    /// Version: 4.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/ScheduledStatus/#params-quote_approval_policy"/>
    [JsonPropertyName("quote_approval_policy")]
    public QuotePolicy? QuoteApprovalPolicy { get; set; } = null;

    /// <summary>
    /// Version: 2.7.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/ScheduledStatus/#params-with_rate_limit"/>
    [JsonPropertyName("with_rate_limit")]
    public bool WithRateLimit { get; set; } = false;
}

public sealed class ScheduledStatusPoll {
    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/ScheduledStatus/#params-poll-options"/>
    [JsonPropertyName("options")]
    public List<string> Options { get; set; } = [];

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/ScheduledStatus/#params-poll-expires_in"/>
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; } = 0;

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/ScheduledStatus/#params-poll-multiple"/>
    [JsonPropertyName("multiple")]
    public bool? Multiple { get; set; } = null;

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/ScheduledStatus/#params-poll-hide_totals"/>
    [JsonPropertyName("hide_totals")]
    public bool? HideTotals { get; set; } = null;
}
