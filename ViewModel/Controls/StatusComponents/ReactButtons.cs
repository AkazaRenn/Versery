using CommunityToolkit.Mvvm.ComponentModel;

namespace ViewModel.Controls.StatusComponents; 
public sealed partial class ReactButtons: ObservableObject {
    public string StatusId { get; set; } = string.Empty;

    [ObservableProperty]
    public partial bool Reblogged { get; set; } = false;
    [ObservableProperty]
    public partial bool Favourited { get; set; } = false;
    [ObservableProperty]
    public partial bool Bookmarked { get; set; } = false;
}
