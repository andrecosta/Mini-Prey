using System;

namespace KokoEngine
{
    public class RenderManager : IRenderManagerInternal
    {
        public Action<ISpriteRenderer> RenderSpriteHandler { get; set; }
        public Action<Vector2, Vector2, Color> RenderLineHandler { get; set; }
        public Action<Rect, Color> RenderRectangleHandler { get; set; }
        public Action<Vector2, float, Color> RenderCircleHandler { get; set; }
        public Action<Vector2, float, Color> RenderRayHandler { get; set; }

        void IRenderManagerInternal.RenderScene(IScene scene)
        {
            // Draw the active scene's game objects which contain renderable components
            foreach (IGameObject rootGameObject in scene.GetRootGameObjects())
                DrawGameObjects(rootGameObject);
        }

        void IRenderManagerInternal.RenderSprite(ISpriteRenderer sr)
        {
            RenderSpriteHandler?.Invoke(sr);
        }

        void IRenderManagerInternal.RenderLine(Vector2 start, Vector2 end, Color color)
        {
            RenderLineHandler?.Invoke(start, end, color);
        }

        void IRenderManagerInternal.RenderRectangle(Rect rectangle, Color color)
        {
            RenderRectangleHandler?.Invoke(rectangle, color);
        }

        void IRenderManagerInternal.RenderCircle(Vector2 position, float radius, Color color)
        {
            RenderCircleHandler?.Invoke(position, radius, color);
        }

        void IRenderManagerInternal.RenderRay(Vector2 position, float distance, Color color)
        {
            RenderRayHandler?.Invoke(position, distance, color);
        }

        private void DrawGameObjects(IGameObject rootGameObject)
        {
            foreach (IComponent component in rootGameObject.GetComponents())
            {
                ISpriteRenderer sr = component as ISpriteRenderer;
                if (sr == null) continue;

                RenderSpriteHandler?.Invoke(sr);
            }

            // Recursive call for all children GameObjects
            foreach (var child in rootGameObject.Transform.Children)
                DrawGameObjects(child.GameObject);
        }
    }
}
