namespace KokoEngine
{
    public static class Input
    {
        internal static Engine ManagerInstance { private get; set; }

        public static bool GetAction(string actionName) => ManagerInstance.InputManager.GetAction(actionName);
        public static bool GetActionDown(string actionName) => ManagerInstance.InputManager.GetActionDown(actionName);
        public static bool GetActionUp(string actionName) => ManagerInstance.InputManager.GetActionUp(actionName);
        public static float GetAxis(string axisName) => ManagerInstance.InputManager.GetAxis(axisName);
    }
}
