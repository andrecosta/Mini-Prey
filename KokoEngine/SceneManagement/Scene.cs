using System.Collections.Generic;

namespace KokoEngine
{
    public class Scene
    {
        public string name { get; set; }
        private List<GameObject> _rootGameObjects;

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
