using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace View.Controls;

internal sealed partial class UserProfileButton: UserControl {
    private readonly ViewModel.Controls.UserProfileButton viewModel = new();

    public Uri? AvatarUri {
        get => BitmapImage.UriSource;
        set => BitmapImage.UriSource = value;
    }
    new public CornerRadius CornerRadius {
        get => ImageButton.CornerRadius;
        set => ImageButton.CornerRadius = value;
    }

    public UserProfileButton() {
        InitializeComponent();
    }
}
