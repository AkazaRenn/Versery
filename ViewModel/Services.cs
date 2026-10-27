using Microsoft.Extensions.DependencyInjection;

namespace ViewModel;

public class Services: Model.Services {
    public new static void Configure(IServiceCollection services) {
        Model.Services.Configure(services);
    }
}
