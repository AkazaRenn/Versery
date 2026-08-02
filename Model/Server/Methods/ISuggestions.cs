using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

public interface ISuggestions {
    [Get("/api/v1/suggestions")]
    Task<List<Account>> Get();

    [Delete("/api/v1/suggestions/{accountId}")]
    Task Delete(string accountId);
}
