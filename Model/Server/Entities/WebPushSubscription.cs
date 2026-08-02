using System.Text.Json.Serialization;

namespace Model.Server.Entities;

public sealed class WebPushSubscription {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("endpoint")]
    public Uri? Endpoint { get; set; } = null;

    [JsonPropertyName("standard")]
    public bool Standard { get; set; } = false;

    [JsonPropertyName("server_key")]
    public string ServerKey { get; set; } = string.Empty;

    [JsonPropertyName("alerts")]
    public WebPushSubscriptionAlerts Alerts { get; set; } = new();
}

public sealed class WebPushSubscriptionAlerts {
    [JsonPropertyName("mention")]
    public bool Mention { get; set; } = false;

    [JsonPropertyName("status")]
    public bool Status { get; set; } = false;

    [JsonPropertyName("reblog")]
    public bool Reblog { get; set; } = false;

    [JsonPropertyName("follow")]
    public bool Follow { get; set; } = false;

    [JsonPropertyName("follow_request")]
    public bool FollowRequest { get; set; } = false;

    [JsonPropertyName("favourite")]
    public bool Favourite { get; set; } = false;

    [JsonPropertyName("poll")]
    public bool Poll { get; set; } = false;

    [JsonPropertyName("update")]
    public bool Update { get; set; } = false;

    [JsonPropertyName("admin.sign_up")]
    public bool AdminSignUp { get; set; } = false;

    [JsonPropertyName("admin.report")]
    public bool AdminReport { get; set; } = false;
}
