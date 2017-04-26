namespace KokoEngine
{
    public interface ITimeManager
    {
        float DeltaTime { get; }
        double TotalTime { get; }
        float TimeScale { get; set; }
    }
}