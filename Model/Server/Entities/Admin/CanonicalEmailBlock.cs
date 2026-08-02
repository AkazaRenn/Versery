using System.Text.Json.Serialization;

namespace Model.Server.Entities.Admin;

/// <see href="https://docs.joinmastodon.org/entities/Admin_CanonicalEmailBlock/">Mastodon API Documentation</see>
public sealed class CanonicalEmailBlock {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_CanonicalEmailBlock/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_CanonicalEmailBlock/#canonical_email_hash"/>
    [JsonPropertyName("canonical_email_hash")]
    public string CanonicalEmailHash { get; set; } = string.Empty;
}
