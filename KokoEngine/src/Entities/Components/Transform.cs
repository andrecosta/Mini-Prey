using System;
using System.Collections.Generic;

namespace KokoEngine
{
    public class Transform : Component, ITransform
    {
        public Vector3 Position { get; set; }
        public float Rotation { get; set; }
        public Vector3 Scale { get; set; } = Vector3.One;

        public Vector3 Up
        {
            get
            {
                float c = (float) Math.Cos(Rotation);
                float s = (float) Math.Sin(Rotation);
                return new Vector3(-s, c);
            }
        }
        public void Translate(Vector3 translation) => Position += translation;
        public void Translate(float x, float y, float z = 0) => Translate(new Vector3(x, y, z));
        public void Translate(Vector2 translation, float z = 0) => Translate(translation.X, translation.Y, z);

        public ITransform Parent
        {
            get { return _parent; }
            set
            {
                _parent.Children.Remove(value);
                _parent = value;
                Parent.Children.Add(_parent);
            }
        }
        public List<ITransform> Children { get; } = new List<ITransform>();
        public int ChildCount => Children.Count;
        private ITransform _parent;
        //public void SetParent(ITransform parent) => Parent = parent;
        public ITransform GetChild(int index) => Children[index];
    }
}
