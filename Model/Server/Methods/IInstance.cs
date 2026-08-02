using Model.Server.Entities;
using Refit;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/instance/">Mastodon API Documentation</see>
public interface IInstance {
    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/instance/#v2">Mastodon API Documentation</see>
    [Get("/api/v2/instance")]
    Task<Instance> V2();

    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/instance/#peers">Mastodon API Documentation</see>
    [Get("/api/v1/instance/peers")]
    Task<List<string>> Peers();

    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/instance/#activity">Mastodon API Documentation</see>
    [Get("/api/v1/instance/activity")]
    Task<List<WeeklyActivity>> Activity();

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/instance/#v1">Mastodon API Documentation</see>
    [Get("/api/v1/instance")]
    Task<V1Instance> V1();

    /// <summary>
    /// Version: 3.4.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/instance/#rules">Mastodon API Documentation</see>
    [Get("/api/v1/instance/rules")]
    Task<List<Rule>> Rules();

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/instance/#domain_blocks">Mastodon API Documentation</see>
    [Get("/api/v1/instance/domain_blocks")]
    Task<List<DomainBlock>> DomainBlocks();

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/instance/#extended_description">Mastodon API Documentation</see>
    [Get("/api/v1/instance/extended_description")]
    Task<ExtendedDescription> ExtendedDescription();

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/instance/#privacy_policy">Mastodon API Documentation</see>
    [Get("/api/v1/instance/privacy_policy")]
    Task<PrivacyPolicy> PrivacyPolicy();

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/instance/#terms_of_service">Mastodon API Documentation</see>
    [Get("/api/v1/instance/terms_of_service")]
    Task<TermsOfService> TermsOfService();

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/instance/#terms_of_service_date">Mastodon API Documentation</see>
    [Get("/api/v1/instance/terms_of_service/{date}")]
    Task<TermsOfService> TermsOfServiceDate(string date);

    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/instance/#translation_languages">Mastodon API Documentation</see>
    [Get("/api/v1/instance/translation_languages")]
    Task<Dictionary<CultureInfo, List<CultureInfo>>> TranslationLanguages();
}

public sealed class WeeklyActivity {
    [JsonPropertyName("week")]
    public DateTime Week { get; set; } = DateTime.MinValue;

    [JsonPropertyName("statuses")]
    public long Statuses { get; set; } = 0;

    [JsonPropertyName("logins")]
    public long Logins { get; set; } = 0;

    [JsonPropertyName("registrations")]
    public long Registrations { get; set; } = 0;
}
