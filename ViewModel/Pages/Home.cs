using CommunityToolkit.Mvvm.Messaging;
using Model.Access;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Utilities;

namespace ViewModel.Pages;

public sealed partial class Home: IRecipient<Messages.SignInCompleted> {
    private readonly Client client = Utilities.Services.Get<Client>();
    private readonly Dictionary<string, List<Controls.Status>> contentIdToStatusesDict = [];
    private readonly HashSet<string> statusIds = [];
    private bool loadingOldStatuses = false;

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
            statusIds.Add(status.Id);
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
            statusIds.Remove(status.Id);
        }
    }

    private void ResetIdToTimelinesDict() {
        contentIdToStatusesDict.Clear();
        statusIds.Clear();
        foreach (var status in Statuses) {
            if (!contentIdToStatusesDict.TryGetValue(status.ContentId, out var list)) {
                list = [];
                contentIdToStatusesDict[status.ContentId] = list;
            }
            list.Add(status);
            statusIds.Add(status.Id);
        }
    }

    public void Receive(Messages.SignInCompleted message) {
        Statuses.Clear();
        _ = LoadInitialTimelines();
    }

    private async Task LoadInitialTimelines() {
        var timelines = client.GetTimelineFromDatabase(Model.Enums.TimelineType.Home);
        if (!timelines.Any()) {
            timelines = await client.GetTimelineFromServer(Model.Enums.TimelineType.Home);
        }
        foreach (var timeline in timelines) {
            Statuses.Add(new Controls.Status(timeline));
        }
    }

    public async Task LoadLatestTimelines() {
        var timelines = await client.GetTimelineFromServer(Model.Enums.TimelineType.Home);

        int index = 0;
        foreach (var timeline in timelines) {
            if (!statusIds.Contains(timeline.Id)) {
                Statuses.Insert(index++, new Controls.Status(timeline));
            }
        }
    }

    public async Task OnStatusRealized(int index) {
        if ((index < Statuses.Count - 5) || !Statuses.Any() || loadingOldStatuses) {
            return;
        }

        loadingOldStatuses = true;
        var timelines = await Task.Run(() => 
            client.GetTimelineFromDatabase(Model.Enums.TimelineType.Home, Statuses.Last().Id)
        );
        foreach (var timeline in timelines) {
            Statuses.Add(new Controls.Status(timeline));
        }
        loadingOldStatuses = false;
    }

    //[RelayCommand]
    //void Load() {
    //    if (!client.Ready)
    //}
}
