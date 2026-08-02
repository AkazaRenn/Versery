using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/TermsOfService/">Mastodon API Documentation</see>
public sealed class TermsOfService {
    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/TermsOfService/#effective_date"/>
    [JsonPropertyName("effective_date")]
    public DateOnly EffectiveDate { get; set; } = DateOnly.MinValue;

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/TermsOfService/#effective"/>
    [JsonPropertyName("effective")]
    public bool Effective { get; set; } = false;

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/TermsOfService/#content"/>
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/TermsOfService/#succeeded_by"/>
    [JsonPropertyName("succeeded_by")]
    public DateOnly? SucceededBy { get; set; } = null;
}
