using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/Tag/">Mastodon API Documentation</see>
public class Tag: ShallowTag {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("history")]
    public IEnumerable<TagHistory>? History { get; set; } = null;

    public sealed class TagHistory {
        [JsonPropertyName("day")]
        public string Day { get; set; } = string.Empty;

        [JsonPropertyName("uses")]
        public string Uses { get; set; } = string.Empty;

        [JsonPropertyName("accounts")]
        public string Accounts { get; set; } = string.Empty;
    }

    [JsonPropertyName("following")]
    public bool? Following { get; set; } = null;

    [JsonPropertyName("featuring")]
    public bool? Featuring { get; set; } = null;
}

/// <see href="https://docs.joinmastodon.org/entities/Tag/#admin">Mastodon API Documentation</see>
public sealed class AdminTag: Tag {
    [JsonPropertyName("trendable")]
    public bool Trendable { get; set; } = false;

    [JsonPropertyName("usable")]
    public bool Usable { get; set; } = false;

    [JsonPropertyName("requires_review")]
    public bool RequiresReview { get; set; } = false;
}