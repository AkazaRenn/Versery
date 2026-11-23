using CommunityToolkit.Mvvm.ComponentModel;
using Model;
using Model.Access;
using Model.Utilities;
using ViewModel.Interfaces;

namespace ViewModel.Controls;

public sealed partial class Notification: ObservableObject, IPaginatedViewModel<Notification, Model.Entities.Notification> {
    private static readonly Client client = Model.Services.Get<Client>();
    private static readonly Dictionary<string, WeakReference<Notification>> cache = [];

    private readonly Sentinel<string, Notification> sentinel;
    private int version = 0;

    public string Id { get; }

    public Status.Account? Reblogger { get; } = null;
    public Status Content { get; }

    private Notification(Model.Entities.Notification Notification, int version) {
        Id = Notification.Id;
        var status = client.GetStatus(Id)!;

        if (string.IsNullOrWhiteSpace(status.Reblog)) {
            Content = Status.Create(Id);
        } else {
            Reblogger = Status.Account.Create(status.Account);
            Content = Status.Create(status.Reblog);
        }

        sentinel = new(Id, cache);
        this.version = version;
    }

    public void Update(int version) {
        //throw new NotImplementedException();
    }

    public static IEnumerable<Notification> FromEntities(IEnumerable<Model.Entities.Notification> Notifications, int version) {
        Notifications = Notifications.ToCollection();
        for (int i = 0; i < Notifications.Count(); i++) {
            yield return Create(Notifications.ElementAt(i), version);
        }
    }

    public async void DownloadMedia() {
        _ = Content.DownloadMedia();
        _ = Reblogger?.DownloadAvatar();
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