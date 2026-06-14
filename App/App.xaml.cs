using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;

namespace App;

public sealed partial class App: Application {
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

        Utilities.Services.Provider = Host.Services;
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args) {
        Host.Services.GetRequiredService<View.MainWindow>().Activate();
    }
}
