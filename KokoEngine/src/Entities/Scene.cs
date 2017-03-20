using System.Collections.Generic;

namespace KokoEngine
{
    public sealed class Scene : Entity
    {
        private List<GameObject> _rootGameObjects;

        public Scene(string name)
        {
            Name = name;
        }

        public void AddRootGameObject(GameObject go)
        {
            if (_rootGameObjects == null)
                _rootGameObjects = new List<GameObject>();

            _rootGameObjects.Add(go);
        }

        public List<GameObject> GetRootGameObjects()
        {
            if (_rootGameObjects == null)
                _rootGameObjects = new List<GameObject>();

            return _rootGameObjects;
        }

    }
}
