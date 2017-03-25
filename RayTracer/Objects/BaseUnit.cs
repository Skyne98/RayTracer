using Nexus;
using Nexus.Graphics.Colors;
using RayTracer.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Objects
{
    public class BaseUnit
    {
        private Transform _transform;

        private Color _color = Colors.Red;

        public Transform Transform
        {
            get { return _transform; }
        }

        public Color Color
        {
            get { return _color; }
        }

        public BaseUnit()
        {
            _transform = new Transform();
        }

        public void SetColor(Color color)
        {
            _color = color;
        }

        public CollisionVector CollideWith(Vector3D vector)
        {
            vector = Matrix3D.Invert(_transform.TransformationMatrix).Transform(vector);
            var result = VectorAABBIntersection(vector);
            bool intersected = result.intersected;
            if (!intersected)
            {
                return null;
            }
            Vector3D collisionVector = result.collisionVector;
            collisionVector = _transform.TransformationMatrix.Transform(collisionVector);
            CollisionVector collVector = new CollisionVector(_color, collisionVector);
            return collVector;
        }

        private (bool intersected, Vector3D collisionVector) VectorAABBIntersection(Vector3D vector)
        {
            vector.Normalize();
            Vector3D dirFrac = new Vector3D();
            // r.dir is unit direction vector of ray
            dirFrac.X = 1.0f / vector.X;
            dirFrac.Y = 1.0f / vector.Y;
            dirFrac.Z = 1.0f / vector.Z;
            // lb is the corner of AABB with minimal coordinates - left bottom, rt is maximal corner
            // r.org is origin of ray
            float t1 = (0 - 0) * dirFrac.X;
            float t2 = (1 - 0) * dirFrac.X;
            float t3 = (0 - 0) * dirFrac.Y;
            float t4 = (1 - 0) * dirFrac.Y;
            float t5 = (0 - 0) * dirFrac.Z;
            float t6 = (0 - 0) * dirFrac.Z;

            float tmin = Math.Max(Math.Max(Math.Min(t1, t2), Math.Min(t3, t4)), Math.Min(t5, t6));
            float tmax = Math.Min(Math.Min(Math.Max(t1, t2), Math.Max(t3, t4)), Math.Max(t5, t6));

            var collisionVector = new Vector3D();
            // if tmax < 0, ray (line) is intersecting AABB, but whole AABB is behing us
            if (tmax < 0)
            {
                collisionVector = tmax * dirFrac;
                return (false, Vector3D.Zero);
            }

            // if tmin > tmax, ray doesn't intersect AABB
            if (tmin > tmax)
            {
                collisionVector = tmax * dirFrac;
                return (false, Vector3D.Zero);
            }

            collisionVector = tmin * dirFrac;
            return (true, collisionVector);
        }
    }
}
