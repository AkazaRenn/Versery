using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model.Access;

namespace ViewModel.Shared;

public sealed partial class PreviewCard(Model.Server.Entities.PreviewCard card): ObservableObject {
    public Uri? Url { get; } = card.Url;
    public string Title { get; } = card.Title;
    public string Description { get; } = card.Description;
    public string ProviderName { get; } = card.ProviderName;
    [RelayCommand]
    private void Invoke() {
        if (Url is not null) {
            _ = Windows.System.Launcher.LaunchUriAsync(Url);
        }
    }

    public Uri? ImageRemote { get; } = card.Image;
    [ObservableProperty]
    public partial Uri? Image { get; private set; } = null;
    internal async void DownloadImage() {
        if (Image is null && ImageRemote is not null) {
            Image = await Cache.Get(ImageRemote);
        }
    }
}
