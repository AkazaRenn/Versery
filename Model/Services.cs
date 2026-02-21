using Microsoft.Extensions.DependencyInjection;

namespace Model;
public static class Services {
    public static void Add(IServiceCollection services) {
        services.AddSingleton<Client>();
    }
}
