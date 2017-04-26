namespace KokoEngine
{
    public class Seek : SteeringBehaviour
    {
        public ITransform target;

        public override Vector3 Calculate(IVehicle vehicle)
        {
            return behaviours.Seek(target.Position, vehicle);
        }
    }
}
