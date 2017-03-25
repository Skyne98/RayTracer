﻿using Nexus;
using Nexus.Graphics.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Structs
{
    public class CollisionVector
    {
        private Color _color;

        public Color Color
        {
            get { return _color; }
        }

        private Vector3D _vector;

        public Vector3D Vector
        {
            get { return _vector; }
        }

        public CollisionVector(Color color, Vector3D vector)
        {
            _color = color;
            _vector = vector;
        }
    }
}
