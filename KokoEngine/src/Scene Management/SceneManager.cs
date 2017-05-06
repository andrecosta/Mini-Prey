using System;
using System.Collections.Generic;
using System.Linq;

namespace KokoEngine
{
    public class SceneManager : ISceneManagerInternal
    {
        private readonly Dictionary<string, ISceneInternal> _sceneMap = new Dictionary<string, ISceneInternal>();
        private ISceneInternal _activeScene;
        
        // TODO: Build-time!
        public void AddScene(IScene scene)
        {
            _sceneMap.Add(scene.Name, scene as ISceneInternal);
        }

        public IScene GetActiveScene()
        {
            return _activeScene;
        }

        public void LoadScene(string sceneName)
        {
            // Get the scene reference from the scene map and set it as active
            _sceneMap.TryGetValue(sceneName, out _activeScene);
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

        void ISceneManagerInternal.Initialize()
        {
            // Load the first scene
            LoadScene(0);
        }

        void ISceneManagerInternal.Update()
        {
            // Check if there are any GameObjects waiting to be created
            CheckGameObjectsPendingCreation();
            
            // Check if there are any GameObjects waiting to be destroyed
            CheckGameObjectsPendingDestruction();

            // Start and update all GameObjects
            foreach (var go in _activeScene.GameObjects.Where(go => go.IsActive))
            {
                StartGameObject(go);

                // Call the attached GameObject components' internal Update method
                foreach (IComponent c in go.GetComponents())
                    ((IComponentInternal)c).Update();
            }
        }

        private void CheckGameObjectsPendingCreation()
        {
            if (_activeScene.GameObjectsPendingCreation.Count > 0)
            {
                // Add the GameObject to the scene
                foreach (var go in _activeScene.GameObjectsPendingCreation)
                    _activeScene.GameObjects.Add(go);

                _activeScene.GameObjectsPendingCreation.Clear();

                // Awake GameObjects's components
                foreach (var go in _activeScene.GameObjects)
                    AwakeGameObject(go);
            }
        }

        private void CheckGameObjectsPendingDestruction()
        {
            if (_activeScene.GameObjectsPendingDestruction.Count > 0)
            {
                // Disable the GameObject's components
                foreach (var go in _activeScene.GameObjectsPendingDestruction)
                    DisableGameObject(go);

                // Remove the GameObject from the scene
                foreach (var go in _activeScene.GameObjectsPendingDestruction)
                    _activeScene.GameObjects.Remove(go);

                _activeScene.GameObjectsPendingDestruction.Clear();
            }
        }

        private void AwakeGameObject(IGameObject go)
        {
            foreach (IComponent component in go.GetComponents())
            {
                var behaviour = component as IBehaviour;
                if (behaviour != null && !behaviour.IsAwake)
                    behaviour.Awake();
            }
        }

        private void StartGameObject(IGameObject go)
        {
            foreach (IComponent component in go.GetComponents())
            {
                IBehaviour behaviour = component as IBehaviour;
                if (behaviour != null && !behaviour.IsStarted && !behaviour.Enabled)
                {
                    behaviour.Start();
                    behaviour.Enabled = true;
                }
            }
        }

        private void DisableGameObject(IGameObject go)
        {
            foreach (IComponent component in go.GetComponents())
            {
                IBehaviour behaviour = component as IBehaviour;
                if (behaviour != null && behaviour.IsStarted && behaviour.Enabled)
                    behaviour.Enabled = false;
            }
        }
    }
}
