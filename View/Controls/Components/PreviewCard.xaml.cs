using Microsoft.UI.Xaml.Controls;

namespace View.Controls.Components;

public sealed partial class PreviewCard: Button {
    public bool LoadImage {
        get => ImageRectangle is not null;
        set {
            if (value) {
                FindName(nameof(ImageRectangle));
            } else {
                UnloadObject(ImageRectangle);
            }
        }
    }
    public Uri ImageSource {
        get => Image.UriSource;
        set => Image?.UriSource = value;
    }
    public string Title {
        get => TitleTextBlock.Text;
        set => TitleTextBlock?.Text = value;
    }
    public string Description {
        get => DescriptionTextBlock.Text;
        set => DescriptionTextBlock?.Text = value;
    }
    public bool LoadProviderName {
        get => ProviderNameTextBlock is not null;
        set {
            if (value) {
                FindName(nameof(ProviderNameTextBlock));
            } else {
                UnloadObject(ProviderNameTextBlock);
            }
        }
    }
    public string ProviderName {
        get => ProviderNameTextBlock.Text;
        set => ProviderNameTextBlock?.Text = value;
    }

    public PreviewCard() {
        this.InitializeComponent();
    }
}