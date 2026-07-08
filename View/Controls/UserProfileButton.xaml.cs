using Microsoft.UI.Xaml.Controls;

namespace View.Controls;
internal sealed partial class UserProfileButton: Button {
    private readonly ViewModel.Controls.UserProfileButton viewModel = new();

    public UserProfileButton() {
        InitializeComponent();
    }
}
