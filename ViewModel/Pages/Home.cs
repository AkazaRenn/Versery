using Model.Enumerations;
using ViewModel.Abstractions;
using ViewModel.Controls;

namespace ViewModel.Pages;

public sealed partial class Home: PaginatedViewModelCollection<Timeline, Model.Entities.Timeline> {
    protected override Model.Entities.Timeline[] GetFromDatabase(string? afterId = null) {
        return client.GetTimelineFromDatabase(afterId);
    }

    protected override async Task<Model.Entities.Timeline[]> GetFromServer(string? afterId = null) {
        return await client.GetTimelineFromServer(TimelineType.Home, afterId);
    }
}
