using System.Text.Json.Serialization;

namespace Model.Server.Entities;

public sealed class AsyncRefreshResponse {
    [JsonPropertyName("async_refresh")]
    public AsyncRefresh AsyncRefresh { get; set; } = new();
}

public sealed class AsyncRefresh {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public AsyncRefreshStatus Status { get; set; } = AsyncRefreshStatus.Running;

    [JsonPropertyName("result_count")]
    public int? ResultCount { get; set; } = null;
}

public enum AsyncRefreshStatus {
    [JsonStringEnumMemberName("running")]
    Running,

    [JsonStringEnumMemberName("finished")]
    Finished
}
