using System.Collections.Generic;

namespace KokoEngine
{
    /// <summary>
    /// Provides a position, rotation and scale to a GameObject. Transforms can be parented to other Transforms.
    /// </summary>
    public interface ITransform : IComponent
    {
        /// <summary>
        /// The position of the transform.
        /// </summary>
        Vector3 Position { get; set; }

        /// <summary>
        /// The rotation angle of the transform.
        /// </summary>
        float Rotation { get; set; }

        /// <summary>
        /// The scale of the transform for each axis.
        /// </summary>
        Vector3 Scale { get; set; }

        /// <summary>
        /// Returns the 'Up' direction of the transform world-space.
        /// </summary>
        Vector3 Up { get; }

        /// <summary>
        /// Translates the transform.
        /// </summary>
        /// <param name="translation">Amount of translation in 3D.</param>
        void Translate(Vector3 translation);

        /// <summary>
        /// Translates the transform.
        /// </summary>
        /// <param name="translation">Amount of translation in 2D.</param>
        /// <param name="z">Amount of translation on the z axis. (Optional)</param>
        void Translate(Vector2 translation, float z = 0);

        /// <summary>
        /// Translates the transform.
        /// </summary>
        /// <param name="x">Amount of translation on the x axis.</param>
        /// <param name="y">Amount of translation on the y axis.</param>
        /// <param name="z">Amount of translation on the z axis. (Optional)</param>
        void Translate(float x, float y, float z = 0);

        /// <summary>
        /// The parent of this transform.
        /// </summary>
        ITransform Parent { get; set; }

        /// <summary>
        /// The children of this transform.
        /// </summary>
        List<ITransform> Children { get; }

        /// <summary>
        /// The amount of children of this transform.
        /// </summary>
        int ChildCount { get; }

        /// <summary>
        /// Returns a child transform by index.
        /// </summary>
        /// <param name="index">The index of the child.</param>
        /// <returns></returns>
        ITransform GetChild(int index);
    }
}