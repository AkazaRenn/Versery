using CommunityToolkit.Mvvm.Messaging;
using Model;
using Model.Access;
using Model.Enumerations;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ViewModel.Pages;

public sealed partial class Home: IRecipient<Messages.SignInCompleted> {
    private readonly Client client = Model.Services.Get<Client>();
    private bool loadingOldStatuses = false;
    private bool hasMoreStatusesToLoad = true;

    public ObservableCollection<Controls.Timeline> Statuses { get; } = [];

    public Home() {
        Statuses.CollectionChanged += Timelines_CollectionChanged;

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

    public void Receive(Messages.SignInCompleted message) {
        Statuses.Clear();
        _ = LoadInitialTimelines();
    }

    private async Task LoadInitialTimelines() {
        var timelines = client.GetTimelineFromDatabase(count: 10);
        if (timelines.Length == 0) {
            timelines = await client.GetTimelineFromServer(TimelineType.Home);
        }

        var statuses = Controls.Timeline.FromTimelines(timelines).ToArray();
        foreach (var status in statuses) {
            Statuses.Add(status);
        }
    }

    public async Task LoadLatestTimelines() {
        var statuses = await Task.Run(async () => {
            var timelines = await client.GetTimelineFromServer(TimelineType.Home);
            return Controls.Timeline.FromTimelines(timelines).ToArray();
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
            return Controls.Timeline.FromTimelines(timelines).ToArray();
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
