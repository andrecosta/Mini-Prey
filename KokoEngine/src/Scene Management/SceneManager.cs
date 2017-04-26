using System;
using System.Collections.Generic;
using System.Linq;

namespace KokoEngine
{
    public class SceneManager : ISceneManagerInternal
    {
        private readonly Dictionary<string, IScene> _sceneMap = new Dictionary<string, IScene>();
        private IScene _activeScene;
        
        // TODO: Build-time!
        public void AddScene(IScene scene)
        {
            _sceneMap.Add(scene.Name, scene);
        }

        public IScene GetActiveScene()
        {
            return _activeScene;
        }

        public void LoadScene(string sceneName)
        {
            // Get the scene reference from the scene map and set it as active
            _sceneMap.TryGetValue(sceneName, out _activeScene);

            // Call the Awake method for every scene component
            AwakeScene();

            // Call the Start method for every scene component
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

        private void AwakeScene()
        {
            if (_activeScene != null)
                foreach (IGameObject rootGameObject in _activeScene.GetRootGameObjects())
                    AwakeGameObjects(rootGameObject);
        }

        private void AwakeGameObjects(IGameObject rootGameObject)
        {
            foreach (IComponent component in rootGameObject.GetComponents())
            {
                (component as IBehaviour)?.Awake();
            }

            // Recursively start all children GameObjects
            foreach (var child in rootGameObject.Transform.Children)
                AwakeGameObjects(child.GameObject);
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

        void ISceneManagerInternal.Initialize()
        {
            // Load the first scene
            LoadScene(0);
        }

        void ISceneManagerInternal.Update()
        {
            foreach(var rootGameObject in _activeScene.GetRootGameObjects())
                UpdateGameObjects(rootGameObject);
        }

        private void UpdateGameObjects(IGameObject rootGameObject)
        {
            // Call the attached GameObject components' internal Update method
            foreach (IComponent c in rootGameObject.GetComponents())
                ((IComponentInternal) c).Update();

            // Recursively update all children GameObjects
            foreach (ITransform child in rootGameObject.Transform.Children)
                UpdateGameObjects(child.GameObject);
        }
    }
}
