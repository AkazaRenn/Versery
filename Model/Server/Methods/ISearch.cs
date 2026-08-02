using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

public interface ISearch {
    [Get("/api/v2/search")]
    Task<Search> Search(
        [AliasAs("q")] string q,
        [AliasAs("resolve")] bool? resolve = null);
}
