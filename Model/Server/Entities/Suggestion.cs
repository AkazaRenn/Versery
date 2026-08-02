using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/Suggestion/">Mastodon API Documentation</see>
public sealed class Suggestion {
    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Suggestion/#source"/>
    [Obsolete("deprecated, use sources instead")]
    [JsonPropertyName("source")]
    public SuggestionReason Source { get; set; } = SuggestionReason.Staff;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Suggestion/#sources"/>
    [JsonPropertyName("sources")]
    public List<SuggestionSource> Sources { get; set; } = [];

    /// <summary>
    /// Version: 3.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Suggestion/#account"/>
    [JsonPropertyName("account")]
    public Account Account { get; set; } = new();
}

public enum SuggestionReason {
    [JsonStringEnumMemberName("staff")]
    Staff,

    [JsonStringEnumMemberName("past_interactions")]
    PastInteractions,

    [JsonStringEnumMemberName("global")]
    Global,
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