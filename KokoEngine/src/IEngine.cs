using System;

namespace KokoEngine
{
    public interface IEngine
    {
        void Setup(Action<IScreenManager, IAssetManager, IInputManager, IRenderManager> callback);
        void Initialize();
        void Update(float dt);
        void Render();
    }
}