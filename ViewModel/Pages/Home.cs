using CommunityToolkit.Mvvm.Messaging;
using Model;
using Model.Access;
using Model.Enumerations;
using ViewModel.Abstractions;
using ViewModel.Controls;

namespace ViewModel.Pages;

public sealed partial class Home: PaginatedViewModelCollection<Timeline, Model.Entities.Timeline>, IRecipient<Messages.SignInCompleted> {
    private readonly Client client = Model.Services.Get<Client>();

    public Home() {
        StrongReferenceMessenger.Default.Register<Messages.SignInCompleted>(this);
    }

    protected override void OnAddToViewModelsCollection(Timeline viewModel) {
        _ = viewModel.DownloadMedias();
    }

    protected override Model.Entities.Timeline[] GetFromDatabase(string? afterId = null) {
        if (!client.SignedIn) {
            return [];
        }
        return client.GetTimelineFromDatabase(afterId);
    }

    protected override async Task<Model.Entities.Timeline[]> GetFromServer() {
        if (!client.SignedIn) {
            return [];
        }
        return await client.GetTimelineFromServer(TimelineType.Home);
    }

    public void Receive(Messages.SignInCompleted message) {
        ViewModels.Clear();
        _ = LoadInitialItems();
    }
}
