using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

public interface IFilters {
    [Get("/api/v1/filters")]
    Task<List<Filter>> Get();

    [Post("/api/v1/filters")]
    Task<Filter> Post(
        [AliasAs("phrase")] string? phrase = null,
        [AliasAs("context[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? contexts = null,
        [AliasAs("irreversible")] bool? irreversible = null,
        [AliasAs("whole_word")] bool? wholeWord = null,
        [AliasAs("expires_in")] uint? expiresIn = null);

    [Get("/api/v1/filters/{filterId}")]
    Task<Filter> Get(string filterId);

    [Put("/api/v1/filters/{filterId}")]
    Task<Filter> Put(
        string filterId,
        [AliasAs("phrase")] string? phrase = null,
        [AliasAs("context[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? contexts = null,
        [AliasAs("irreversible")] bool? irreversible = null,
        [AliasAs("whole_word")] bool? wholeWord = null,
        [AliasAs("expires_in")] uint? expiresIn = null);

    [Delete("/api/v1/filters/{filterId}")]
    Task Delete(string filterId);
}
