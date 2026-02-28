using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace View.Controls; 
public sealed partial class UserProfileButton: Button {
    private readonly ViewModel.Controls.UserProfileButton? viewModel = Services.Provider?.GetService<ViewModel.Controls.UserProfileButton>();

    public UserProfileButton() {
        InitializeComponent();

        DataContext = viewModel;
    }
}
