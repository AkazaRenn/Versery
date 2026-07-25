using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;

namespace App;

public sealed partial class App: Application, View.Interfaces.IWindowHelper {
    private readonly HashSet<Window> windows = [];

    public IHost Host { get; private set; }

    public App() {
        InitializeComponent();

        Host = Microsoft.Extensions.Hosting.Host
            .CreateDefaultBuilder()
            .ConfigureServices((context, services) => {
                View.Services.Configure(services);
                ViewModel.Services.Configure(services);
                Model.Services.Configure(services);
            })
            .Build();

        Model.Services.Provider = Host.Services;
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args) {
        var mainWindow = Host.Services.GetRequiredService<View.MainWindow>();
        windows.Add(mainWindow);
        mainWindow.Activate();
    }

    public bool TryGetWindow(UIElement element, out Window? window) {
        foreach (var w in windows) {
            if (w.Content.XamlRoot == element.XamlRoot) {
                window = w;
                return true;
            }
        }
        window = null;
        return false;
    }
}
