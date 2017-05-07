using System.Collections.Generic;

namespace KokoEngine
{
    public class DebugManager : IDebugManagerInternal
    {
        public bool IsOpen { get; set; }
        Font IDebugManager.ConsoleFont { get; set; }

        private readonly List<string> _messages = new List<string>();
        private readonly List<Line> _lines = new List<Line>();

        void IDebugManagerInternal.Initialize()
        {
            
        }

        void IDebugManagerInternal.Update()
        {

        }

        void IDebugManagerInternal.Render()
        {
            if (!IsOpen) return;

            // Draw console background
            Engine.Instance.RenderManager.RenderRectangle(
                new Rect(0, Engine.Instance.ScreenManager.Height - 200, Engine.Instance.ScreenManager.Width, 200),
                Color.BlackTransparent, 0.1f);

            // Draw lines
            foreach (Line line in _lines)
                Engine.Instance.RenderManager.RenderLine(line.Start, line.End, line.Color, line.Size);
            _lines.Clear();

            // Draw log messages
            for (int i = 0; i < _messages.Count; i++)
            {
                Font font = (this as IDebugManagerInternal).ConsoleFont;
                string text = _messages[i];
                Vector2 position = new Vector2(10, (Engine.Instance.ScreenManager.Height - 15) - i * 15);
                Color color = Color.White;

                Engine.Instance.RenderManager.RenderText(font, text, position, color, 0, 0, 1, 0);
            }

            // Draw debug hotkeys
            Color c = Color.Green;
            if (Seek.Paused)
                c = Color.Red;
            Engine.Instance.RenderManager.RenderText((this as IDebugManagerInternal).ConsoleFont, "[F1] Toggle Seek", new Vector2(10, 10), c, 0, 0, 1, 0);
            c = Color.Green;
            if (Pursuit.Paused)
                c = Color.Red;
            Engine.Instance.RenderManager.RenderText((this as IDebugManagerInternal).ConsoleFont, "[F2] Toggle Pursuit", new Vector2(120, 10), c, 0, 0, 1, 0);
        }

        public void Log(string message)
        {
            if (!IsOpen) return;

            _messages.Insert(0, message);
            if (_messages.Count > 13)
                _messages.RemoveAt(13);
        }

        public void DrawLine(Vector2 start, Vector2 end, Color color, int size)
        {
            if (!IsOpen) return;

            _lines.Add(new Line(start, end, color, size));
        }

        public void Toggle() => IsOpen = !IsOpen;

        struct Line
        {
            public Vector2 Start;
            public Vector2 End;
            public Color Color;
            public int Size;

            public Line(Vector2 start, Vector2 end, Color color, int size)
            {
                Start = start;
                End = end;
                Color = color;
                Size = size;
            }
        }
    }
}
