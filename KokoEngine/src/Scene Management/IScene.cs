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

        /// <summary>
        /// Adds a GameObject to the scene.
        /// </summary>
        /// <param name="go">The GameObject to add.</param>
        IGameObject AddGameObject(IGameObject go);

        /// <summary>
        /// Returns all the root GameObjects contained in the scene.
        /// </summary>
        /// <returns>List of GameObjects.</returns>
        List<IGameObject> GetRootGameObjects();
    }
}
