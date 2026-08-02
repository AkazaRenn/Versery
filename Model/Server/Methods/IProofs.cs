using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

public interface IProofs {
    [Get("/api/proofs")]
    Task<IdentityProof> Get();
}
