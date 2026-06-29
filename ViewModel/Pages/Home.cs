using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Model.Access;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Utilities;

namespace ViewModel.Pages;

public sealed partial class Home: IRecipient<Messages.SignInCompleted> {
    private readonly Client client = Utilities.Services.Get<Client>();
    private readonly Dictionary<string, List<Controls.Status>> idToStatusesDict = [];

    public ObservableCollection<Controls.Status> Statuses { get; } = [];

    public Home() {
        WeakReferenceMessenger.Default.RegisterAll(this);

        Statuses.CollectionChanged += Timelines_CollectionChanged;
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
            if (!idToStatusesDict.TryGetValue(status.Id, out var list)) {
                list = [];
                idToStatusesDict[status.Id] = list;
            }
            list.Add(status);
        }
    }

    private void RemoveFromIdToTimelinesDict(System.Collections.IList? statuses) {
        if (statuses is null) { 
            return; 
        }

        foreach (Controls.Status status in statuses) {
            if (idToStatusesDict.TryGetValue(status.Id, out var list)) {
                list.Remove(status);
                if (list.Count == 0) {
                    idToStatusesDict.Remove(status.Id);
                }
            }
        }
    }

    private void ResetIdToTimelinesDict() {
        idToStatusesDict.Clear();
        foreach (var status in Statuses) {
            if (!idToStatusesDict.TryGetValue(status.Id, out var list)) {
                list = [];
                idToStatusesDict[status.Id] = list;
            }
            list.Add(status);
        }
    }

    public void Receive(Messages.SignInCompleted message) {
        Statuses.Clear();
        var databaseTimelines = client.GetTimelineFromDatabase(Model.Enums.TimelineType.Home);
        foreach (var databaseTimeline in databaseTimelines) {
            Statuses.Add(new Controls.Status(databaseTimeline));
        }
    }

    //[RelayCommand]
    //void Load() {
    //    if (!client.Ready)
    //}
}
