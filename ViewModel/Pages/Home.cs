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
        var timelines = await Task.Run(async () => {
            var timelines = await client.GetTimelineFromServer(TimelineType.Home);
            return Controls.Timeline.FromTimelines(timelines).ToArray();
        });

        if (timelines.Length == 0) {
            return;
        }

        var lastOverlapIndex = 0;
        for (int i = 0; i < Math.Min(timelines.Length * 2, Timelines.Count); i++) {
            if (Timelines[i] == timelines[^1]) {
                lastOverlapIndex = i;
                break;
            }
        }

        // No overlapping, add to the beginning
        if (lastOverlapIndex == 0) {
            for (int i = 0; i < timelines.Length; i++) {
                Timelines.Insert(i, timelines[i]);
            }
            return;
        }

        // Remove no-longer-existing ones, update overlapping ones
        var newIds = timelines.Select(t => t.Id).ToHashSet();
        for (int i = lastOverlapIndex; i >= 0; i--) {
            if (newIds.Contains(Timelines[i].Id)) {
                // try to update the existing entry
            } else {
                Timelines.RemoveAt(i);
            }
        }

        // Add the rest to the beginning
        var currentFirstId = Timelines[0].Id;
        for (int i = 0; i < timelines.Length; i++) {
            if (timelines[i].Id == currentFirstId) {
                break;
            }
            Timelines.Insert(i, timelines[i]);
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
