using System.Text.Json.Serialization;

namespace Model.Server.Methods.Entities;

public sealed class Markers {
    [JsonPropertyName("home")]
    public Model.Server.Entities.Marker? Home { get; set; }

    [JsonPropertyName("notifications")]
    public Model.Server.Entities.Marker? Notifications { get; set; }
}
