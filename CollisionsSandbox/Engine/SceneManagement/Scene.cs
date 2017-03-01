using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPrey.Engine.SceneManagement
{
    class Scene
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
