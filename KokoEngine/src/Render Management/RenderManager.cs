using System;
using System.Linq;

namespace KokoEngine
{
    public class RenderManager : IRenderManagerInternal
    {
        public Action<ISpriteRenderer> RenderSpriteHandler { get; set; }
        public Action<Font, string, Vector2, Color, float, float, float, float> RenderTextHandler { get; set; }
        public Action<Vector2, Vector2, Color, int> RenderLineHandler { get; set; }
        public Action<Rect, Color, float> RenderRectangleHandler { get; set; }
        public Action<Vector2, float, Color> RenderCircleHandler { get; set; }
        public Action<Vector2, float, Color> RenderRayHandler { get; set; }

        void IRenderManagerInternal.RenderScene(ISceneInternal scene)
        {
            // Draw the active scene's game objects which contain renderable components
            foreach (IGameObject rootGameObject in scene.GameObjects.Where(go => go.IsActive))
                DrawGameObjects(rootGameObject);
        }

        void IRenderManagerInternal.RenderSprite(ISpriteRenderer sr)
        {
            RenderSpriteHandler?.Invoke(sr);
        }

        void IRenderManagerInternal.RenderText(Font font, string text, Vector2 position, Color color, float alignmentOffset, float rotation, float scale, float layer)
        {
            RenderTextHandler?.Invoke(font, text, position, color, alignmentOffset, rotation, scale, layer);
        }

        void IRenderManagerInternal.RenderLine(Vector2 start, Vector2 end, Color color, int size)
        {
            RenderLineHandler?.Invoke(start, end, color, size);
        }

        void IRenderManagerInternal.RenderRectangle(Rect rectangle, Color color, float layer)
        {
            RenderRectangleHandler?.Invoke(rectangle, color, layer);
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
                if (sr != null)
                    RenderSpriteHandler?.Invoke(sr);

                ITextRenderer tr = component as ITextRenderer;
                if (tr != null)
                    RenderTextHandler?.Invoke(tr.Font, tr.Text, tr.Transform.Position + tr.Offset, tr.Color, 0.5f, tr.Transform.Rotation, tr.Size, 0.5f);

                ILineRenderer lr = component as ILineRenderer;
                if (lr != null)
                    RenderLineHandler?.Invoke(lr.Start, lr.End, lr.Color, lr.Size);
            }

            // Recursive call for all children GameObjects
            foreach (var child in rootGameObject.Transform.Children)
                DrawGameObjects(child.GameObject);
        }
    }
}
