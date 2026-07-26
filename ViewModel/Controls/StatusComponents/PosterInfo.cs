using CommunityToolkit.Mvvm.ComponentModel;
using ViewModel.Controls.RichTextRenderer;

namespace ViewModel.Controls.StatusComponents; 
public sealed partial class PosterInfo: ObservableObject {
    public string AccountId { get; set; } = string.Empty;

    [ObservableProperty]
    public partial TextWithEmoji DisplayNameInfo { get; set; } = new();
    [ObservableProperty]
    public partial string AccountName { get; set; } = string.Empty;
}
