using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <summary>
/// Represents a proof from an external identity provider.
/// Version: 2.8.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/IdentityProof/">Mastodon API Documentation</see>
public class IdentityProof {
    /// <summary>
    /// The name of the identity provider.
    /// </summary>
    [JsonPropertyName("provider")]
    public string Provider { get; set; } = string.Empty;

    /// <summary>
    /// The account owner’s username on the identity provider’s service.
    /// </summary>
    [JsonPropertyName("provider_username")]
    public string ProviderUsername { get; set; } = string.Empty;

    /// <summary>
    /// When the identity proof was last updated.
    /// </summary>
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; } = default;

    /// <summary>
    /// A link to a statement of identity proof, hosted by the identity provider.
    /// </summary>
    [JsonPropertyName("proof_url")]
    public Uri? ProofUrl { get; set; } = null;

    /// <summary>
    /// The account owner’s profile URL on the identity provider.
    /// </summary>
    [JsonPropertyName("profile_url")]
    public Uri? ProfileUrl { get; set; } = null;
}