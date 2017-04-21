namespace KokoEngine
{
    public class Flee : SteeringBehaviour
    {
        public ITransform target;

        public override Vector3 Calculate(IVehicle vehicle)
        {
            return behaviours.Flee(target.Position, vehicle);
        }
    }
}
