namespace KokoEngine
{
    /// <summary>
    /// A State that can be used with a <see cref="IFSM"/> (Finite State Machine).
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// The GameObject assigned to this state.
        /// </summary>
        IGameObject Agent { get; set; }

        /// <summary>
        /// The Finite State Machine where this state is loaded.
        /// </summary>
        IFSM FSM { get; set; }

        /// <summary>
        /// Called when this state is entered.
        /// </summary>
        void OnEnterState();

        /// <summary>
        /// Called when this state is exited.
        /// </summary>
        void OnExitState();

        /// <summary>
        /// Called when this state is loaded.
        /// </summary>
        void OnLoad();

        /// <summary>
        /// Called when this state is updated.
        /// </summary>
        void UpdateState();
    }
}