namespace KokoEngine
{
    public class SteeringBehaviours
    {
        public Vector3 Seek(Vector3 target, IVehicle vehicle)
        {
            Vector3 distanceToTarget = target - vehicle.Position;
            Vector3 desired = distanceToTarget.Normalized * vehicle.MaxSpeed;

            return desired - vehicle.Velocity;
        }

        public Vector3 Flee(Vector3 target, IVehicle vehicle)
        {
            Vector3 distanceFromTarget = vehicle.Position - target;
            Vector3 desired = distanceFromTarget.Normalized * vehicle.MaxSpeed;

            return desired - vehicle.Velocity;
        }

        public Vector3 Pursuit(IVehicle target, IVehicle vehicle)
        {
            Vector3 distanceToTarget = target.Position - vehicle.Position;
            float t = distanceToTarget.Magnitude / vehicle.MaxSpeed;
            Vector3 futurePosition = target.Position + target.Velocity * t;

            return Seek(futurePosition, vehicle);
        }
    }
}
