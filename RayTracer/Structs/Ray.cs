using Nexus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RayTracer.Scene;
using RayTracer.Objects;

namespace RayTracer.Structs
{
    public class Ray
    {
        private float _remainingLength;

        public float RemainingLength
        {
            get { return _remainingLength; }
        }


        private float _maxLength;
        public float MaxLength
        {
            get { return _maxLength; }
        }

        private List<CollisionVector> _collisionVectors;
        public List<CollisionVector> CollisionVectors
        {
            get { return _collisionVectors; }
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
            _remainingLength = _maxLength;
        }

        /// <summary>
        /// Computes ray's collisions and bounces, casting it from it's position and direction. Repopulates Vectors with new data.
        /// </summary>
        /// <param name="scene">Scene to cast the ray on.</param>
        public void Cast(Scene.Scene scene)
        {
            //TODO: Make more complex raycasting
            _collisionVectors = new List<CollisionVector>();
            _remainingLength = _maxLength;

            var objects = scene.Objects.ToList();
            Object3D collidedObject = null;
            CollisionVector collidedObjectVector = null;

            foreach (var obj in objects)
            {
                var collision = obj.CollidesWith(_direction, _position);

                if (collision != null)
                {
                    if (collidedObject == null || collision.Vector.Length() < collidedObjectVector.Vector.Length())
                    {
                        collidedObject = obj;
                        collidedObjectVector = collision;
                    }
                }
            }

            if (collidedObject != null)
            {
                _collisionVectors.Add(collidedObjectVector);
            }

            _collisionVectors.Add(null);
        }
    }
}
