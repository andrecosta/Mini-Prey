using System;

namespace KokoEngine
{
    public interface IRenderManager
    {
        Action<ISpriteRenderer> RenderSpriteHandler { get; set; }
        Action<Font, string, Vector2, Color, float, float, float, float> RenderTextHandler { get; set; }
        Action<Vector2, Vector2, Color, int> RenderLineHandler { get; set; }
        Action<Rect, Color, float> RenderRectangleHandler { get; set; }
        Action<Vector2, float, Color> RenderCircleHandler { get; set; }
        Action<Vector2, float, Color> RenderRayHandler { get; set; }
    }
}
