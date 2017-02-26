using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPrey.Engine.SceneManagement
{
    struct Scene
    {
        public string name { get; set; }
        List<GameObject> RootGameObjects;

        public void AddRootGameObject(GameObject go)
        {
            if (RootGameObjects == null)
                RootGameObjects = new List<GameObject>();

            RootGameObjects.Add(go);
        }

        public List<GameObject> GetRootGameObjects()
        {
            if (RootGameObjects == null)
                RootGameObjects = new List<GameObject>();

            return RootGameObjects;
        }
    }
}
