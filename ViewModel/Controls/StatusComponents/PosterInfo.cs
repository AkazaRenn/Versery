using CommunityToolkit.Mvvm.ComponentModel;

namespace ViewModel.Controls.StatusComponents; 
public sealed partial class PosterInfo: ObservableObject {
    public string Id { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string DisplayName { get; set; } = string.Empty;
    [ObservableProperty]
    public partial string AccountName { get; set; } = string.Empty;
}
