using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

public interface IEndorsements {
    [Get("/api/v1/endorsements")]
    Task<List<Account>> Get();
}
