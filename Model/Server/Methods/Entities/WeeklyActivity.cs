using System.Text.Json.Serialization;

namespace Model.Server.Methods.Entities;
/// <summary>
/// Version: 3.0.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/methods/instance/#response-2">Mastodon API Documentation</see>
public sealed class WeeklyActivity {
    /// <summary>
    /// Midnight at the first day of the week.
    /// </summary>
    [JsonPropertyName("week")]
    public DateTime Week { get; set; } = DateTime.MinValue;

    /// <summary>
    /// Statuses created since the week began.
    /// </summary>
    [JsonPropertyName("statuses")]
    public long Statuses { get; set; } = 0;

    /// <summary>
    /// User logins since the week began.
    /// </summary>
    [JsonPropertyName("logins")]
    public long Logins { get; set; } = 0;

    /// <summary>
    /// User registrations since the week began.
    /// </summary>
    [JsonPropertyName("registrations")]
    public long Registrations { get; set; } = 0;
}
