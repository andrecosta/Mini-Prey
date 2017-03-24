namespace KokoEngine
{
    class SceneFactory : IFactory<IScene>
    {
        public IScene Create()
        {
            return new Scene();
        }
    }
}
