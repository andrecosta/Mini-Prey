namespace KokoEngine
{
    /// <summary>
    /// A Finite State Machine to control user-created states on a GameObject.
    /// </summary>
    public interface IFSM : IBehaviour
    {
        /// <summary>
        /// Sets the current state to the new state of type T.
        /// </summary>
        /// <typeparam name="T">The type of the state.</typeparam>
        void SetState<T>();

        /// <summary>
        /// Sets the current global state to the new state of type T.
        /// </summary>
        /// <typeparam name="T">The type of the state.</typeparam>
        void SetGlobalState<T>();
    }
}
