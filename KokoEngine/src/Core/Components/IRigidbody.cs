namespace KokoEngine
{
    /// <summary>
    /// Provides physics capabilities to a GameObject.
    /// </summary>
    public interface IRigidbody : IComponent
    {
        /// <summary>
        /// Amount of reduction applied to residual velocity each frame.
        /// </summary>
        float Damping { get; }

        /// <summary>
        /// Force of gravity.
        /// </summary>
        Vector3 Gravity { get; }

        /// <summary>
        /// Mass of the body.
        /// </summary>
        float mass { get; set; }

        /// <summary>
        /// Velocity of the body.
        /// </summary>
        Vector3 velocity { get; set; }

        /// <summary>
        /// Acceleration of the body.
        /// </summary>
        Vector3 acceleration { get; }

        /// <summary>
        /// Adds a force to this body. Added forces will accomulate.
        /// </summary>
        /// <param name="force">The amount of force to be added.</param>
        void AddForce(Vector3 force);
    }
}
