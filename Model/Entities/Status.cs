using Model.Interfaces;
using System.Text.Json.Serialization;

namespace Model.Entities;

public record Status: Server.Entities.Status, IDatabaseEntity {
    [JsonIgnore]
    public string IndexId => Id;
    [JsonPropertyName("account")]
    public new string Account { get; init; } = string.Empty;
    [JsonPropertyName("quote")]
    public new string? Quote { get; init; } = null;
    [JsonPropertyName("reblog")]
    public new string? Reblog { get; init; } = null;
    public DateTimeOffset ExpiresAt { get; init; } = DateTimeOffset.UtcNow.AddMonths(3);

    public Status(Server.Entities.Status serverStatus) : base(serverStatus) {
        Account = serverStatus.Account.Id;
        Quote = serverStatus.Quote?.QuotedStatus?.Id;
        Reblog = serverStatus.Reblog?.Id;
    }

    private static readonly Server.Entities.Status empty = (Server.Entities.Status)System.Runtime.CompilerServices.RuntimeHelpers.GetUninitializedObject(typeof(Server.Entities.Status));
    [JsonConstructor]
    public Status() : base(empty) {
    }
}
