namespace KokoEngine
{
    public abstract class Behaviour : Component, IBehaviour
    {
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
        void IBehaviour.Awake() => Awake();

        protected virtual void Start() { }
        void IBehaviour.Start() => Start();

        protected virtual void End() { }
        void IBehaviour.End() => End();

        protected virtual void OnEnable() { }
        void IBehaviour.OnEnable() => OnEnable();

        protected virtual void OnDisable() { }
        void IBehaviour.OnDisable() => OnDisable();
    }
}
