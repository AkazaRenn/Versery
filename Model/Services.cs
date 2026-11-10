using Microsoft.Extensions.DependencyInjection;
using Model.DataPersistence;
using Model.Server;
using Model.Server.Entities;
using Refit;

namespace Model;

public class Services {
    public static void Configure(IServiceCollection services) {
        services.AddSingleton<HttpClient>(sp => {
            var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd($"{Constants.AppName}/{Constants.AppVersionString} (+{Constants.ProjectLink})");
            return client;
        });
        services.AddSingleton<Access.Client>();
        services.AddSingleton<ApplicationStates>();
        services.AddSingleton<RefitSettings>(sp => {
            return new() {
                ContentSerializer = new SystemTextJsonContentSerializer(MastodonAPISerializerContext.Default.Options),
            };
        });
    }
    public static IServiceProvider Provider { get; set; } = null!;
    public static T Get<T>() where T : notnull {
        return Provider.GetRequiredService<T>();
    }
}
