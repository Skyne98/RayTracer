using Nexus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Structs
{
    public class Transform
    {
        public readonly static Vector3D WorldForward = new Vector3D(0, 0, 1);
        public readonly static Vector3D WorldUp = new Vector3D(0, 1, 0);
        public readonly static Vector3D WorldRight = new Vector3D(1, 0, 0);

        private Vector3D _position;
        public Vector3D Position
        {
            get { return _position; }
            set { _position = value; }
        }

        private Vector3D _rotation;
        public Vector3D Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        private Vector3D _scale;
        public Vector3D Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        public Matrix3D TransformationMatrix
        {
            get { return GetTransformationMatrix(); }
        }

        //Basic vectors
        public Vector3D Forward 
        {
            get { return GetForward(); }
        }
        public Vector3D Up
        {
            get { return GetUp(); }
        }
        public Vector3D Right
        {
            get { return GetRight(); }
        }

        public Transform()
        {
            _position = Vector3D.Zero;
            _rotation = Vector3D.Zero;
            _scale = Vector3D.One;
        }

        public Matrix3D GetTransformationMatrix()
        {
            //Matrix3D scaleMatrix = new Matrix3D(_scale.X, 0, 0, 0, 0, _scale.Y, 0, 0, 0, 0, _scale.Z, 0, 0, 0, 0, 1);
            //Matrix3D xRotationMatrix = new Matrix3D(1, 0, 0, 0, 0, (float)Math.Cos(_rotation.X), (float)-Math.Sin(_rotation.X), 0, 0, (float)Math.Sin(_rotation.X), (float)Math.Cos(_rotation.X), 0, 0, 0, 0, 1);
            //Matrix3D yRotationMatrix = new Matrix3D((float)Math.Cos(_rotation.Y), 0, (float)Math.Sin(_rotation.Y), 0, 0, 1, 0, 0, (float)-Math.Sin(_rotation.Y), 0, (float)Math.Cos(_rotation.Y), 0, 0, 0, 0, 1);
            //Matrix3D zRotationMatrix = new Matrix3D((float)Math.Cos(_rotation.Z), (float)-Math.Sin(_rotation.Z), 0, 0, (float)Math.Sin(_rotation.Z), (float)Math.Cos(_rotation.Z), 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
            //Matrix3D translateMatrix = new Matrix3D(1, 0, 0, _position.X, 0, 1, 0, _position.Y, 0, 0, 1, _position.Z, 0, 0, 0, 1);

            //Matrix3D transformationMatrix = scaleMatrix * xRotationMatrix * yRotationMatrix * zRotationMatrix * translateMatrix;

            //return transformationMatrix;

            Matrix3D scaleMatrix = Matrix3D.CreateScale(_scale);
            Matrix3D xRotationMatrix = Matrix3D.CreateRotationX(MathUtility.ToRadians(_rotation.X));
            Matrix3D yRotationMatrix = Matrix3D.CreateRotationY(MathUtility.ToRadians(_rotation.Y));
            Matrix3D zRotationMatrix = Matrix3D.CreateRotationZ(MathUtility.ToRadians(_rotation.Z));
            Matrix3D translateMatrix = Matrix3D.CreateTranslation(_position.X, _position.Y, _position.Z);

            Matrix3D transformationMatrix = scaleMatrix * zRotationMatrix * yRotationMatrix * xRotationMatrix * translateMatrix;
            //Matrix3D transformationMatrix = scaleMatrix * xRotationMatrix * yRotationMatrix * zRotationMatrix * translateMatrix;
            //Matrix3D transformationMatrix = translateMatrix * zRotationMatrix * yRotationMatrix * xRotationMatrix * scaleMatrix;

            return transformationMatrix;
        }

        public Vector3D GetForward()
        {
            var worldForward = Transform.WorldForward;

            var transformedForward = TransformationMatrix.Transform(worldForward);
            transformedForward.Normalize();

            return transformedForward;
        }
        public Vector3D GetUp()
        {
            var worldUp = Transform.WorldUp;

            var transformedForward = TransformationMatrix.Transform(worldUp);
            transformedForward.Normalize();

            return transformedForward;
        }
        public Vector3D GetRight()
        {
            var worldRight = Transform.WorldRight;

            var transformedForward = TransformationMatrix.Transform(worldRight);
            transformedForward.Normalize();

            return transformedForward;
        }
    }
}
