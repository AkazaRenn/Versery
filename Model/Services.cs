using LiteDB;
using Microsoft.Extensions.DependencyInjection;
using Model.Access;
using Model.DataPersistence;
using Windows.Storage;

namespace Model;
public static class Services {
    public static void Configure(IServiceCollection services) {
        services.AddSingleton<HttpClient>();
        services.AddSingleton<Client>();
        services.AddSingleton<ApplicationStates>();
        services.AddSingleton<LiteDatabase>(sp => {
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, "data.db");
            return new LiteDatabase(path);
        });
    }
    public static IServiceProvider Provider { get; set; } = null!;
    public static T Get<T>() where T : notnull {
        return Provider.GetRequiredService<T>();
    }
}
