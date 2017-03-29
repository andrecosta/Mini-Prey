using System.Collections.Generic;

namespace KokoEngine
{
    public interface IScene
    {
        string Name { get; }

        void AddGameObject(IGameObject go);
        List<IGameObject> GetRootGameObjects();
    }
}
