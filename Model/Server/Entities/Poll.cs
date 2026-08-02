using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/Poll/">Mastodon API Documentation</see>
public sealed class Poll {
    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Poll/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Poll/#expires_at"/>
    [JsonPropertyName("expires_at")]
    public DateTime? ExpiresAt { get; set; } = null;

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Poll/#expired"/>
    [JsonPropertyName("expired")]
    public bool Expired { get; set; } = false;

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Poll/#multiple"/>
    [JsonPropertyName("multiple")]
    public bool Multiple { get; set; } = false;

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Poll/#votes_count"/>
    [JsonPropertyName("votes_count")]
    public long VotesCount { get; set; } = 0;

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Poll/#voters_count"/>
    [JsonPropertyName("voters_count")]
    public int? VotersCount { get; set; } = null;

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Poll/#options"/>
    [JsonPropertyName("options")]
    public List<PollOption> Options { get; set; } = [];

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Poll/#emojis"/>
    [JsonPropertyName("emojis")]
    public List<CustomEmoji> Emojis { get; set; } = [];

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Poll/#voted"/>
    [JsonPropertyName("voted")]
    public bool? Voted { get; set; } = null;

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Poll/#own_votes"/>
    [JsonPropertyName("own_votes")]
    public List<int> OwnVotes { get; set; } = [];
}

public sealed class PollOption {
    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Poll/#option-title"/>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Poll/#option-votes_count"/>
    [JsonPropertyName("votes_count")]
    public int? VotesCount { get; set; } = null;
}

public sealed class PollParameters {
    public List<string> Options { get; set; } = [];

    public TimeSpan ExpiresIn { get; set; } = default;

    public bool? Multiple { get; set; } = null;

    public bool? HideTotals { get; set; } = null;
}
