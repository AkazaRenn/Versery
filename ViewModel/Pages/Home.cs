using CommunityToolkit.Mvvm.Messaging;
using Model;
using Model.Access;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ViewModel.Pages;

public sealed partial class Home: IRecipient<Messages.SignInCompleted> {
    private readonly Client client = Model.Services.Get<Client>();
    private readonly Dictionary<string, List<Controls.Status>> contentIdToStatusesDict = [];
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
        var timelines = await client.GetTimelineFromDatabase();
        if (!timelines.Any()) {
            timelines = await client.GetTimelineFromServer(Model.Enums.TimelineType.Home);
        }

        foreach (var status in Controls.Status.FromTimelines(timelines)) {
            Statuses.Add(status);
        }
    }

    public async Task LoadLatestTimelines() {
        var timelines = await client.GetTimelineFromServer(Model.Enums.TimelineType.Home);

        int index = 0;
        foreach (var status in Controls.Status.FromTimelines(timelines)) {
            Statuses.Insert(index++, status);
        }
    }

    public async Task OnStatusRealized(int index) {
        if ((index < Statuses.Count - 5) || !Statuses.Any() || loadingOldStatuses) {
            return;
        }

        loadingOldStatuses = true;
        var timelines = await client.GetTimelineFromDatabase(Statuses.Last().Id);
        foreach (var status in Controls.Status.FromTimelines(timelines)) {
            Statuses.Add(status);
        }
        loadingOldStatuses = false;
    }

    //[RelayCommand]
    //void Load() {
    //    if (!client.Ready)
    //}
}
