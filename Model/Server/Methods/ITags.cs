using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

public interface ITags {
    [Get("/api/v1/tags/{tag}")]
    Task<Tag> Get(string tag);

    [Post("/api/v1/tags/{tag}/follow")]
    Task<Tag> Follow(string tag);

    [Post("/api/v1/tags/{tag}/unfollow")]
    Task<Tag> Unfollow(string tag);
}
