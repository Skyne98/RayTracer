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

        public CollisionVector CollidesWith(Vector3D vector, Vector3D origin)
        {
            vector = Vector3D.Normalize(vector);

            vector = Matrix3D.Invert(_transform.TransformationMatrix).Transform(vector);
            origin = Matrix3D.Invert(_transform.TransformationMatrix).TransformVector(origin);

            var result = VectorAABBIntersection(vector, origin);
            bool intersected = result.intersected;
            if (!intersected)
            {
                return null;
            }

            //Collision found
            Vector3D collisionVector = result.collisionVector;
            collisionVector = _transform.TransformationMatrix.Transform(collisionVector);

            //Find collision face
            //0 - South
            //1 - East
            //2 - North
            //3 - West
            //4 - Up
            //5 - Down

            //TODO: Find face
            byte collisionFace = 0;


            CollisionVector collVector = new CollisionVector(_color, collisionVector, collisionFace);
            return collVector;
        }

        private (bool intersected, Vector3D collisionVector) VectorAABBIntersection(Vector3D vector, Vector3D origin)
        {
            //first test if start in box
            if (origin.X >= 0
                && origin.X <= 1
                && origin.Y >= 0
                && origin.Y <= 1
                && origin.Z >= 0
                && origin.Z <= 1)
                return (true, Vector3D.Zero);// here we concidere cube is full and origine is in cube so intersect at origine

            //Second we check each face
            Vector3D maxT = new Vector3D(-1.0f);
            //Vector3 minT = new Vector3(-1.0f);
            //calcul intersection with each faces
            if (origin.X < 0 && vector.X != 0.0f)
                maxT.X = (0 - origin.X) / vector.X;
            else if (origin.X > 1 && vector.X != 0.0f)
                maxT.X = (1 - origin.X) / vector.X;
            if (origin.Y < 0 && vector.Y != 0.0f)
                maxT.Y = (0 - origin.Y) / vector.Y;
            else if (origin.Y > 1 && vector.Y != 0.0f)
                maxT.Y = (1 - origin.Y) / vector.Y;
            if (origin.Z < 0 && vector.Z != 0.0f)
                maxT.Z = (0 - origin.Z) / vector.Z;
            else if (origin.Z > 1 && vector.Z != 0.0f)
                maxT.Z = (1 - origin.Z) / vector.Z;

            //get the maximum maxT
            if (maxT.X > maxT.Y && maxT.X > maxT.Z)
            {
                if (maxT.X < 0.0f)
                    return (false, Vector3D.Zero);// ray go on opposite of face
                                //coordonate of hit point of face of cube
                float coord = origin.Z + maxT.X * vector.Z;
                // if hit point coord ( intersect face with ray) is out of other plane coord it miss 
                if (coord < 0 || coord > 1)
                    return (false, Vector3D.Zero);
                coord = origin.Y + maxT.X * vector.Y;
                if (coord < 0 || coord > 1)
                    return (false, Vector3D.Zero);
                return (true, vector * maxT.X);
            }
            if (maxT.Y > maxT.X && maxT.Y > maxT.Z)
            {
                if (maxT.Y < 0.0f)
                    return (false, Vector3D.Zero);// ray go on opposite of face
                                                  //coordonate of hit point of face of cube
                float coord = origin.Z + maxT.Y * vector.Z;
                // if hit point coord ( intersect face with ray) is out of other plane coord it miss 
                if (coord < 0 || coord > 1)
                    return (false, Vector3D.Zero);
                coord = origin.X + maxT.Y * vector.X;
                if (coord < 0 || coord > 1)
                    return (false, Vector3D.Zero);
                return (true, vector * maxT.Y);
            }
            else //Z
            {
                if (maxT.Z < 0.0f)
                    return (false, Vector3D.Zero);// ray go on opposite of face
                                                  //coordonate of hit point of face of cube
                float coord = origin.X + maxT.Z * vector.X;
                // if hit point coord ( intersect face with ray) is out of other plane coord it miss 
                if (coord < 0 || coord > 1)
                    return (false, Vector3D.Zero);
                coord = origin.Y + maxT.Z * vector.Y;
                if (coord < 0 || coord > 1)
                    return (false, Vector3D.Zero);
                return (true, vector * maxT.Z);
            }
        }
    }
}
