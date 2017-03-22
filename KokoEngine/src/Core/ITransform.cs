using System.Collections.Generic;

namespace KokoEngine
{
    public interface ITransform : IComponent
    {
        Vector3 position { get; set; }
        float rotation { get; set; }
        Vector3 scale { get; set; }
        Vector3 Up { get; }
        ITransform parent { get; set; }
        List<ITransform> Children { get; }
        int childCount { get; }
        void Translate(Vector3 translation);
        void Translate(float x, float y, float z = 0);
        void Translate(Vector2 translation, float z = 0);
        void SetParent(ITransform parent);
        ITransform GetChild(int index);
    }
}