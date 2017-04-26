namespace KokoEngine
{
    internal interface IRenderManagerInternal : IRenderManager
    {
        void RenderScene(IScene scene);
        void RenderSprite(ISpriteRenderer sr);
        void RenderLine(Vector2 start, Vector2 end, Color color);
        void RenderRectangle(Rect rectangle, Color color);
        void RenderCircle(Vector2 position, float radius, Color color);
        void RenderRay(Vector2 position, float distance, Color color);
    }
}
