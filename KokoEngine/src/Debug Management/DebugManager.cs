using System.Collections.Generic;

namespace KokoEngine
{
    public class DebugManager : IDebugManagerInternal
    {
        public bool IsOpen { get; set; }
        Font IDebugManager.ConsoleFont { get; set; }

        private readonly List<string> _messages = new List<string>();

        void IDebugManagerInternal.Initialize()
        {
            
        }

        void IDebugManagerInternal.Update()
        {
            if (!IsOpen) return;

        }

        void IDebugManagerInternal.Render()
        {
            if (!IsOpen) return;

            // Draw console background
            Engine.Instance.RenderManager.RenderRectangle(
                new Rect(0, Engine.Instance.ScreenManager.Height - 200, Engine.Instance.ScreenManager.Width, 200),
                Color.BlackTransparent, 0.1f);

            // Draw lines


            // Draw log messages
            for (int i = 0; i < _messages.Count; i++)
            {
                Font font = (this as IDebugManagerInternal).ConsoleFont;
                string text = _messages[i];
                Vector2 position = new Vector2(10, (Engine.Instance.ScreenManager.Height - 15) - i * 15);
                Color color = Color.White;

                Engine.Instance.RenderManager.RenderText(font, text, position, color, 0, 0, 1, 0);
            }
        }

        public void Log(string message)
        {
            _messages.Insert(0, message);
            if (_messages.Count > 13)
                _messages.RemoveAt(13);
        }

        public void DrawLine(Vector2 start, Vector2 end, Color color, int size)
        {
            throw new System.NotImplementedException();
        }

        public void Toggle() => IsOpen = !IsOpen;
    }
}
