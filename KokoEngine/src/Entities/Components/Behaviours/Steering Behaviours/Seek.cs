namespace KokoEngine
{
    public class Seek : SteeringBehaviour
    {
        public ITransform target;

        public override Vector3 Calculate(Vehicle vehicle)
        {
            return behaviours.Seek(target.Position, vehicle);
        }
    }
}
