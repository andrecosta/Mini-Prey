namespace KokoEngine
{
    public interface ISceneManager
    {
        //Action<float> UpdateHook();
        //void RegisterUpdateable(IGameObject go);

        void OnGameObjectCreated(IGameObject go);

        /// <summary>
        /// Create a new scene and add it to the scene map.
        /// </summary>
        /// <param name="name">The name of the scene to create</param>
        /// <returns></returns>
        IScene CreateScene(string name);

        /// <summary>
        /// Get the currently active scene.
        /// </summary>
        IScene GetActiveScene();

        /// <summary>
        /// Load a scene. This will set it as the active scene and Awake all of its GameObjects.
        /// </summary>
        /// <param name="sceneName">The name of the scene to load</param>
        void LoadScene(string sceneName);

        /// <summary>
        /// Load a scene. This will set it as the active scene and Awake all of its GameObjects.
        /// </summary>
        /// <param name="sceneIndex">The index of the scene to load</param>
        void LoadScene(int sceneIndex);

        /// <summary>
        /// Load a scene. This will set it as the active scene and Awake all of its GameObjects.
        /// </summary>
        /// <param name="scene">The scene to load</param>
        void LoadScene(IScene scene);

        /// <summary>
        /// Starts all GameObjects contained in this scene.
        /// </summary>
        void StartScene();

        /// <summary>
        /// Update the currently active scene.
        /// </summary>
        /// <param name="dt">Time delta</param>
        void UpdateActiveScene(float dt);
    }
}