namespace KokoEngine
{
    public class Key
    {
        public enum State { Up, Down }

        public string Name;
        public State CurrentState;
        public State PreviousState;

        public Key(string name)
        {
            Name = name;
        }
    }
}
