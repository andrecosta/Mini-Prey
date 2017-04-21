using System;
using System.Collections.Generic;

namespace KokoEngine
{
    public interface IInputManager
    {
        // HOOKS FOR MONOGAME
        Func<string, bool> GetUpdatedKeyState { get; set; }
        Func<Vector2> GetUpdatedMouseState { get; set; }

        // Initial setup
        void AddActionBinding(string name, params string[] keys);
        void AddAxisBinding(string name, string positiveKey, string negativeKey, float sensitivity = 1, float gravity = 1);

        // Runtime
        bool GetAction(string actionName);
        bool GetActionDown(string actionName);
        bool GetActionUp(string actionName);
        float GetAxis(string axisName);
        Vector2 GetMousePosition();
    }
}
