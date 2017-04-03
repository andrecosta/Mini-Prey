namespace KokoEngine
{
    public abstract class State : IState
    {
        public IGameObject Agent { get; set; }
        public IFSM FSM { get; set; }

        public virtual void OnLoad()
        {
        }

        public virtual void UpdateState()
        {
        }

        public virtual void FixedUpdateState()
        {
        }

        public virtual void OnEnterState()
        {
        }

        public virtual void OnExitState()
        {
        }
    }
}
