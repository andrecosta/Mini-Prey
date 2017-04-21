using System;
using System.Collections.Generic;
using System.Linq;

namespace KokoEngine
{
    public class InputManager : IInputManagerInternal
    {
        // HOOKS FOR MONOGAME
        public Func<string, bool> GetUpdatedKeyState { get; set; }
        public Func<Vector2> GetUpdatedMouseState { get; set; }

        // Action bindings
        private List<InputAction> _actionBindings = new List<InputAction>();
        private List<InputAxis> _axisBindings = new List<InputAxis>();
        private List<Key> _trackedKeys = new List<Key>();
        private Vector2 _mousePosition;

        public void AddActionBinding(string actionName, params string[] keyNames)
        {
            // Convert key names into key objects
            var keys = new List<Key>();
            foreach (var keyName in keyNames)
                keys.Add(new Key(keyName));

            // Create action and add it to the list
            var action = new InputAction(actionName, keys);
            _actionBindings.Add(action);

            // Add keys to be tracked
            _trackedKeys.AddRange(keys);
        }

        public void AddAxisBinding(string axisName, string positiveKey, string negativeKey, float sensitivity = 1, float gravity = 1)
        {
            // Convert key names into key objects
            Key pKey = new Key(positiveKey);
            Key nKey = new Key(negativeKey);

            // Create axis and add it to the list
            var axis = new InputAxis(axisName, pKey, nKey, sensitivity, gravity);
            _axisBindings.Add(axis);

            // Add keys to be tracked
            _trackedKeys.AddRange(new[] {pKey, nKey});
        }

        public Vector2 GetMousePosition()
        {
            return _mousePosition;
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
            InputAxis axis = _axisBindings.Where(a => a.Name == axisName).OrderByDescending(a => a.Timestamp).FirstOrDefault();
            if (axis != null)
                return axis.Value;

            return 0;
        }

        void IInputManagerInternal.Update(float dt)
        {
            // Update keys state
            foreach (Key key in _trackedKeys)
            {
                key.PreviousState = key.CurrentState;
                bool isKeyDown = GetUpdatedKeyState(key.Name);
                key.CurrentState = isKeyDown ? Key.State.Down : Key.State.Up;
            }

            foreach (var axis in _axisBindings)
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

        private List<Key> GetKeysByActionName(string actionName)
        {
            return _actionBindings.Where(a => a.Name == actionName).SelectMany(a => a.Keys).ToList();
        }
    }
}
