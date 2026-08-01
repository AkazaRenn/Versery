using Microsoft.UI.Composition;

namespace View.Interfaces;

public interface ICompositionGraphicsDeviceProvider {
    CompositionGraphicsDevice CompositionGraphicsDevice { get; }
}