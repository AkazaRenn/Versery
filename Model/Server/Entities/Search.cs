using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/Search/">Mastodon API Documentation</see>
public sealed class Search {
    /// <summary>
    /// Version: 1.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Search/#accounts"/>
    [JsonPropertyName("accounts")]
    public List<Account> Accounts { get; set; } = [];

    /// <summary>
    /// Version: 1.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Search/#statuses"/>
    [JsonPropertyName("statuses")]
    public List<Status> Statuses { get; set; } = [];

    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Search/#hashtags"/>
    [JsonPropertyName("hashtags")]
    public List<Tag> Hashtags { get; set; } = [];

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Search/#collections"/>
    [JsonPropertyName("collections")]
    public List<Collection> Collections { get; set; } = [];
}
