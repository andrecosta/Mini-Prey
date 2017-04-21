using System.Collections.Generic;

namespace KokoEngine
{
    public class InputAction
    {
        public string Name { get; }
        public List<Key> Keys { get; }
        // Callbacks? for command pattern

        public InputAction(string name, List<Key> keys)
        {
            Name = name;
            Keys = keys;
        }
    }
}
