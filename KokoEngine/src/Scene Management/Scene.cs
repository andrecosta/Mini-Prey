using System.Collections.Generic;

namespace KokoEngine
{
    public class Scene : IScene
    {
        public string Name { get; }

        private readonly List<IGameObject> _rootGameObjects = new List<IGameObject>();

        public Scene(string name)
        {
            Name = name;
        }

        public IGameObject CreateGameObject(string name)
        {
            GameObject go = new GameObject(name, this);
            _rootGameObjects.Add(go);

            return go;
        }

        public List<IGameObject> GetRootGameObjects() => _rootGameObjects;
    }
}
