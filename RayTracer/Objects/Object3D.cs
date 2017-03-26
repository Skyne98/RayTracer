using Nexus;
using RayTracer.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Objects
{
    public class Object3D
    {
        private Transform _transform;

        public Transform Transform
        {
            get { return _transform; }
        }

        private List<BaseUnit> _units;

        public List<BaseUnit> Units
        {
            get { return _units; }
        }

        public Object3D()
        {
            _transform = new Transform();
            _units = new List<BaseUnit>();
        }

        public CollisionVector CollidesWith(Vector3D vector, Vector3D origin)
        {
            CollisionVector collisionVector = null;
            vector = Matrix3D.Invert(_transform.TransformationMatrix).Transform(vector);
            origin = Matrix3D.Invert(_transform.TransformationMatrix).TransformVector(origin);
            foreach (BaseUnit unit in _units)
            {
                collisionVector = unit.CollidesWith(vector, origin);
                if(collisionVector != null)
                {
                    return new CollisionVector(collisionVector.Color,
                        _transform.TransformationMatrix.Transform(collisionVector.Vector), collisionVector.Face);
                }
            }
            return collisionVector;
        }
    }
}
