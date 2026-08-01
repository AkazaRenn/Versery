using Microsoft.Extensions.DependencyInjection;

namespace View;

public static class Services {
    public static void Configure(IServiceCollection services) {
        services.AddSingleton<MainWindow>();
    }
}
