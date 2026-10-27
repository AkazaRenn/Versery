using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace View.Utilities;

public class DialogService {
    private ContentDialog? current = null;

    public IAsyncOperation<ContentDialogResult> Show(ContentDialog dialog) {
        current?.Hide();
        current = dialog;
        dialog.Closed += (s, e) => current = null;

        return dialog.ShowAsync();
    }
}