using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/filters/">Mastodon API Documentation</see>
public interface IFilters {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/filters/#get">Mastodon API Documentation</see>
    [Get("/api/v2/filters")]
    Task<List<Filter>> Get();

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/filters/#create">Mastodon API Documentation</see>
    [Post("/api/v2/filters")]
    Task<Filter> Create(
        [AliasAs("title")] string title,
        [AliasAs("context[]")][Query(CollectionFormat.Multi)] IEnumerable<string> contexts,
        [AliasAs("filter_action")] string? filterAction = null,
        [AliasAs("expires_in")] uint? expiresIn = null,
        [AliasAs("keywords_attributes[][keyword]")][Query(CollectionFormat.Multi)] IEnumerable<string>? keywords = null,
        [AliasAs("keywords_attributes[][whole_word]")][Query(CollectionFormat.Multi)] IEnumerable<bool>? wholeWords = null);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/filters/#get-one">Mastodon API Documentation</see>
    [Get("/api/v2/filters/{filterId}")]
    Task<Filter> GetOne(string filterId);

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/filters/#update">Mastodon API Documentation</see>
    [Put("/api/v2/filters/{filterId}")]
    Task<Filter> Update(
        string filterId,
        [AliasAs("title")] string? title = null,
        [AliasAs("context[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? contexts = null,
        [AliasAs("filter_action")] string? filterAction = null,
        [AliasAs("expires_in")] uint? expiresIn = null,
        [AliasAs("keywords_attributes[][keyword]")][Query(CollectionFormat.Multi)] IEnumerable<string>? keywords = null,
        [AliasAs("keywords_attributes[][whole_word]")][Query(CollectionFormat.Multi)] IEnumerable<bool>? wholeWords = null,
        [AliasAs("keywords_attributes[][id]")][Query(CollectionFormat.Multi)] IEnumerable<string>? keywordIds = null,
        [AliasAs("keywords_attributes[][_destroy]")][Query(CollectionFormat.Multi)] IEnumerable<bool>? keywordDestroy = null);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/filters/#delete">Mastodon API Documentation</see>
    [Delete("/api/v2/filters/{filterId}")]
    Task Delete(string filterId);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/filters/#keywords-get">Mastodon API Documentation</see>
    [Get("/api/v2/filters/{filterId}/keywords")]
    Task<List<FilterKeyword>> KeywordsGet(string filterId);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/filters/#keywords-create">Mastodon API Documentation</see>
    [Post("/api/v2/filters/{filterId}/keywords")]
    Task<FilterKeyword> KeywordsCreate(
        string filterId,
        [AliasAs("keyword")] string keyword,
        [AliasAs("whole_word")] bool? wholeWord = null);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/filters/#keywords-get-one">Mastodon API Documentation</see>
    [Get("/api/v2/filters/keywords/{keywordId}")]
    Task<FilterKeyword> KeywordsGetOne(string keywordId);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/filters/#keywords-update">Mastodon API Documentation</see>
    [Put("/api/v2/filters/keywords/{keywordId}")]
    Task<FilterKeyword> KeywordsUpdate(
        string keywordId,
        [AliasAs("keyword")] string keyword,
        [AliasAs("whole_word")] bool? wholeWord = null);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/filters/#keywords-delete">Mastodon API Documentation</see>
    [Delete("/api/v2/filters/keywords/{keywordId}")]
    Task KeywordsDelete(string keywordId);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/filters/#statuses-get">Mastodon API Documentation</see>
    [Get("/api/v2/filters/{filterId}/statuses")]
    Task<List<FilterStatus>> StatusesGet(string filterId);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/filters/#statuses-add">Mastodon API Documentation</see>
    [Post("/api/v2/filters/{filterId}/statuses")]
    Task<FilterStatus> StatusesAdd(
        string filterId,
        [AliasAs("status_id")] string statusId);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/filters/#statuses-get-one">Mastodon API Documentation</see>
    [Get("/api/v2/filters/statuses/{statusFilterId}")]
    Task<FilterStatus> StatusesGetOne(string statusFilterId);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/filters/#statuses-remove">Mastodon API Documentation</see>
    [Delete("/api/v2/filters/statuses/{statusFilterId}")]
    Task<FilterStatus> StatusesRemove(string statusFilterId);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/filters/#get-v1">Mastodon API Documentation</see>
    [Get("/api/v1/filters")]
    Task<List<V1Filter>> GetV1();

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/filters/#create-v1">Mastodon API Documentation</see>
    [Post("/api/v1/filters")]
    Task<V1Filter> CreateV1(
        [AliasAs("phrase")] string phrase,
        [AliasAs("context[]")][Query(CollectionFormat.Multi)] IEnumerable<string> contexts,
        [AliasAs("irreversible")] bool? irreversible = null,
        [AliasAs("whole_word")] bool? wholeWord = null,
        [AliasAs("expires_in")] uint? expiresIn = null);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/filters/#get-one-v1">Mastodon API Documentation</see>
    [Get("/api/v1/filters/{filterId}")]
    Task<V1Filter> GetOneV1(string filterId);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/filters/#update-v1">Mastodon API Documentation</see>
    [Put("/api/v1/filters/{filterId}")]
    Task<V1Filter> UpdateV1(
        string filterId,
        [AliasAs("phrase")] string phrase,
        [AliasAs("context[]")][Query(CollectionFormat.Multi)] IEnumerable<string> contexts,
        [AliasAs("irreversible")] bool? irreversible = null,
        [AliasAs("whole_word")] bool? wholeWord = null,
        [AliasAs("expires_in")] uint? expiresIn = null);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/filters/#delete-v1">Mastodon API Documentation</see>
    [Delete("/api/v1/filters/{filterId}")]
    Task DeleteV1(string filterId);
}
