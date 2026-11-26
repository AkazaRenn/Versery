using CommunityToolkit.Mvvm.ComponentModel;
using Model.Access;

namespace ViewModel.Shared;

public sealed partial class Emoji(string shortCode, Uri? sourceRemote = null): ObservableObject {
    public string Shortcode => shortCode;
    [ObservableProperty]
    public partial Uri? Source { get; private set; } = null;
    internal async void Load() {
        if (sourceRemote is not null) {
            Source = await Cache.Get(sourceRemote);
        }
    }
}
