using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/QuoteApproval/">Mastodon API Documentation</see>
public sealed class QuoteApproval {
    /// <summary>
    /// Version: 4.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/QuoteApproval/#automatic"/>
    [JsonPropertyName("automatic")]
    public List<FeatureApprovalPolicy> Automatic { get; set; } = [];

    /// <summary>
    /// Version: 4.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/QuoteApproval/#manual"/>
    [JsonPropertyName("manual")]
    public List<FeatureApprovalPolicy> Manual { get; set; } = [];

    /// <summary>
    /// Version: 4.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/QuoteApproval/#current_user"/>
    [JsonPropertyName("current_user")]
    public UserFeatureOption CurrentUser { get; set; } = UserFeatureOption.Unknown;
}