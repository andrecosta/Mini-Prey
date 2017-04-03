namespace KokoEngine
{
    public interface ISteeringBehaviour : IBehaviour
    {
        Vector3 Calculate(IVehicle vehicle);
    }
}