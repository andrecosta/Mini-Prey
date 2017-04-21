using System;

namespace KokoEngine
{
    public class InputAxis
    {
        public string Name { get; }
        public Key PositiveKey { get; }
        public Key NegativeKey { get; }
        public float Gravity { get; }
        public float Sensitivity { get; }
        public float Value { get; set; }
        public double Timestamp { get; set; }

        public InputAxis(string name, Key positiveKey, Key negativeKey, float sensitivity, float gravity)
        {
            Name = name;
            PositiveKey = positiveKey;
            NegativeKey = negativeKey;
            Sensitivity = sensitivity;
            Gravity = gravity;
        }
    }
}
