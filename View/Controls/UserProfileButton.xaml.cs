using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace View.Controls;
public sealed partial class UserProfileButton: Button {
    private readonly ViewModel.Controls.UserProfileButton? viewModel = Services.Provider?.GetService<ViewModel.Controls.UserProfileButton>();

    public UserProfileButton() {
        InitializeComponent();
    }
}
