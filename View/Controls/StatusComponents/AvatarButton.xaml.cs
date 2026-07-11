using Microsoft.UI.Xaml.Controls;

namespace View.Controls.StatusComponents; 
public sealed partial class AvatarButton: Button {
    public Uri Avatar { 
        get => AvatarImage.UriSource;
        set => AvatarImage.UriSource = value;
    }

    public AvatarButton() {
        InitializeComponent();
    }
}
