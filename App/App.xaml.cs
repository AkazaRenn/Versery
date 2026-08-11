using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using View;

namespace App;

public sealed partial class App: Application, View.Interfaces.IWindowHelper {
    private readonly List<Window> windows = [];

    public IHost Host { get; private set; }

    public App() {
        InitializeComponent();

        Host = Microsoft.Extensions.Hosting.Host
            .CreateDefaultBuilder()
            .ConfigureServices((context, services) => {
                ViewModel.Services.Configure(services);
                Model.Services.Configure(services);
            })
            .Build();

        Model.Services.Provider = Host.Services;
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args) {
        CreateWindow<MainWindow>().Activate();
    }

    public T CreateWindow<T>() where T : Window, new() {
        var window = new T();
        windows.Add(window);
        window.Closed += Window_Closed;
        return window;
    }

    private void Window_Closed(object sender, WindowEventArgs args) {
        windows.RemoveAll(w => w == (Window)sender);
    }

    public bool TryGetWindow(UIElement element, out Window? window) {
        window = windows.FirstOrDefault(w => w.Content.XamlRoot == element.XamlRoot);
        return window != null;
    }
}
