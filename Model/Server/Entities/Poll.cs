using System.Text.Json.Serialization;

namespace Model.Server.Entities;

public sealed class Poll {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("expires_at")]
    public DateTime? ExpiresAt { get; set; }

    [JsonPropertyName("expired")]
    public bool Expired { get; set; }

    [JsonPropertyName("multiple")]
    public bool Multiple { get; set; }

    [JsonPropertyName("votes_count")]
    public long VotesCount { get; set; }

    [JsonPropertyName("voters_count")]
    public int? VotersCount { get; set; }

    [JsonPropertyName("voted")]
    public bool? Voted { get; set; }

    [JsonPropertyName("own_votes")]
    public IEnumerable<int> OwnVotes { get; set; } = [];

    [JsonPropertyName("options")]
    public IEnumerable<PollOption> Options { get; set; } = [];

    [JsonPropertyName("emojis")]
    public IEnumerable<CustomEmoji> Emojis { get; set; } = [];
}

public sealed class PollOption {
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("votes_count")]
    public int? VotesCount { get; set; }
}

public sealed class PollParameters {
    public IEnumerable<string> Options { get; set; } = [];

    public TimeSpan ExpiresIn { get; set; }

    public bool? Multiple { get; set; }

    public bool? HideTotals { get; set; }
}
