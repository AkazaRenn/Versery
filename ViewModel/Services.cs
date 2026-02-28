using Microsoft.Extensions.DependencyInjection;

namespace ViewModel;
public static class Services {
    public static void Add(IServiceCollection services) {
        services.AddTransient<Controls.UserProfileButton>();
        services.AddTransient<Pages.Login>();
    }
}
