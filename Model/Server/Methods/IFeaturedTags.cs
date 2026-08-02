using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

public interface IFeaturedTags {
    [Get("/api/v1/featured_tags")]
    Task<List<FeaturedTag>> Get();

    [Post("/api/v1/featured_tags")]
    Task<FeaturedTag> Post([AliasAs("name")] string name);

    [Delete("/api/v1/featured_tags/{id}")]
    Task Delete(string id);

    [Get("/api/v1/featured_tags/suggestions")]
    Task<List<Tag>> GetSuggestions();
}
