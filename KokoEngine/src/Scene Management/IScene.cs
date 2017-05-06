using System.Collections.Generic;

namespace KokoEngine
{
    /// <summary>
    /// A scene represents a game level. Only one scene can be active at a time.
    /// </summary>
    public interface IScene
    {
        /// <summary>
        /// The name of the scene.
        /// </summary>
        string Name { get; }

        IGameObject CreateGameObject(string name);

        /// <summary>
        /// Returns all the root GameObjects contained in the scene.
        /// </summary>
        /// <returns>List of GameObjects.</returns>
        List<IGameObject> GetRootGameObjects();

        List<IGameObject> GetPendingGameObjects();
    }
}
