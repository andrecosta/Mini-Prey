namespace KokoEngine
{
    public interface IFSM
    {
        void SetState<T>();
        void SetGlobalState<T>();
    }
}
