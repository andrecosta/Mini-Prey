namespace KokoEngine
{
    public abstract class SteeringBehaviour : Behaviour
    {
        protected SteeringBehaviours behaviours = new SteeringBehaviours();

        public abstract Vector3 Calculate(Vehicle vehicle);
    }
}
