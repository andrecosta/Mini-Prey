using System;
using System.Collections.Generic;

namespace KokoEngine
{
    public interface IInputManager
    {
        Vector2 MousePosition { get; set; }
        List<Key> TrackedKeys { get; }

        List<InputAction> ActionBindings { get; }

        void AddActionBinding(string name, params string[] keys);
        void AddAxisBinding(string name, string positiveKey, string negativeKey, float sensitivity = 1, float gravity = 1);

        bool GetAction(string actionName);
        bool GetActionDown(string actionName);
        bool GetActionUp(string actionName);
        float GetAxis(string axisName);

        void Update(float dt);
    }
}
