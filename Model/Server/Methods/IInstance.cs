using Model.Server.Entities;
using Model.Server.Methods.Entities;
using Refit;

namespace Model.Server.Methods;

public interface IInstance {
    [Get("/api/v2/instance")]
    Task<Instance> Get();

    [Get("/api/v1/instance/peers")]
    Task<List<string>> GetPeers();

    [Get("/api/v1/instance/activity")]
    Task<List<WeeklyActivity>> GetActivity();
}
