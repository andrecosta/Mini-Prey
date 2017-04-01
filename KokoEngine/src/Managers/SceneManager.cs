using System;
using System.Collections.Generic;
using System.Linq;

namespace KokoEngine
{
    public class SceneManager : ISceneManager
    {
        private Dictionary<string, IScene> _sceneMap = new Dictionary<string, IScene>();
        private IScene _activeScene;

        /// <summary>
        /// Create a new scene and add it to the scene map.
        /// </summary>
        /// <param name="name">The name of the scene to create</param>
        /// <returns></returns>
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

        /// <summary>
        /// Load a scene. This will set it as the active scene and Awake all of its GameObjects.
        /// </summary>
        /// <param name="sceneName">The name of the scene to load</param>
        public void LoadScene(string sceneName)
        {
            // Get the scene reference from the scene map and set it as active
            _sceneMap.TryGetValue(sceneName, out _activeScene);

            // Start the scene scripts
            StartScene();
        }

        /// <summary>
        /// Load a scene. This will set it as the active scene and Awake all of its GameObjects.
        /// </summary>
        /// <param name="sceneIndex">The index of the scene to load</param>
        public void LoadScene(int sceneIndex)
        {
            if (sceneIndex < _sceneMap.Count)
                LoadScene(_sceneMap.ElementAt(sceneIndex).Key);
        }

        /// <summary>
        /// Load a scene. This will set it as the active scene and Awake all of its GameObjects.
        /// </summary>
        /// <param name="scene">The scene to load</param>
        public void LoadScene(IScene scene)
        {
            foreach (var s in _sceneMap)
            {
                if (s.Value == scene)
                    LoadScene(s.Key);
            }
        }

        /// <summary>
        /// Starts all GameObjects contained in this scene.
        /// </summary>
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
                script?.Start();
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
