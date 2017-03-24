using System.Collections.Generic;

namespace KokoEngine
{
    public interface IScene
    {
        string Name { get; }

        void AddRootGameObject(IGameObject go);
        List<IGameObject> GetRootGameObjects();
    }
}
