using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;

namespace ViewModel.Controls;

public sealed partial class PostPanel: ObservableObject {
    [ObservableProperty]
    public partial bool Up { get; set; } = false;
}
