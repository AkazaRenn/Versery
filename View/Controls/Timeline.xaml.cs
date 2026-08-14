using FluentIcons.Common;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using View.Interfaces;

namespace View.Controls;

internal sealed partial class Timeline: Grid {
    private Window? window;

    public ViewModel.Controls.Timeline? ViewModel {
        get;
        set {
            if (field != value) {
                field = value;
                if (DispatcherQueue.HasThreadAccess) {
                    Bindings.Update();
                } else {
                    _ = DispatcherQueue.TryEnqueue(Bindings.Update);
                }
            }
        }
    }

    public Timeline() {
        InitializeComponent();
    }

    private void Grid_Loaded(object sender, RoutedEventArgs e) {
    //    if ((window is null) && (Application.Current is IWindowHelper windowHelper)) {
    //        if (windowHelper.TryGetWindow(this, out window)) {
    //            window?.Activated -= Window_Activated;
    //            window?.Activated += Window_Activated;
    //        }
    //    }
    }

    private void Grid_Unloaded(object sender, RoutedEventArgs e) {
        //window?.Activated -= Window_Activated;
    }

    //private void Window_Activated(object sender, WindowActivatedEventArgs args) {
    //    switch (args.WindowActivationState) {
    //    case WindowActivationState.Deactivated:
    //        PosterAvatarBitmapImage?.Stop();
    //        RebloggerAvatarBitmapImage?.Stop();
    //        break;
    //    default:
    //        PosterAvatarBitmapImage?.Play();
    //        RebloggerAvatarBitmapImage?.Play();
    //        break;
    //    }
    //}

    private double PosterAvatarScale => ViewModel?.Reblogger is null ? 1 : 0.9;
}
