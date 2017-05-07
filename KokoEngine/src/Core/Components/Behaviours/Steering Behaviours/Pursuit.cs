namespace KokoEngine
{
    public class Pursuit : SteeringBehaviour
    {
        public static bool Paused;
        public Vehicle target;

        public override Vector3 Calculate(IVehicle vehicle)
        {
            if (Paused) return Vector3.Zero;

            return behaviours.Pursuit(target, vehicle);
        }
    }
}
