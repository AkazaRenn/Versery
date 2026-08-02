using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/Role/">Mastodon API Documentation</see>
public sealed class Role: AccountRole {
    [JsonPropertyName("permissions")]
    public string Permissions { get; set; } = String.Empty;

    [JsonPropertyName("highlighted")]
    public bool Highlighted { get; set; } = false;

    [JsonPropertyName("collection_limit")]
    public int CollectionLimit { get; set; } = 0;
}