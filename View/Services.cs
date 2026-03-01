using Microsoft.Extensions.DependencyInjection;

namespace View;
public static class Services {
    public static void Add(IServiceCollection services) {
        services.AddSingleton<MainWindow>();
    }

    public static IServiceProvider? Provider { get; set; }
}
