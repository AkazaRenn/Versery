using CommunityToolkit.Mvvm.ComponentModel;

namespace ViewModel.Controls.StatusComponents; 
public sealed partial class ReactButtons: ObservableObject {
    public string StatusId { get; set; } = string.Empty;

    [ObservableProperty]
    public partial bool HasReplies { get; set; } = false;
    [ObservableProperty]
    public partial bool CanBeReblogged { get; set; } = true;
    [ObservableProperty]
    public partial bool IsReblogged { get; set; } = false;
    [ObservableProperty]
    public partial bool IsFavourited { get; set; } = false;
    [ObservableProperty]
    public partial bool IsBookmarked { get; set; } = false;
}
