using Model.Server.Entities;
using Model.Server.Methods.Enumerations;
using Refit;

namespace Model.Server.Methods;

public interface IDirectory {
    [Get("/api/v1/directory")]
    Task<List<Account>> Get(
        [AliasAs("offset")] int? offset = null,
        [AliasAs("limit")] int? limit = null,
        [AliasAs("order")] DirectoryOrder? order = null,
        [AliasAs("local")] bool? local = null);
}
