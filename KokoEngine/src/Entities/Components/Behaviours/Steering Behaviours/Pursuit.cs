namespace KokoEngine
{
    public class Pursuit : SteeringBehaviour
    {
        public Vehicle target;

        public override Vector3 Calculate(Vehicle vehicle)
        {
            return behaviours.Pursuit(target, vehicle);
        }
    }
}
