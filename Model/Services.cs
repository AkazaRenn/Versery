using Microsoft.Extensions.DependencyInjection;
using Model.Api;
using Model.DataPersistence;

namespace Model;
public static class Services {
    public static void Add(IServiceCollection services) {
        services.AddSingleton<Client>();
        services.AddSingleton<ApplicationStates>();
    }
}
