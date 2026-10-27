using Microsoft.Extensions.DependencyInjection;
using View.Utilities;

namespace View;

public class Services: ViewModel.Services {
    public new static void Configure(IServiceCollection services) {
        services.AddSingleton<DialogService>();
        ViewModel.Services.Configure(services);
    }
}
