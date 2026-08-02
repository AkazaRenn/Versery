using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/WebPushSubscription/">Mastodon API Documentation</see>
public sealed class WebPushSubscription {
    /// <summary>
    /// Version: 2.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/WebPushSubscription/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 2.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/WebPushSubscription/#endpoint"/>
    [JsonPropertyName("endpoint")]
    public Uri? Endpoint { get; set; } = null;

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/WebPushSubscription/#standard"/>
    [JsonPropertyName("standard")]
    public bool Standard { get; set; } = false;

    /// <summary>
    /// Version: 2.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/WebPushSubscription/#server_key"/>
    [JsonPropertyName("server_key")]
    public string ServerKey { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/WebPushSubscription/#alerts"/>
    [JsonPropertyName("alerts")]
    public WebPushSubscriptionAlerts Alerts { get; set; } = new();
}

public sealed class WebPushSubscriptionAlerts {
    /// <summary>
    /// Version: 2.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/WebPushSubscription/#mention"/>
    [JsonPropertyName("mention")]
    public bool Mention { get; set; } = false;

    /// <summary>
    /// Version: 3.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/WebPushSubscription/#status"/>
    [JsonPropertyName("status")]
    public bool Status { get; set; } = false;

    /// <summary>
    /// Version: 2.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/WebPushSubscription/#reblog"/>
    [JsonPropertyName("reblog")]
    public bool Reblog { get; set; } = false;

    /// <summary>
    /// Version: 2.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/WebPushSubscription/#follow"/>
    [JsonPropertyName("follow")]
    public bool Follow { get; set; } = false;

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/WebPushSubscription/#follow_request"/>
    [JsonPropertyName("follow_request")]
    public bool FollowRequest { get; set; } = false;

    /// <summary>
    /// Version: 2.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/WebPushSubscription/#favourite"/>
    [JsonPropertyName("favourite")]
    public bool Favourite { get; set; } = false;

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/WebPushSubscription/#poll"/>
    [JsonPropertyName("poll")]
    public bool Poll { get; set; } = false;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/WebPushSubscription/#update"/>
    [JsonPropertyName("update")]
    public bool Update { get; set; } = false;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/WebPushSubscription/#admin-sign_up"/>
    [JsonPropertyName("admin.sign_up")]
    public bool AdminSignUp { get; set; } = false;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/WebPushSubscription/#admin-report"/>
    [JsonPropertyName("admin.report")]
    public bool AdminReport { get; set; } = false;
}
