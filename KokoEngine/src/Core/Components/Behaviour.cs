namespace KokoEngine
{
    public abstract class Behaviour : Component, IBehaviour
    {
        public bool IsAwake { get; private set; }
        public bool IsStarted { get; private set; }

        private bool _enabled;
        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
                if (_enabled)
                    OnEnable();
                else
                    OnDisable();
            }
        }

        protected virtual void Awake() { }
        void IBehaviour.Awake()
        {
            Awake();
            IsAwake = true;
        }

        protected virtual void Start() { }
        void IBehaviour.Start()
        {
            Start();
            IsStarted = true;
        }

        protected virtual void End() { }
        void IBehaviour.End() => End();

        protected virtual void OnEnable() { }
        void IBehaviour.OnEnable() => OnEnable();

        protected virtual void OnDisable() { }
        void IBehaviour.OnDisable() => OnDisable();

        protected T Instantiate<T>(string name, Vector2 position) where T : IComponent, new()
        {
            return (GameObject as IGameObjectInternal).Scene.CreateGameObject<T>(name, position);
        }

        protected void Destroy(IGameObject go)
        {
            (go as IGameObjectInternal)?.Scene.DestroyGameObject(go);
        }
            
    }
}
