using System;
using System.Collections.Generic;
using System.Linq;

namespace KokoEngine
{
    public class SceneManager : ISceneManager
    {
        private Dictionary<string, IScene> _sceneMap = new Dictionary<string, IScene>();
        private IScene _activeScene;

        public IScene CreateScene(string name)
        {
            IScene scene = new Scene(name);
            _sceneMap.Add(name, scene);

            return scene;
        }

        public IScene GetActiveScene()
        {
            return _activeScene;
        }

        public void LoadScene(string sceneName)
        {
            // Get the scene reference from the scene map and set it as active
            _sceneMap.TryGetValue(sceneName, out _activeScene);

            // Start the scene scripts
            StartScene();
        }

        public void LoadScene(int sceneIndex)
        {
            if (sceneIndex < _sceneMap.Count)
                LoadScene(_sceneMap.ElementAt(sceneIndex).Key);
        }

        public void LoadScene(IScene scene)
        {
            foreach (var s in _sceneMap)
            {
                if (s.Value == scene)
                    LoadScene(s.Key);
            }
        }
        
        private void StartScene()
        {
            if (_activeScene != null)
                foreach (IGameObject rootGameObject in _activeScene.GetRootGameObjects())
                    StartGameObjects(rootGameObject);
        }

        private void StartGameObjects(IGameObject rootGameObject)
        {
            foreach (IComponent component in rootGameObject.GetComponents())
            {
                IBehaviour script = component as IBehaviour;
                if (script != null)
                {
                    script.Start();
                    script.Enabled = true;
                }
            }

            // Recursively start all children GameObjects
            foreach (var child in rootGameObject.Transform.Children)
                StartGameObjects(child.GameObject);
        }

        public void UpdateActiveScene(float dt)
        {
            foreach(var rootGameObject in _activeScene.GetRootGameObjects())
                UpdateGameObjects(rootGameObject, dt);
        }

        private void UpdateGameObjects(IGameObject rootGameObject, float dt)
        {
            // Call the attached GameObject components' internal Update method
            foreach (IComponent c in rootGameObject.GetComponents())
                ((IComponentInternal) c).Update(dt);

            // Recursively update all children GameObjects
            foreach (ITransform child in rootGameObject.Transform.Children)
                UpdateGameObjects(child.GameObject, dt);
        }
    }
}
