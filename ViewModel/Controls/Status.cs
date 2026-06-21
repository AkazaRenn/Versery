using CommunityToolkit.Mvvm.ComponentModel;
using Model.Access;

namespace ViewModel.Controls;

public sealed partial class Status: ObservableObject {
    private readonly Client client = Utilities.Services.Get<Client>();
    private readonly Model.Entities.Status? status;

    public string PosterId { get; } = string.Empty;
    public string DisplayName { get; } = string.Empty;
    public DateTime CreatedAt { get; } = DateTime.MinValue;
    public string Uri { get; } = string.Empty;
    public string Content { get; } = string.Empty;

    public Status(Model.Entities.Timeline timeline) {
        if (client.GetStatus(timeline.Id) is Model.Entities.Status status) {
            PosterId = "dfsoiajfdia@dfojajfoa.cojifaj";
            DisplayName = "fdsoijfodia";
            CreatedAt = status.CreatedAt;
        }
    }
}
