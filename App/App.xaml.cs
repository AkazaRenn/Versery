using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;

namespace App;

public partial class App: Application {
    public IHost Host { get; private set; }

    public App() {
        InitializeComponent();

        Host = Microsoft.Extensions.Hosting.Host
            .CreateDefaultBuilder()
            .ConfigureServices((context, services) => {
                View.Services.Add(services);
                Model.Services.Add(services);
            })
            .Build();   
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args) {
        Host.Services.GetRequiredService<View.MainWindow>().Activate();
    }
}
