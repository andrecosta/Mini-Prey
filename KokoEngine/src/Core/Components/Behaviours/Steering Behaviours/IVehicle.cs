using System.Collections.Generic;

namespace KokoEngine
{
    public interface IVehicle : IBehaviour
    {
        List<ISteeringBehaviour> Behaviours { get; }
        Vector3 Direction { get; }
        float MaxSpeed { get; set; }
        Vector3 Position { get; }
        Vector3 Velocity { get; }
    }
}