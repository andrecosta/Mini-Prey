using System.Collections.Generic;

namespace KokoEngine
{
    public sealed class Scene : IScene
    {
        public string Name { get; }

        private readonly List<IGameObject> _rootGameObjects = new List<IGameObject>();

        public Scene(string name)
        {
            Name = name;
        }

        public void AddGameObject(IGameObject go)
        {
            var gameObjectInternal = go as IGameObjectInternal;
            if (gameObjectInternal == null) return;

            gameObjectInternal.Scene = this;

            _rootGameObjects.Add(go);
        }

        public List<IGameObject> GetRootGameObjects() => _rootGameObjects;
    }
}
