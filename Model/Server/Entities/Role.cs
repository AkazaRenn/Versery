using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <summary>
/// Represents a custom user role that grants permissions.
/// Version: 4.6.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Role/">Mastodon API Documentation</see>
public class Role: AccountRole {
    /// <summary>
    /// A bitmask that represents the sum of all permissions granted to the role. This is a potentially large integer in decimal representation. The absence of special permissions is denoted by '0'.
    /// </summary>
    [JsonPropertyName("permissions")]
    public string Permissions { get; set; } = String.Empty;

    /// <summary>
    /// Whether the role is publicly visible as a badge on user profiles.
    /// </summary>
    [JsonPropertyName("highlighted")]
    public bool Highlighted { get; set; } = false;

    /// <summary>
    /// Maximum number of Collections that users with this role are allowed to create.
    /// </summary>
    [JsonPropertyName("collection_limit")]
    public int CollectionLimit { get; set; } = 0;
}