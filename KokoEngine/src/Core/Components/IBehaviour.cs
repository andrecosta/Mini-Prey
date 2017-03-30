namespace KokoEngine
{
    public interface IBehaviour : IComponent
    {
        bool Enabled { get; set; }

        void Awake();
        void Start();
        void End();
        void OnEnable();
        void OnDisable();
    }
}