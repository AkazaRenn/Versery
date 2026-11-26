using CommunityToolkit.Mvvm.ComponentModel;
using Model.Access;

namespace ViewModel.Shared;

public sealed partial class MediaPreview(Model.Server.Entities.MediaAttachment media): ObservableObject {
    public double Aspect { get; } = media.Meta?.Original?.Aspect ?? 1;
    public string? Blurhash { get; } = media.Blurhash;
    public string? Description { get; } = media.Description;
    public Uri? Full { get; } = media.RemoteUrl;

    public Uri? Remote { get; } = media.PreviewUrl;
    [ObservableProperty]
    public partial Uri? Uri { get; private set; } = null;
    internal async void Download() {
        if (Uri is null && Remote is not null) {
            Uri = await Cache.Get(Remote);
        }
    }
}
