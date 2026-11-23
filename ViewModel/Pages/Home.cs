using Model.Enumerations;
using ViewModel.Abstractions;
using ViewModel.Controls;

namespace ViewModel.Pages;

public sealed partial class Home: PaginatedViewModelCollection<Timeline, Model.Entities.Timeline> {
    protected override Model.Entities.Timeline[] GetFromDatabase(int count, string? afterId = null) {
        return client.GetTimelineFromDatabase(count, afterId);
    }

    protected override async Task<Model.Entities.Timeline[]> GetFromServer(int count, string? afterId = null) {
        return await client.GetTimelineFromServer(TimelineType.Home, count, afterId);
    }
}
