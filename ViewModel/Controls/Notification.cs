using CommunityToolkit.Mvvm.ComponentModel;
using Model;
using Model.Utilities;
using System.Collections.ObjectModel;
using ViewModel.Enumerations;
using ViewModel.Interfaces;

namespace ViewModel.Controls;

public sealed partial class Notification: ObservableObject, IPaginatedViewModel<Notification, Model.Entities.Notification> {
    private static readonly Dictionary<string, WeakReference<Notification>> cache = [];

    private readonly Sentinel<string, Notification> sentinel;
    private int version = 0;

    public string Id { get; }
    public string PaginateId { get; private set; }

    [ObservableProperty]
    public partial int NotificationsCount { get; private set; } = 0;
    public NotificationType Type { get; } = NotificationType.Unknown;
    public ObservableCollection<Account> SampleAccounts { get; } = [];
    public Status? Status { get; private set; } = null;

    private Notification(Model.Entities.Notification notification, int version) {
        Id = notification.Id;
        PaginateId = notification.PageMinId ?? notification.Id;
        NotificationsCount = notification.NotificationsCount;
        Type = notification.Type.Client;

        foreach (var accountId in notification.SampleAccountIds) {
            SampleAccounts.Add(Account.Create(accountId));
        }
        if (!string.IsNullOrWhiteSpace(notification.StatusId)) {
            Status = Status.Create(notification.StatusId);
        }

        sentinel = new(Id, cache);
        this.version = version;
    }

    public void Update(int version) {
        if (this.version == version) {
            return;
        }

        //throw new NotImplementedException();
        this.version = version;
    }

    public static IEnumerable<Notification> FromEntities(IEnumerable<Model.Entities.Notification> Notifications, int version) {
        Notifications = Notifications.ToCollection();
        for (int i = 0; i < Notifications.Count(); i++) {
            yield return Create(Notifications.ElementAt(i), version);
        }
    }

    public async void DownloadMedia() {
        Status?.DownloadMedia();
        foreach (var account in SampleAccounts) {
            account.DownloadAvatar();
        }
    }

    public static Notification Create(Model.Entities.Notification Notification, int version) {
        lock (cache) {
            if ((!cache.TryGetValue(Notification.Id, out var reference)) ||
                (!reference.TryGetTarget(out var obj))) {
                obj = new(Notification, version);
                cache[Notification.Id] = new(obj);
            }
            return obj;
        }
    }
}