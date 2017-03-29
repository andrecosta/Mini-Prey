using System.Collections.Generic;

namespace KokoEngine
{
    public interface ITransform : IComponent, ITree<ITransform>
    {
        Vector3 Position { get; set; }
        float Rotation { get; set; }
        Vector3 Scale { get; set; }

        Vector3 Up { get; }
        void Translate(Vector3 translation);
        void Translate(Vector2 translation, float z = 0);
        void Translate(float x, float y, float z = 0);
    }
}