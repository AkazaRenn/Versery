using Microsoft.Graphics.Canvas;
using Microsoft.UI.Composition;

namespace View.Interfaces;

public interface IGraphicsDeviceProvider {
    CanvasDevice CanvasDevice { get; }
    CompositionGraphicsDevice CompositionGraphicsDevice { get; }
}