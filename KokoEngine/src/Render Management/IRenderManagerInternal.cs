namespace KokoEngine
{
    internal interface IRenderManagerInternal : IRenderManager
    {
        void RenderScene(IScene scene);
        void RenderSprite(ISpriteRenderer sr);
        void RenderText(ITextRenderer tr);
        void RenderLine(ILineRenderer lr);
        void RenderRectangle(Rect rectangle, Color color);
        void RenderCircle(Vector2 position, float radius, Color color);
        void RenderRay(Vector2 position, float distance, Color color);
    }
}
