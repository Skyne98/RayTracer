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

        public CollisionVector CollidesWith(Vector3D vector)
        {
            CollisionVector collisionVector = null;
            vector = Matrix3D.Invert(_transform.TransformationMatrix).Transform(vector);
            foreach (BaseUnit unit in _units)
            {
                collisionVector = unit.CollideWith(vector);
                if(collisionVector != null)
                {
                    return new CollisionVector(collisionVector.Color,
                        _transform.TransformationMatrix.Transform(collisionVector.Vector));
                }
            }
            return collisionVector;
        }
    }
}
