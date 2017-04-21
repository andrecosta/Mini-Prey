using System;
using System.Collections.Generic;
using System.Linq;

namespace KokoEngine
{
    public class InputManager : IInputManager
    {
        // -- USABLE BY MONOGAME
        public Vector2 MousePosition { get; set; }

        // Property to track key states -- USABLE BY MONOGAME
        public List<Key> TrackedKeys { get; } = new List<Key>();

        // -- PUBLIC
        // Action bindings
        public List<InputAction> ActionBindings { get; } = new List<InputAction>();
        public List<InputAxis> AxisBindings { get; } = new List<InputAxis>();

        public void AddActionBinding(string actionName, params string[] keyNames)
        {
            // Convert key names into key objects
            var keys = new List<Key>();
            foreach (var keyName in keyNames)
                keys.Add(new Key(keyName));

            // Create action and add it to the list
            var action = new InputAction(actionName, keys);
            ActionBindings.Add(action);

            // Add keys to be tracked
            TrackedKeys.AddRange(keys);
        }

        public void AddAxisBinding(string axisName, string positiveKey, string negativeKey, float sensitivity = 1, float gravity = 1)
        {
            // Convert key names into key objects
            Key pKey = new Key(positiveKey);
            Key nKey = new Key(negativeKey);

            // Create axis and add it to the list
            var axis = new InputAxis(axisName, pKey, nKey, sensitivity, gravity);
            AxisBindings.Add(axis);

            // Add keys to be tracked
            TrackedKeys.AddRange(new[] {pKey, nKey});
        }

        public Vector2 GetMousePosition()
        {
            return MousePosition;
        }

        public bool GetAction(string actionName)
        {
            List<Key> actionKeys = GetKeysByActionName(actionName);
            return actionKeys != null &&
                   actionKeys.Any(key => key.CurrentState == Key.State.Down);
        }

        public bool GetActionDown(string actionName)
        {
            List<Key> actionKeys = GetKeysByActionName(actionName);
            return actionKeys != null &&
                   actionKeys.Any(key => key.CurrentState == Key.State.Down && key.PreviousState != Key.State.Down);
        }

        public bool GetActionUp(string actionName)
        {
            List<Key> actionKeys = GetKeysByActionName(actionName);
            return actionKeys != null &&
                   actionKeys.Any(key => key.CurrentState == Key.State.Up && key.PreviousState != Key.State.Up);
        }

        public float GetAxis(string axisName)
        {
            InputAxis axis = AxisBindings.Where(a => a.Name == axisName).OrderByDescending(a => a.Timestamp).FirstOrDefault();
            if (axis != null)
                return axis.Value;

            return 0;
        }

        private List<Key> GetKeysByActionName(string actionName)
        {
            return ActionBindings.Where(a => a.Name == actionName).SelectMany(a => a.Keys).ToList();
        }

        public void Update(float dt)
        {
            foreach (var axis in AxisBindings)
            {
                // Axis values go from -1 (min) to 1 (max)

                if (axis.PositiveKey.CurrentState == Key.State.Down)
                {
                    // If positive key is pressed, increase axis value (modified by sensitivity) until it reaches 1
                    axis.Value = Math.Min(axis.Value + dt * axis.Sensitivity, 1);
                    axis.Timestamp = Time.TotalTime;
                }
                else if (axis.Value > 0)
                    // If positive key is not pressed, progressively return axis value to 0 (modified by gravity)
                    axis.Value = Math.Max(axis.Value - dt * axis.Gravity, 0);

                if (axis.NegativeKey.CurrentState == Key.State.Down)
                {
                    // If negative key is pressed, decrease axis value (modified by sensitivity) until it reaches -1
                    axis.Value = Math.Max(axis.Value - dt * axis.Sensitivity, -1);
                    axis.Timestamp = Time.TotalTime;
                }
                else if (axis.Value < 0)
                    // If negative key is not pressed, progressively return axis value to 0 (modified by gravity)
                    axis.Value = Math.Min(axis.Value + dt * axis.Gravity, 0);
            }
        }
    }
}
