using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <summary>
/// Represents a hashtag used within the content of a status.
/// Version: 4.4.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Tag/">Mastodon API Documentation</see>
public class Tag: ShallowTag {
    /// <summary>
    /// ID of the hashtag in the database. Useful for constructing URLs for the moderation tools & Admin API.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Usage statistics for given days (typically the past week).
    /// </summary>
    [JsonPropertyName("history")]
    public IEnumerable<TagHistory>? History { get; set; } = null;

    public class TagHistory {
        /// <summary>
        /// UNIX timestamp on midnight of the given day.
        /// </summary>
        [JsonPropertyName("day")]
        public string Day { get; set; } = string.Empty;

        /// <summary>
        /// The counted usage of the tag within that day.
        /// </summary>
        [JsonPropertyName("uses")]
        public string Uses { get; set; } = string.Empty;

        /// <summary>
        /// The total of accounts using the tag within that day.
        /// </summary>
        [JsonPropertyName("accounts")]
        public string Accounts { get; set; } = string.Empty;
    }

    /// <summary>
    /// Whether the current token’s authorized user is following this tag.
    /// </summary>
    [JsonPropertyName("following")]
    public bool? Following { get; set; } = null;

    /// <summary>
    /// Whether the current token’s authorized user is featuring this tag on their profile.
    /// </summary>
    [JsonPropertyName("featuring")]
    public bool? Featuring { get; set; } = null;
}

/// <summary>
/// Version: 3.5.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Tag/#admin">Mastodon API Documentation</see>
public class AdminTag: Tag {
    /// <summary>
    /// Whether the hashtag has been approved to trend.
    /// </summary>
    [JsonPropertyName("trendable")]
    public bool Trendable { get; set; } = false;

    /// <summary>
    /// Whether the hashtag has not been disabled from auto-linking.
    /// </summary>
    [JsonPropertyName("usable")]
    public bool Usable { get; set; } = false;

    /// <summary>
    /// Whether the hashtag has not been reviewed yet to approve or deny its trending.
    /// </summary>
    [JsonPropertyName("requires_review")]
    public bool RequiresReview { get; set; } = false;
}