using Microsoft.Extensions.DependencyInjection;

namespace View;

public class Services: ViewModel.Services {
    public new static void Configure(IServiceCollection services) {
        ViewModel.Services.Configure(services);
    }
}
