using ViewModel.Abstractions;
using ViewModel.Shared;

namespace ViewModel.Controls;

public sealed partial class Notification: PaginatedViewModel {
    public override string PaginateId { get; }

    public NotificationGroup Group { get; }

    internal Notification(Model.Entities.Notification notification) : base(notification.Id) {
        Group = NotificationGroup.Create(notification.GroupKey);
        PaginateId = notification.PageMinId ?? Id;
    }

    public override void Update(int version) {
        //throw new NotImplementedException();
        Group.Update(version);
    }

    public override void DownloadMedia() {
        Group.DownloadMedia();
    }
}