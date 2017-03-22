using System;
using System.Collections.Generic;

namespace KokoEngine
{
    public class Transform : Component, ITransform
    {
        public Vector3 position { get; set; }
        public float rotation { get; set; }
        public Vector3 scale { get; set; }

        public Vector3 Up
        {
            get
            {
                float c = (float) Math.Cos(rotation);
                float s = (float) Math.Sin(rotation);
                return new Vector3(-s, c);
            }
        }

        public ITransform parent
        {
            get { return _parent; }
            set
            {
                _parent.Children.Remove(value);
                _parent = value;
                parent.Children.Add(_parent);
            }
        }
        public List<ITransform> Children { get; }
        public int childCount => Children.Count;

        private ITransform _parent;

        public Transform()
        {
            Children = new List<ITransform>();
            scale = Vector3.One;
        }

        public void Translate(Vector3 translation)
        {
            position += translation;
        }

        public void Translate(float x, float y, float z = 0)
        {
            Translate(new Vector3(x, y, z));
        }

        public void Translate(Vector2 translation, float z = 0)
        {
            Translate(translation.X, translation.Y, z);
        }

        public void SetParent(ITransform parent)
        {
            this.parent = parent;
        }

        public ITransform GetChild(int index)
        {
            return Children[index];
        }
    }
}
