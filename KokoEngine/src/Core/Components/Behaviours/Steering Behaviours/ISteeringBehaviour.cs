namespace KokoEngine
{
    /// <summary>
    /// Base class for Steering Behaviours.
    /// </summary>
    public interface ISteeringBehaviour : IBehaviour
    {
        /// <summary>
        /// Performs the calculation of the respective steering behaviour.
        /// </summary>
        /// <param name="vehicle">The Vehicle associated with the steering behaviour.</param>
        /// <returns>The force resulting from the behaviour calculation.</returns>
        Vector3 Calculate(IVehicle vehicle);
    }
}