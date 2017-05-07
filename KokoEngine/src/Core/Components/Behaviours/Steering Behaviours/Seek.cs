namespace KokoEngine
{
    public class Seek : SteeringBehaviour
    {
        public static bool Paused;
        public ITransform target;

        public override Vector3 Calculate(IVehicle vehicle)
        {
            if (Paused) return Vector3.Zero;

            return behaviours.Seek(target.Position, vehicle);
        }
    }
}
