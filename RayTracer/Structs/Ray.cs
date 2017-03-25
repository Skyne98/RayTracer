using Nexus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RayTracer.Scene;

namespace RayTracer.Structs
{
    public class Ray
    {
        private float _length;
        public float Length
        {
            get { return _length; }
        }

        private float _maxLength;
        public float MaxLength
        {
            get { return _maxLength; }
        }

        private List<Vector3D> _vectors;
        public List<Vector3D> Vectors
        {
            get { return _vectors; }
        }

        private Vector3D _position;
        public Vector3D Position
        {
            get { return _position; }
        }

        private Vector3D _direction;
        public Vector3D Direction
        {
            get { return _direction; }
        }

        /// <summary>
        /// Create new ray.
        /// </summary>
        /// <param name="position">Position of the ray on the scene.</param>
        /// <param name="direction">Normalized ray's direction.</param>
        /// <param name="maxLength">Max length of the ray.</param>
        public Ray(Vector3D position, Vector3D direction, float maxLength)
        {
            _position = position;
            _direction = direction;
            _maxLength = maxLength;
        }

        /// <summary>
        /// Computes ray's collisions and bounces, casting it from it's position and direction. Repopulates Vectors with new data.
        /// </summary>
        /// <param name="scene">Scene to cast the ray on.</param>
        public void Cast(Scene.Scene scene)
        {
            //TODO: Make more complex raycasting
            _vectors = new List<Vector3D>();

            var tempVector = new Vector3D(_direction.X, _direction.Y, _direction.Z);
            tempVector.Normalize();

            _vectors.Add(tempVector * _maxLength);
        }
    }
}
