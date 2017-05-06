using System.Collections.Generic;

namespace KokoEngine
{
    /// <summary>
    /// A scene represents a game level. Only one scene can be active at a time.
    /// </summary>
    internal interface ISceneInternal : IScene
    {
        List<IGameObject> GameObjects { get; }
        List<IGameObject> GameObjectsPendingCreation { get; }
        List<IGameObject> GameObjectsPendingDestruction { get; }

        /// <summary>
        /// Returns all the GameObjects contained in the scene.
        /// </summary>
        /// <returns>List of GameObjects.</returns>

        void DestroyGameObject(IGameObject go);
    }
}
