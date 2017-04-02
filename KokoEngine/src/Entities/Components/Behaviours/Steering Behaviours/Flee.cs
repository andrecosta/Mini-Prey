namespace KokoEngine
{
    public class Flee : SteeringBehaviour
    {
        public ITransform target;

        public override Vector3 Calculate(Vehicle vehicle)
        {
            return behaviours.Flee(target.Position, vehicle);
        }
    }
}
