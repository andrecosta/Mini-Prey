using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPrey.Engine.SceneManagement
{
    static class SceneManager
    {
        public static Dictionary<string, Scene> SceneMap = new Dictionary<string, Scene>();
        private static Scene _activeScene;

        public static Scene GetActiveScene()
        {
            return _activeScene;
        }

        public static void LoadScene(string sceneName)
        {
            SceneMap.TryGetValue(sceneName, out _activeScene);

            foreach (var rootGameObject in _activeScene.GetRootGameObjects())
                StartGameObjects(rootGameObject);
        }

        public static void LoadScene(int sceneIndex)
        {
            if (sceneIndex < SceneMap.Count)
                LoadScene(SceneMap.ElementAt(sceneIndex).Key);
        }

        static void StartGameObjects(GameObject rootGameObject)
        {
            foreach (Component c in rootGameObject.GetComponents())
                c.Start();

            foreach (var child in rootGameObject.transform.Children)
                StartGameObjects(child.gameObject);
        }
    }
}
