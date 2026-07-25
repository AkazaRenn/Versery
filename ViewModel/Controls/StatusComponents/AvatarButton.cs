using CommunityToolkit.Mvvm.ComponentModel;

namespace ViewModel.Controls.StatusComponents; 
public sealed partial class AvatarButton: ObservableObject {
    [ObservableProperty]
    public partial string ContentPosterId { get; set; } = string.Empty;
    [ObservableProperty]
    public partial Uri? ContentPosterAvatar { get; set; }
    [ObservableProperty]
    public partial Uri? RebloggerAvatar { get; set; }
    [ObservableProperty]
    public partial bool IsReblog { get; set; } = false;
}
