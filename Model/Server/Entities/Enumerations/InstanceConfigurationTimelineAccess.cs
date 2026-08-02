using System.Text.Json.Serialization;

namespace Model.Server.Entities.Enumerations;
/// <see href="https://docs.joinmastodon.org/entities/Instance/#timeline_access">
public enum InstanceConfigurationTimelineAccess {
    [JsonStringEnumMemberName("public")]
    Public,

    [JsonStringEnumMemberName("authenticated")]
    Authenticated,

    [JsonStringEnumMemberName("disabled")]
    Disabled,
}