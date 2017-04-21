namespace KokoEngine
{
    /// <summary>
    /// Manager for creating, retrieving and controlling the execution of game scenes.
    /// </summary>
    public interface ISceneManager
    {
        /// <summary>
        /// Creates a new scene and adds it to the scene map.
        /// </summary>
        /// <param name="name">The name of the scene to create.</param>
        /// <returns>The created scene.</returns>
        IScene CreateScene(string name);

        /// <summary>
        /// Returns the currently active scene.
        /// </summary>
        IScene GetActiveScene();

        /// <summary>
        /// Loads a scene. This will set it as the active scene and Awake all of its GameObjects.
        /// </summary>
        /// <param name="sceneName">The name of the scene to load.</param>
        void LoadScene(string sceneName);

        /// <summary>
        /// Loads a scene. This will set it as the active scene and Awake all of its GameObjects.
        /// </summary>
        /// <param name="sceneIndex">The index of the scene to load.</param>
        void LoadScene(int sceneIndex);

        /// <summary>
        /// Loads a scene. This will set it as the active scene and Awake all of its GameObjects.
        /// </summary>
        /// <param name="scene">The scene to load.</param>
        void LoadScene(IScene scene);

        void UpdateActiveScene(float dt);
    }
}