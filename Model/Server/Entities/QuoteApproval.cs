using Model.Server.Entities.Enumerations;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/QuoteApproval/">Mastodon API Documentation</see>
public sealed class QuoteApproval {
    [JsonPropertyName("automatic")]
    public IEnumerable<FeatureApprovalPolicy> Automatic { get; set; } = [];

    [JsonPropertyName("manual")]
    public IEnumerable<FeatureApprovalPolicy> Manual { get; set; } = [];

    [JsonPropertyName("current_user")]
    public UserFeatureOption CurrentUser { get; set; } = default;
}