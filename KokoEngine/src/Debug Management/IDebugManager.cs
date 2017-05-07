namespace KokoEngine
{
    public interface IDebugManager
    {
        bool IsOpen { get; set; }
        Font ConsoleFont { get; set; }

        void Toggle();
        void Log(string message);
        void DrawLine(Vector2 start, Vector2 end, Color color, int size);
    }
}
