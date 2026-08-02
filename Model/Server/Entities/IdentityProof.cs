using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/IdentityProof/">Mastodon API Documentation</see>
public sealed class IdentityProof {
    [JsonPropertyName("provider")]
    public string Provider { get; set; } = string.Empty;

    [JsonPropertyName("provider_username")]
    public string ProviderUsername { get; set; } = string.Empty;

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; } = default;

    [JsonPropertyName("proof_url")]
    public Uri? ProofUrl { get; set; } = null;

    [JsonPropertyName("profile_url")]
    public Uri? ProfileUrl { get; set; } = null;
}