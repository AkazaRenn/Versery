using System.Text.Json.Serialization;

namespace Model.Server.Entities.Enumerations;
/// <see href="https://docs.joinmastodon.org/entities/RelationshipSeveranceEvent/#type">Mastodon API Documentation</see>
public enum RelationshipSeveranceEventType {
    [JsonStringEnumMemberName("domain_block")]
    DomainBlock,

    [JsonStringEnumMemberName("user_domain_block")]
    UserDomainBlock,

    [JsonStringEnumMemberName("account_suspension")]
    AccountSuspension,
}