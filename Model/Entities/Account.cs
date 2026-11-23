using Model.Interfaces;
using System.Text.Json.Serialization;

namespace Model.Entities;

public record Account: Server.Entities.Account, IDatabaseEntity {
    [JsonIgnore]
    public string IndexId => Id;
    public DateTimeOffset ExpiresAt { get; init; } = DateTimeOffset.UtcNow.AddMonths(3);

    public Account(Server.Entities.Account serverAccount) : base(serverAccount) {
    }

    private static readonly Server.Entities.Account empty = (Server.Entities.Account)System.Runtime.CompilerServices.RuntimeHelpers.GetUninitializedObject(typeof(Server.Entities.Account));
    [JsonConstructor]
    public Account() : base(empty) {
    }
}
