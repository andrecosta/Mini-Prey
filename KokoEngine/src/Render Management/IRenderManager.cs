using System;

namespace KokoEngine
{
    public interface IRenderManager
    {
        Action<ISpriteRenderer> RenderSpriteHandler { get; set; }
        Action<Vector2, Vector2, Color> RenderLineHandler { get; set; }
        Action<Rect, Color> RenderRectangleHandler { get; set; }
        Action<Vector2, float, Color> RenderCircleHandler { get; set; }
        Action<Vector2, float, Color> RenderRayHandler { get; set; }
    }
}
