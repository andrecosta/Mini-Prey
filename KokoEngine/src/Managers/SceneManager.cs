using System.Collections.Generic;
using System.Linq;

namespace KokoEngine
{
    public static class SceneManager
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

            //foreach (var rootGameObject in _activeScene.GetRootGameObjects())
                //AwakeGameObjects(rootGameObject);
        }

        public static void LoadScene(int sceneIndex)
        {
            if (sceneIndex < SceneMap.Count)
                LoadScene(SceneMap.ElementAt(sceneIndex).Key);
        }

        public static void AwakeScene()
        {
            foreach (var rootGameObject in _activeScene.GetRootGameObjects())
                AwakeGameObjects(rootGameObject);
        }

        static void AwakeGameObjects(GameObject rootGameObject)
        {
            foreach (IComponent c in rootGameObject.GetComponents())
                ((IComponentInternal)c).Awake();

            foreach (var child in rootGameObject.Transform.Children)
                AwakeGameObjects(child.GameObject);
        }

        public static void StartScene()
        {
            foreach (var rootGameObject in _activeScene.GetRootGameObjects())
                StartGameObjects(rootGameObject);
        }

        static void StartGameObjects(GameObject rootGameObject)
        {
            foreach (IComponent c in rootGameObject.GetComponents())
                ((IComponentInternal)c).Start();

            foreach (var child in rootGameObject.Transform.Children)
                StartGameObjects(child.GameObject);
        }


        // TODO: make private and/or decouple!!! -----------------------------------------------------

        public static void UpdateActiveScene(float dt)
        {
            foreach(var rootGameObject in _activeScene.GetRootGameObjects())
                UpdateGameObjects(rootGameObject, dt);
        }

        static void UpdateGameObjects(GameObject rootGameObject, float dt)
        {
            //List<Collider> colliders = new List<Collider>();

            // Call the attached GameObject components' internal Update method
            foreach (Component c in rootGameObject.GetComponents())
            {
                ((IComponentInternal) c).Update(dt);
                //var collider = c as Collider;
                //if (collider != null)
                //    colliders.Add(collider);
            }

            // Recursively update the children GameObjects
            foreach (var child in rootGameObject.Transform.Children)
                UpdateGameObjects(child.GameObject, dt);
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
