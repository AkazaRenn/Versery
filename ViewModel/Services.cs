using Microsoft.Extensions.DependencyInjection;

namespace ViewModel;
public static class Services {
    public static void Configure(IServiceCollection services) {
        services.AddTransient<Controls.UserProfileButton>();
        services.AddTransient<Pages.Home>();
        services.AddTransient<Pages.SignIn>();
    }
}
