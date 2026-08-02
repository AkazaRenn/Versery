using System.Text.Json.Serialization;

namespace Model.Server.Entities.Enumerations;
/// <summary>
/// Version: 4.3.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/RelationshipSeveranceEvent/#type">Mastodon API Documentation</see>
public enum RelationshipSeveranceEventType {
    /// <summary>
    /// A moderator suspended a whole domain
    /// </summary>
    [JsonStringEnumMemberName("domain_block")]
    DomainBlock,

    /// <summary>
    /// The user blocked a whole domain
    /// </summary>
    [JsonStringEnumMemberName("user_domain_block")]
    UserDomainBlock,

    /// <summary>
    /// A moderator suspended a specific account
    /// </summary>
    [JsonStringEnumMemberName("account_suspension")]
    AccountSuspension,
}