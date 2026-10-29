using CommunityToolkit.Mvvm.ComponentModel;

namespace ViewModel.Controls;

public sealed partial class PostPanel: ObservableObject {
    [ObservableProperty]
    public partial bool Up { get; set; } = false;
}
