using System.Collections.Generic;

namespace KokoEngine
{
    /// <summary>
    /// Base agent that will be controlled by Steering Behaviours.
    /// </summary>
    public interface IVehicle : IBehaviour
    {
        /// <summary>
        /// The Steering Behaviours that can be applied to this vehicle.
        /// </summary>
        List<ISteeringBehaviour> Behaviours { get; }

        /// <summary>
        /// The direction of the vehicle.
        /// </summary>
        Vector3 Direction { get; }

        /// <summary>
        /// The maximum speed that the vehicle can attain.
        /// </summary>
        float MaxSpeed { get; set; }

        /// <summary>
        /// The current position of the vehicle.
        /// </summary>
        Vector3 Position { get; }

        /// <summary>
        /// The current velocity of the vehicle.
        /// </summary>
        Vector3 Velocity { get; }
    }
}