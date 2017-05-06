namespace KokoEngine
{
    /// <summary>
    /// Base class for all user created scripts. A Behaviour is a Component that can be enabled or disabled, and defines overridable methods
    /// used for game logic.
    /// </summary>
    public interface IBehaviour : IComponent
    {
        bool IsAwake { get; }
        bool IsStarted { get; }

        /// <summary>
        /// The state of this behaviour.
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// Called as soon as the behaviour is created.
        /// </summary>
        void Awake();

        /// <summary>
        /// Called after all behaviours are awoken.
        /// </summary>
        void Start();

        /// <summary>
        /// Called at the end of life of the behaviour.
        /// </summary>
        void End();

        /// <summary>
        /// Called whenever the behaviour is enabled.
        /// </summary>
        void OnEnable();

        /// <summary>
        /// Called whenever the behaviour is disabled.
        /// </summary>
        void OnDisable();
    }
}