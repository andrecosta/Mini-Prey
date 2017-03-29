namespace KokoEngine
{
    public interface IRigidbody : IComponent
    {
        float Damping { get; }
        Vector3 Gravity { get; }
        float mass { get; set; }
        Vector3 velocity { get; }
        Vector3 acceleration { get; }
        void AddForce(Vector3 force);
    }
}
