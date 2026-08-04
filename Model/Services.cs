using LiteDB;
using Microsoft.Extensions.DependencyInjection;
using Model.DataPersistence;
using Model.Server;
using Refit;
using Windows.Storage;

namespace Model;

public static class Services {
    public static void Configure(IServiceCollection services) {
        services.AddSingleton<HttpClient>();
        services.AddSingleton<Access.Client>();
        services.AddSingleton<ApplicationStates>();
        services.AddSingleton<LiteDatabase>(sp => {
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, "data.db");
            return new LiteDatabase(path);
        });
        services.AddSingleton<RefitSettings>(sp => {
            return new() {
                ContentSerializer = new SystemTextJsonContentSerializer(JsonContext.Default.Options),
                UrlParameterFormatter = new UrlParameterFormatter(),
            };
        });
    }
    public static IServiceProvider Provider { get; set; } = null!;
    public static T Get<T>() where T : notnull {
        return Provider.GetRequiredService<T>();
    }
}
