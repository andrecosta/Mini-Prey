namespace KokoEngine
{
    public static class Time
    {
        internal static Engine ManagerInstance { private get; set; }

        public static float DeltaTime => ManagerInstance.DeltaTime;
        public static double TotalTime => ManagerInstance.TotalTime;
        public static float TimeScale
        {
            get { return ManagerInstance.TimeScale; }
            set { ManagerInstance.TimeScale = value; }
        }
    }
}
