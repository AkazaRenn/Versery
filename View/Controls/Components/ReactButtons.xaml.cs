using FluentIcons.Common;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Windows.Input;

namespace View.Controls.Components;

internal sealed partial class ReactButtons: Grid {
    public ICommand ReplyButtonCommand {
        get => ReplyButton.Command;
        set => ReplyButton?.Command = value;
    }
    public bool ReblogButtonIsEnabled {
        get => ReblogButton.IsEnabled;
        set => ReblogButton?.IsEnabled = value;
    }
    public ICommand ReblogButtonCommand {
        get => ReblogButton.Command;
        set => ReblogButton?.Command = value;
    }
    public bool IsReblogged {
        get => ReblogIconInactive.Opacity == 0;
        set {
            ReblogIconInactive?.Opacity = value ? 0 : 1;
            ReblogIconActive?.Opacity = value ? 1 : 0;
        }
    }
    public ICommand FavouriteButtonCommand {
        get => FavouriteButton.Command;
        set => FavouriteButton?.Command = value;
    }
    public bool IsFavourited {
        get => FavouriteIconInactive.Opacity == 0;
        set {
            FavouriteIconInactive?.Opacity = value ? 0 : 1;
            FavouriteIconActive?.Opacity = value ? 1 : 0;
        }
    }

    internal static Thickness ButtonPadding => new(4, 4, 4, 4);
    internal static double FontSize => 16;
    internal static Icon GetReblogIcon(bool canReblog) => canReblog ? Icon.ArrowRepeatAll : Icon.ArrowRepeatAllOff;

    public ReactButtons() {
        InitializeComponent();
    }
}