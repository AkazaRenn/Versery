using CommunityToolkit.Mvvm.ComponentModel;
using Model.Access;
using System.Collections.ObjectModel;
using ViewModel.Abstractions;
using ViewModel.Controls;
using ViewModel.Enumerations;
using ViewModel.Interfaces;

namespace ViewModel.Shared;

public sealed partial class NotificationGroup: CachedViewModel<NotificationGroup>, ICachedViewModel<NotificationGroup> {
    private static readonly Client client = Model.Services.Get<Client>();
    private int version = 0;

    [ObservableProperty]
    public partial int NotificationsCount { get; private set; } = 0;
    public NotificationType Type { get; } = NotificationType.Unknown;
    public ObservableCollection<Account> SampleAccounts { get; } = [];
    public Status? Status { get; } = null;

    private NotificationGroup(string groupKey) : base(groupKey) {
        var group = client.GetNotificationGroup(groupKey)!;
        NotificationsCount = group.NotificationsCount;
        Type = group.Type.Client;

        foreach (var accountId in group.SampleAccountIds) {
            SampleAccounts.Add(Account.Create(accountId));
        }
        if (!string.IsNullOrWhiteSpace(group.StatusId)) {
            Status = Status.Create(group.StatusId);
        }
    }

    static NotificationGroup ICachedViewModel<NotificationGroup>.New(string id) => new(id);

    internal void Update(int version) {
        if (this.version == version) {
            return;
        }

        this.version = version;
        Status?.Update(version);
        foreach (var account in SampleAccounts) {
            account.Update(version);
        }
    }

    internal void DownloadMedia() {
        Status?.DownloadMedia();
        foreach (var account in SampleAccounts) {
            account.DownloadAvatar();
        }
    }
}
