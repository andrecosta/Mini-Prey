namespace KokoEngine
{
    public interface IFSM : IBehaviour
    {
        void SetState<T>();
        void SetGlobalState<T>();
    }
}
