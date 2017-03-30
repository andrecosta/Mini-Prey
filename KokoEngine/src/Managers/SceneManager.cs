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

            // Get the scene's root GameObjects
            //var rootGameObjects = _activeScene?.GetRootGameObjects();
            //if (rootGameObjects == null) return; // TODO: throw exception

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

        /*private void AwakeScene()
        {
            // Awake every root GameObject and their children
            foreach (IGameObject rootGameObject in _activeScene.GetRootGameObjects())
                AwakeGameObjects(rootGameObject);
        }

        private void AwakeGameObjects(IGameObject rootGameObject)
        {
            foreach (IComponent c in rootGameObject.GetComponents())
                ((IComponentInternal)c).Awake();

            foreach (var child in rootGameObject.Transform.Children)
                AwakeGameObjects(child.GameObject);
        }*/

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

            // Recursively call this method for all children GameObjects
            foreach (var child in rootGameObject.Transform.Children)
                StartGameObjects(child.GameObject);
        }

        // TODO: make private and/or decouple!!! -----------------------------------------------------

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

            // Recursively update the children GameObjects
            foreach (ITransform child in rootGameObject.Transform.Children)
                UpdateGameObjects(child.GameObject, dt);
        }

        public void OnGameObjectCreated(IGameObject go)
        {
            _activeScene.AddGameObject(go);
        }

        /*
public static void DrawActiveScene(SpriteBatch sb, Texture2D dummyTexture)
{
   foreach (var rootGameObject in _activeScene.GetRootGameObjects())
       DrawGameObjects(rootGameObject, sb, dummyTexture);
}

static void DrawGameObjects(GameObject rootGameObject, SpriteBatch sb, Texture2D dummyTexture)
{
   foreach (Component component in rootGameObject.GetComponents())
   {
       SpriteRenderer sr = component as SpriteRenderer;
       if (sr == null)
           continue;

       sb.Draw(dummyTexture, new Rectangle((int)sr.Transform.position.X, (int)sr.Transform.position.Y,
           (int)(50 * sr.Transform.scale.X), (int)(50 * sr.Transform.scale.Y)), sr.color);
   }

   foreach (var child in rootGameObject.Transform.Children)
       DrawGameObjects(child.GameObject, sb, dummyTexture);
}*/
    }
}
