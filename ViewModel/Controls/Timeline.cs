using ViewModel.Abstractions;
using ViewModel.Shared;

namespace ViewModel.Controls;

public sealed partial class Timeline: PaginatedViewModel {
    public Account? Reblogger { get; } = null;
    public Status Content { get; }

    internal Timeline(Model.Entities.Timeline timeline) : base(timeline.Id) {
        Content = Status.Create(timeline.Reblog ?? Id);
        if (timeline.Reblogger is not null) {
            Reblogger = Account.Create(timeline.Reblogger);
        }
    }

    public override void Update(int version) {
        Content.Update(version);
        Reblogger?.Update(version);
    }

    public override void DownloadMedia() {
        Content.DownloadMedia();
        Reblogger?.DownloadAvatar();
    }
}