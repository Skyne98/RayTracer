using Nexus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public static class Extensions
    {
        public static bool IsDistinguishedIdentity(this Matrix3D matrix)
        {
                return matrix.M11 == 1.0f && matrix.M12 == 0.0f && matrix.M13 == 0.0f && matrix.M14 == 0.0f
                       && matrix.M21 == 0.0f && matrix.M22 == 1.0f && matrix.M23 == 0.0f && matrix.M24 == 0.0f
                       && matrix.M31 == 0.0f && matrix.M32 == 0.0f && matrix.M33 == 1.0f && matrix.M34 == 0.0f
                       && matrix.M41 == 0.0f && matrix.M42 == 0.0f && matrix.M43 == 0.0f && matrix.M44 == 1.0f;
        }

        public static void MultiplyVector(this Matrix3D matrix, ref Vector3D vector)
        {
            if (!matrix.IsDistinguishedIdentity())
            {
                float x = vector.X;
                float y = vector.Y;
                float z = vector.Z;
                vector.X = (((x * matrix.M11) + (y * matrix.M21)) + (z * matrix.M31)) + matrix.M41;
                vector.Y = (((x * matrix.M12) + (y * matrix.M22)) + (z * matrix.M32)) + matrix.M42;
                vector.Z = (((x * matrix.M13) + (y * matrix.M23)) + (z * matrix.M33)) + matrix.M43;
                if (!matrix.IsAffine)
                {
                    float w = (((x * matrix.M14) + (y * matrix.M24)) + (z * matrix.M34)) + matrix.M44;
                    vector.X /= w;
                    vector.Y /= w;
                    vector.Z /= w;
                }
            }
        }

        public static Vector3D TransformVector(this Matrix3D matrix, Vector3D vector)
        {
            matrix.MultiplyVector(ref vector);
            return vector;
        }
    }
}
