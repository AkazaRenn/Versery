using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Composition;
using Microsoft.UI.Composition;

namespace View;
public static class Services {
    public static void Configure(IServiceCollection services) {
        services.AddSingleton<MainWindow>();
        services.AddSingleton<CompositionGraphicsDevice>(sp => {
            var compositor = sp.GetRequiredService<MainWindow>().Compositor;
            var device = CanvasDevice.GetSharedDevice();
            return CanvasComposition.CreateCompositionGraphicsDevice(compositor, device);
        });
    }
}
