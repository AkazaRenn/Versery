using Refit;
using System.Text.Json.Serialization;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/proofs/">Mastodon API Documentation</see>
public interface IProofs {
    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/proofs/#get">Mastodon API Documentation</see>
    [Get("/api/proofs")]
    Task<ProofResponse> Get(
        [AliasAs("provider")] string provider,
        [AliasAs("username")] string username);
}

public sealed class ProofResponse {
    [JsonPropertyName("avatar")]
    public Uri? Avatar { get; set; } = null;

    [JsonPropertyName("signatures")]
    public List<ProofSignature> Signatures { get; set; } = [];
}

public sealed class ProofSignature {
    [JsonPropertyName("sig_hash")]
    public string SigHash { get; set; } = string.Empty;

    [JsonPropertyName("kb_username")]
    public string KbUsername { get; set; } = string.Empty;
}
