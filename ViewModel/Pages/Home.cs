using CommunityToolkit.Mvvm.Messaging;
using Model;
using Model.Access;
using Model.Enumerations;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ViewModel.Pages;

public sealed partial class Home: IRecipient<WeakMessages.SignInCompleted> {
    private readonly Client client = Model.Services.Get<Client>();
    private bool loadingOldStatuses = false;
    private bool hasMoreStatusesToLoad = true;

    public ObservableCollection<Controls.Timeline> Timelines { get; } = [];

    public Home() {
        Timelines.CollectionChanged += Timelines_CollectionChanged;

        StrongReferenceMessenger.Default.RegisterAll(this);
        _ = LoadInitialTimelines();
    }

    private void Timelines_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
        switch (e.Action) {
        case NotifyCollectionChangedAction.Add:
        case NotifyCollectionChangedAction.Replace:
            if (e.NewItems is null) {
                return;
            }

            foreach (Controls.Timeline status in e.NewItems) {
                _ = status.DownloadMedias();
            }
            break;
        }
    }

    public void Receive(WeakMessages.SignInCompleted message) {
        Timelines.Clear();
        _ = LoadInitialTimelines();
    }

    private async Task LoadInitialTimelines() {
        var timelines = client.GetTimelineFromDatabase(count: 10);
        if (timelines.Length == 0) {
            timelines = await client.GetTimelineFromServer(TimelineType.Home);
        }

        var statuses = Controls.Timeline.FromTimelines(timelines).ToArray();
        foreach (var status in statuses) {
            Timelines.Add(status);
        }
    }

    public async Task LoadLatestTimelines() {
        var serverTimelines = await Task.Run(async () => {
            return await client.GetTimelineFromServer(TimelineType.Home);
        });

        if (serverTimelines.Length == 0) {
            return;
        }

        // No overlapping, add all to the beginning
        if (serverTimelines[^1].FollowedByGap) {
            var newTimelines = await Task.Run(() => {
                return Controls.Timeline.FromTimelines(serverTimelines).ToArray();
            });
            for (int i = 0; i < newTimelines.Length; i++) {
                Timelines.Insert(i, newTimelines[i]);
            }
            return;
        }

        { // Remove no-longer-existing ones, update overlapping ones
            var newTimelineIds = serverTimelines.Select(t => t.Id).ToHashSet();
            var indexToRemove = new List<int>();
            for (int i = 0; i < Timelines.Count; i++) {
                if (Timelines[i].Id == serverTimelines[^1].Id) {
                    break;
                }

                if (newTimelineIds.Contains(Timelines[i].Id)) {
                    // try to update the timeline
                } else {
                    indexToRemove.Add(i);
                }
            }

            for (int i = indexToRemove.Count - 1; i >= 0; i--) {
                Timelines.RemoveAt(indexToRemove[i]);
            }
        }

        { // Add the rest to the beginning
            var newTimelines = await Task.Run(() => {
                var timelinesToAddEndIndex = Array.FindIndex(serverTimelines, x => x.Id == Timelines[0].Id);
                Model.Entities.Timeline[] timelinesToAdd;
                if (timelinesToAddEndIndex < 0) {
                    timelinesToAdd = serverTimelines;
                } else {
                    timelinesToAdd = serverTimelines[..timelinesToAddEndIndex];
                }
                return Controls.Timeline.FromTimelines(timelinesToAdd).ToArray();
            });
            for (int i = 0; i < newTimelines.Length; i++) {
                Timelines.Insert(i, newTimelines[i]);
            }
        }
    }

    public async Task OnStatusRealized(int index) {
        if ((index < Timelines.Count - 1) ||
            !Timelines.Any() ||
            loadingOldStatuses ||
            !hasMoreStatusesToLoad) {
            return;
        }

        loadingOldStatuses = true;
        var statuses = await Task.Run(() => {
            var timelines = client.GetTimelineFromDatabase(Timelines.Last().Id);
            return Controls.Timeline.FromTimelines(timelines).ToArray();
        });
        if (statuses.Length == 0) {
            hasMoreStatusesToLoad = false;
        } else {
            foreach (var status in statuses) {
                Timelines.Add(status);
            }
        }
        loadingOldStatuses = false;
    }
}
