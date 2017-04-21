namespace KokoEngine
{
    public static class Input
    {
        private static IInputManager _inputManager;

        public static void Init(IInputManager inputManager)
        {
            _inputManager = inputManager;
        }

        public static bool GetAction(string actionName) => _inputManager.GetAction(actionName);
        public static bool GetActionDown(string actionName) => _inputManager.GetActionDown(actionName);
        public static bool GetActionUp(string actionName) => _inputManager.GetActionUp(actionName);
        public static float GetAxis(string axisName) => _inputManager.GetAxis(axisName);
    }
}
