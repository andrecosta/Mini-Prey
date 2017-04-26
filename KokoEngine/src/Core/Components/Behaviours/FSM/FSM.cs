using System;
using System.Collections.Generic;

namespace KokoEngine
{
    public class FSM : Behaviour, IFSM
    {
        private IState _activeState;
        private IState _globalState;
        private Dictionary<Type, IState> _states;

        protected override void Awake()
        {
            _states = new Dictionary<Type, IState>();
        }

        protected override void Update()
        {
            if (_globalState != null)
                _globalState.UpdateState();

            if (_activeState != null)
                _activeState.UpdateState();
        }

        public T LoadState<T>() where T : IState, new()
        {
            Type type = typeof(T);
            T state = new T();
            _states.Add(type, state);

            // Set agent of state
            state.Agent = GameObject;

            // Set the reference for the FiniteStateMachine
            state.FSM = this;

            state.OnLoad();

            return state;
        }

        public void SetState<T>()
        {
            Type type = typeof(T);
            IState state = _states[type];

            // If the state is already active, do nothing
            if (state == _activeState)
                return;

            // If there is an active state, change it
            if (_activeState != null)
            {
                // Exit from current active state
                _activeState.OnExitState();
            }

            // Set new state
            _activeState = state;

            // Enter new state
            _activeState.OnEnterState();
        }

        public void SetGlobalState<T>()
        {
            Type type = typeof(T);
            IState state = _states[type];

            // If the state is already active, do nothing
            if (state == _globalState)
                return;

            // If there is an active state, change it
            if (_globalState != null)
            {
                // Exit from current active state
                _globalState.OnExitState();
            }

            // Set new state
            _globalState = state;

            // Enter new state
            _globalState.OnEnterState();
        }
    }
}
