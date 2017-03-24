namespace KokoEngine
{
    interface IFactory<out T>
    {
        T Create();
    }
}
