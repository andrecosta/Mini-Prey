using System;
using System.Collections.Generic;
using System.Linq;

namespace KokoEngine
{
    public class SteeringBehaviours
    {
        public Vector3 Seek(Vector3 target, Vehicle vehicle)
        {
            Vector3 distanceToTarget = target - vehicle.Position;
            Vector3 desired = distanceToTarget.Normalized * vehicle.MaxSpeed;

            return desired - vehicle.Velocity;
        }

        public Vector3 Flee(Vector3 target, Vehicle vehicle)
        {
            Vector3 distanceFromTarget = vehicle.Position - target;
            Vector3 desired = distanceFromTarget.Normalized * vehicle.MaxSpeed;

            return desired - vehicle.Velocity;
        }

        public Vector3 Pursuit(Vehicle target, Vehicle vehicle)
        {
            Vector3 distanceToTarget = target.Position - vehicle.Position;
            float t = distanceToTarget.magnitude / target.MaxSpeed;
            Vector3 futurePosition = target.Position + target.Velocity * t;

            return Seek(futurePosition, vehicle);
        }
    }
}
