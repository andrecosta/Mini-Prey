namespace KokoEngine
{
    public class TimeManager : ITimeManagerInternal
    {
        public float DeltaTime { get; private set; }
        public double TotalTime { get; private set; }
        public float TimeScale { get; set; } = 1;

        void ITimeManagerInternal.Update(float dt)
        {
            DeltaTime = dt * TimeScale;
            TotalTime += DeltaTime;
        }
    }
}
