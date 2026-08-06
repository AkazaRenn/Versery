using CommunityToolkit.Mvvm.Messaging;
using Model;
using Model.Access;
using Model.Enumerations;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ViewModel.Pages;

public sealed partial class Home: IRecipient<Messages.SignInCompleted> {
    private readonly Client client = Model.Services.Get<Client>();
    private readonly Dictionary<string, List<Controls.Status>> contentIdToStatusesDict = [];
    private bool loadingOldStatuses = false;
    private bool hasMoreStatusesToLoad = true;

    public ObservableCollection<Controls.Status> Statuses { get; } = [];

    public Home() {
        Statuses.CollectionChanged += Timelines_CollectionChanged;

        _ = LoadInitialTimelines();
        WeakReferenceMessenger.Default.RegisterAll(this);
    }

    private void Timelines_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
        switch (e.Action) {
        case NotifyCollectionChangedAction.Add:
            AddToIdToTimelinesDict(e.NewItems);
            break;
        case NotifyCollectionChangedAction.Remove:
            RemoveFromIdToTimelinesDict(e.OldItems);
            break;
        case NotifyCollectionChangedAction.Replace:
            RemoveFromIdToTimelinesDict(e.OldItems);
            AddToIdToTimelinesDict(e.NewItems);
            break;
        case NotifyCollectionChangedAction.Reset:
            ResetIdToTimelinesDict();
            break;
        }
    }

    private void AddToIdToTimelinesDict(System.Collections.IList? statuses) {
        if (statuses is null) {
            return;
        }

        foreach (Controls.Status status in statuses) {
            if (!contentIdToStatusesDict.TryGetValue(status.ContentId, out var list)) {
                list = [];
                contentIdToStatusesDict[status.ContentId] = list;
            }
            list.Add(status);
            _ = status.DownloadMedias();
        }
    }

    private void RemoveFromIdToTimelinesDict(System.Collections.IList? statuses) {
        if (statuses is null) {
            return;
        }

        foreach (Controls.Status status in statuses) {
            if (contentIdToStatusesDict.TryGetValue(status.ContentId, out var list)) {
                list.Remove(status);
                if (list.Count == 0) {
                    contentIdToStatusesDict.Remove(status.ContentId);
                }
            }
        }
    }

    private void ResetIdToTimelinesDict() {
        contentIdToStatusesDict.Clear();
        foreach (var status in Statuses) {
            if (!contentIdToStatusesDict.TryGetValue(status.ContentId, out var list)) {
                list = [];
                contentIdToStatusesDict[status.ContentId] = list;
            }
            list.Add(status);
        }
    }

    public void Receive(Messages.SignInCompleted message) {
        Statuses.Clear();
        _ = LoadInitialTimelines();
    }

    private async Task LoadInitialTimelines() {
        var statuses = await Task.Run(async () => {
            var timelines = client.GetTimelineFromDatabase(count: 10);
            if (timelines.Length == 0) {
                timelines = await client.GetTimelineFromServer(TimelineType.Home);
            }
            return Controls.Status.FromTimelines(timelines).ToArray();
        });

        foreach (var status in statuses) {
            Statuses.Add(status);
        }
    }

    public async Task LoadLatestTimelines() {
        var statuses = await Task.Run(async () => {
            var timelines = await client.GetTimelineFromServer(TimelineType.Home);
            return Controls.Status.FromTimelines(timelines).ToArray();
        });

        for (int i = 0; i < statuses.Length; i++) {
            Statuses.Insert(i, statuses[i]);
        }
    }

    public async Task OnStatusRealized(int index) {
        if ((index < Statuses.Count - 1) ||
            !Statuses.Any() ||
            loadingOldStatuses ||
            !hasMoreStatusesToLoad) {
            return;
        }

        loadingOldStatuses = true;
        var statuses = await Task.Run(() => {
            var timelines = client.GetTimelineFromDatabase(Statuses.Last().Id);
            return Controls.Status.FromTimelines(timelines).ToArray();
        });
        if (statuses.Length == 0) {
            hasMoreStatusesToLoad = false;
        } else {
            foreach (var status in statuses) {
                Statuses.Add(status);
            }
        }
        loadingOldStatuses = false;
    }
}
