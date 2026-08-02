using Model.Server.Entities;
using Raiqub.Generators.EnumUtilities;
using Refit;
using System.Text.Json.Serialization;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/push/">Mastodon API Documentation</see>
public interface IPush {
    /// <summary>
    /// Version: 4.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/push/#create"/>
    [Post("/api/v1/push/subscription")]
    Task<WebPushSubscription> Create(
        [AliasAs("subscription[endpoint]")] Uri endpoint,
        [AliasAs("subscription[keys][p256dh]")] string p256dh,
        [AliasAs("subscription[keys][auth]")] string auth,
        [AliasAs("subscription[standard]")] bool? standard = null,
        [AliasAs("data[alerts][mention]")] bool? mention = null,
        [AliasAs("data[alerts][quote]")] bool? quote = null,
        [AliasAs("data[alerts][status]")] bool? status = null,
        [AliasAs("data[alerts][reblog]")] bool? reblog = null,
        [AliasAs("data[alerts][follow]")] bool? follow = null,
        [AliasAs("data[alerts][follow_request]")] bool? followRequest = null,
        [AliasAs("data[alerts][favourite]")] bool? favourite = null,
        [AliasAs("data[alerts][poll]")] bool? poll = null,
        [AliasAs("data[alerts][update]")] bool? update = null,
        [AliasAs("data[alerts][quoted_update]")] bool? quotedUpdate = null,
        [AliasAs("data[alerts][admin.sign_up]")] bool? adminSignUp = null,
        [AliasAs("data[alerts][admin.report]")] bool? adminReport = null,
        [AliasAs("data[policy]")] PushPolicy? policy = null);

    /// <summary>
    /// Version: 2.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/push/#get"/>
    [Get("/api/v1/push/subscription")]
    Task<WebPushSubscription> Get();

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/push/#update"/>
    [Put("/api/v1/push/subscription")]
    Task<WebPushSubscription> Update(
        [AliasAs("data[alerts][mention]")] bool? mention = null,
        [AliasAs("data[alerts][status]")] bool? status = null,
        [AliasAs("data[alerts][reblog]")] bool? reblog = null,
        [AliasAs("data[alerts][follow]")] bool? follow = null,
        [AliasAs("data[alerts][follow_request]")] bool? followRequest = null,
        [AliasAs("data[alerts][favourite]")] bool? favourite = null,
        [AliasAs("data[alerts][poll]")] bool? poll = null,
        [AliasAs("data[alerts][update]")] bool? update = null,
        [AliasAs("data[alerts][admin.sign_up]")] bool? adminSignUp = null,
        [AliasAs("data[alerts][admin.report]")] bool? adminReport = null,
        [AliasAs("policy")] PushPolicy? policy = null);

    /// <summary>
    /// Version: 2.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/push/#delete"/>
    [Delete("/api/v1/push/subscription")]
    Task Delete();
}

[JsonConverterGenerator]
public enum PushPolicy {
    [JsonStringEnumMemberName("all")]
    All,
    [JsonStringEnumMemberName("followed")]
    Followed,
    [JsonStringEnumMemberName("follower")]
    Follower,
    [JsonStringEnumMemberName("none")]
    None
}
