using Microsoft.Extensions.DependencyInjection;

namespace Utilities;
public static class Services {
    public static IServiceProvider Provider { get; set; } = null!;
    public static T Get<T>() where T: notnull {
        return Provider.GetRequiredService<T>();
    }
}
