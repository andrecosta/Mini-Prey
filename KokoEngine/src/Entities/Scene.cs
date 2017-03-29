using System.Collections.Generic;

namespace KokoEngine
{
    public sealed class Scene : IScene
    {
        public string Name { get; }

        private List<IGameObject> _rootGameObjects;

        public Scene(string name)
        {
            _rootGameObjects = new List<IGameObject>();
            Name = name;
        }

        public void AddGameObject(IGameObject go)
        {
            if (_rootGameObjects == null)
                _rootGameObjects = new List<IGameObject>();

            var gameObjectInternal = go as IGameObjectInternal;
            if (gameObjectInternal != null) gameObjectInternal.Scene = this;

            _rootGameObjects.Add(go);
        }

        public List<IGameObject> GetRootGameObjects()
        {
            if (_rootGameObjects == null)
                _rootGameObjects = new List<IGameObject>();

            return _rootGameObjects;
        }
    }
}
