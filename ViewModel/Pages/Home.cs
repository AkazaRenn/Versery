using CommunityToolkit.Mvvm.Messaging;
using Model;
using Model.Access;
using Model.Enumerations;
using ViewModel.Abstractions;
using ViewModel.Controls;

namespace ViewModel.Pages;

public sealed partial class Home: PaginatedViewModelCollection<Timeline, Model.Entities.Timeline> {
    protected override Model.Entities.Timeline[] GetFromDatabase(string? afterId = null) {
        if (!client.SignedIn) {
            return [];
        }
        return client.GetTimelineFromDatabase(afterId);
    }

    protected override async Task<Model.Entities.Timeline[]> GetFromServer(string? afterId = null) {
        if (!client.SignedIn) {
            return [];
        }
        return await client.GetTimelineFromServer(TimelineType.Home, afterId);
    }
}
