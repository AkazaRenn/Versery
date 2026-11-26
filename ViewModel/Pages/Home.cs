using Model.Enumerations;
using ViewModel.Abstractions;
using ViewModel.Controls;

namespace ViewModel.Pages;

public sealed partial class Home: PaginatedViewModelCollection<Timeline, Model.Entities.Timeline> {
    protected override Timeline CreateViewModel(Model.Entities.Timeline entity) {
        return new(entity);
    }

    protected override Model.Entities.Timeline[] GetFromDatabase(uint count, string? afterId = null) {
        return client.GetTimelineFromDatabase(count, afterId);
    }

    protected override async Task<Model.Entities.Timeline[]> GetFromServer(uint count, string? afterId = null) {
        return await client.GetTimelineFromServer(TimelineType.Home, count, afterId);
    }
}
