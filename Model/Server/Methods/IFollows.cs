using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

public interface IFollows {
    [Post("/api/v1/follows")]
    Task<Account> Post([AliasAs("uri")] string uri);
}
