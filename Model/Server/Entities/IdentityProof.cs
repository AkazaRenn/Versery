using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/IdentityProof/">Mastodon API Documentation</see>
public sealed class IdentityProof {
    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/IdentityProof/#provider"/>
    [JsonPropertyName("provider")]
    public string Provider { get; set; } = string.Empty;

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/IdentityProof/#provider_username"/>
    [JsonPropertyName("provider_username")]
    public string ProviderUsername { get; set; } = string.Empty;

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/IdentityProof/#updated_at"/>
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; } = default;

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/IdentityProof/#proof_url"/>
    [JsonPropertyName("proof_url")]
    public Uri? ProofUrl { get; set; } = null;

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/IdentityProof/#profile_url"/>
    [JsonPropertyName("profile_url")]
    public Uri? ProfileUrl { get; set; } = null;
}