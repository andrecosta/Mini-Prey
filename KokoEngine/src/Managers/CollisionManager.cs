namespace KokoEngine
{
    public class CollisionManager : ICollisionManager
    {
        public void CheckCollisions(IScene scene)
        {
            foreach (IGameObject go in scene.GetRootGameObjects())
            {
                
            }
        }
    }
}
