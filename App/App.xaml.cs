using Microsoft.UI.Xaml;

namespace App;

public partial class App : Application
{
    private Window? mainWindow;

    public App()
    {
        InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        mainWindow = new View.MainWindow();
        mainWindow.Activate();
    }
}
