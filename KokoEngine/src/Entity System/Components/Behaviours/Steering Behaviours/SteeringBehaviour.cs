namespace KokoEngine
{
    public abstract class SteeringBehaviour : Behaviour, ISteeringBehaviour
    {
        protected SteeringBehaviours behaviours = new SteeringBehaviours();

        public abstract Vector3 Calculate(IVehicle vehicle);
    }
}
