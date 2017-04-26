namespace KokoEngine
{
    public static class Random
    {
        private static System.Random _instance;
        private static System.Random Instance => _instance ?? (_instance = new System.Random());

        public static float value => Instance.Next(0, 100) / 100f;

        public static float Range(float min, float max)
        {
            return Instance.Next((int)min, (int)max);
        }

        public static int Range(int min, int max)
        {
            return Instance.Next(min, max);
        }
    }
}
