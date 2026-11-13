using System.Text.Json.Serialization;

namespace Model.Entities;

public record Status: Server.Entities.Status {
    [JsonPropertyName("account")]
    public new string Account { get; set; } = string.Empty;
    [JsonPropertyName("quote")]
    public new string? Quote { get; set; } = null;
    [JsonPropertyName("reblog")]
    public new string? Reblog { get; set; } = null;

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
