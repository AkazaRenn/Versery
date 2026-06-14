using LiteDB;
using Microsoft.Extensions.DependencyInjection;
using Model.Api;
using Model.DataPersistence;
using Windows.Storage;

namespace Model;
public static class Services {
    public static void Configure(IServiceCollection services) {
        services.AddSingleton<Client>();
        services.AddSingleton<ApplicationStates>();
        services.AddSingleton<LiteDatabase>(sp => {
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, "data.db");
            return new LiteDatabase(path);
        });
    }
}
