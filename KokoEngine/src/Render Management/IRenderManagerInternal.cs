namespace KokoEngine
{
    internal interface IRenderManagerInternal : IRenderManager
    {
        void RenderScene(ISceneInternal scene);
        void RenderSprite(ISpriteRenderer sr);
        void RenderText(Font font, string text, Vector2 position, Color color, float alignmentOffset, float rotation, float scale, float layer);
        void RenderLine(ILineRenderer lr);
        void RenderRectangle(Rect rectangle, Color color, float layer);
        void RenderCircle(Vector2 position, float radius, Color color);
        void RenderRay(Vector2 position, float distance, Color color);
    }
}
