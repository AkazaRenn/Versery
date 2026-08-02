using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <summary>
/// <see href="https://docs.joinmastodon.org/entities/Suggestion/">Mastodon API Documentation</see>
public sealed class Suggestion {
    /// <summary>
    /// Version: 3.4.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/Suggestion/#source">Mastodon API Documentation</see>
    [JsonPropertyName("source")]
    public string Source { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/Suggestion/#sources">Mastodon API Documentation</see>
    [JsonPropertyName("sources")]
    public List<SuggestionSource> Sources { get; set; } = [];

    /// <summary>
    /// Version: 3.4.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/Suggestion/#account">Mastodon API Documentation</see>
    [JsonPropertyName("account")]
    public Account Account { get; set; } = new();
}

public enum SuggestionSource {
    [JsonStringEnumMemberName("featured")]
    Featured,

    [JsonStringEnumMemberName("most_followed")]
    MostFollowed,

    [JsonStringEnumMemberName("most_interactions")]
    MostInteractions,

    [JsonStringEnumMemberName("similar_to_recently_followed")]
    SimilarToRecentlyFollowed,

    [JsonStringEnumMemberName("friends_of_friends")]
    FriendsOfFriends,
}