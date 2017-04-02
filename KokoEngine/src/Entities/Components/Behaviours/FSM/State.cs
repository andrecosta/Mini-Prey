namespace KokoEngine
{
    public abstract class State
    {
        public IGameObject Agent;
        public IFSM FSM;

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
