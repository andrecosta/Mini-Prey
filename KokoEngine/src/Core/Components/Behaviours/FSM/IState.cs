namespace KokoEngine
{
    public interface IState
    {
        IGameObject Agent { get; set; }
        IFSM FSM { get; set; }

        void FixedUpdateState();
        void OnEnterState();
        void OnExitState();
        void OnLoad();
        void UpdateState();
    }
}