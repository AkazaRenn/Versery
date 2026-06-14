using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace View.Controls;
internal sealed partial class UserProfileButton: Button {
    private readonly ViewModel.Controls.UserProfileButton viewModel = Utilities.Services.Provider.GetRequiredService<ViewModel.Controls.UserProfileButton>();

    public UserProfileButton() {
        InitializeComponent();
    }
}
