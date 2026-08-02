using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/AsyncRefresh/">Mastodon API Documentation</see>
public sealed class AsyncRefreshResponse {
    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/AsyncRefresh/"/>
    [JsonPropertyName("async_refresh")]
    public AsyncRefresh AsyncRefresh { get; set; } = new();
}

/// <see href="https://docs.joinmastodon.org/entities/AsyncRefresh/">Mastodon API Documentation</see>
public sealed class AsyncRefresh {
    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/AsyncRefresh/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/AsyncRefresh/#status"/>
    [JsonPropertyName("status")]
    public AsyncRefreshStatus Status { get; set; } = AsyncRefreshStatus.Finished;

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/AsyncRefresh/#result_count"/>
    [JsonPropertyName("result_count")]
    public int? ResultCount { get; set; } = null;
}

public enum AsyncRefreshStatus {
    [JsonStringEnumMemberName("running")]
    Running,

    [JsonStringEnumMemberName("finished")]
    Finished
}
