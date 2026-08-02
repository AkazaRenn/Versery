using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

public interface ICustomEmojis {
    [Get("/api/v1/custom_emojis")]
    Task<List<CustomEmoji>> Get();
}
