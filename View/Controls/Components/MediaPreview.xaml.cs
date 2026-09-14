using Microsoft.UI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System.ComponentModel;

namespace View.Controls.Components;

public sealed partial class MediaPreview: Grid, INotifyPropertyChanged {
    public Uri? UriSource {
        get => BitmapImage.UriSource;
        set => BitmapImage.UriSource = value;
    }

    public string BackgroundBlurHash { 
        get;
        set { 
            if (field != value) {
                field = value;
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BackgroundBlurHash)));
            }
        } 
    } = string.Empty;
    static public Brush GetBackground(string BackgroundBlurHash) {
        if (String.IsNullOrEmpty(BackgroundBlurHash)) {
            return new SolidColorBrush(Colors.Red);
        } else {
            return new SolidColorBrush(Colors.Blue);
        }
    }

    public string OverlayText { 
        get; 
        set {
            if (field != value) {
                field = value;
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OverlayText)));
            }
        }
    } = string.Empty;
    static public bool LoadOverlay(string overlayText) => !String.IsNullOrEmpty(overlayText);

    public MediaPreview() {
        InitializeComponent();
    }
    public event PropertyChangedEventHandler? PropertyChanged;
}
