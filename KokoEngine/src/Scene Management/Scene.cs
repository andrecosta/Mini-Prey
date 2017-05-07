using System.Collections.Generic;

namespace KokoEngine
{
    public class Scene : ISceneInternal
    {
        public string Name { get; }

        List<IGameObject> ISceneInternal.GameObjects { get; } = new List<IGameObject>();
        List<IGameObject> ISceneInternal.GameObjectsPendingCreation { get; } = new List<IGameObject>();
        List<IGameObject> ISceneInternal.GameObjectsPendingDestruction { get; } = new List<IGameObject>();

        public Scene(string name)
        {
            Name = name;
        }

        public IGameObject CreateGameObject(string name)
        {
            GameObject go = new GameObject(name, this);
            (this as ISceneInternal).GameObjectsPendingCreation.Add(go);

            return go;
        }

        public T CreateGameObject<T>(string name, Vector2 position) where T : IComponent, new()
        {
            IGameObject go = CreateGameObject(name);
            go.Transform.Position = position;

            return go.AddComponent<T>();
        }

        void ISceneInternal.DestroyGameObject(IGameObject go)
        {
            // Remove the GameObject from the scene
            (this as ISceneInternal).GameObjectsPendingDestruction.Add(go);
        }

        T ISceneInternal.FindObjectOfType<T>()
        {
            foreach (var go in (this as ISceneInternal).GameObjects)
            {
                var component = go.GetComponent<T>();
                if (component != null)
                    return component;
            }

            return default(T);
        }

        List<T> ISceneInternal.FindObjectsOfType<T>()
        {
            List<T> components = new List<T>();

            foreach (var go in (this as ISceneInternal).GameObjects)
            {
                var component = go.GetComponent<T>();
                if (component != null)
                    components.Add(component);
            }

            return components;
        }
    }
}
