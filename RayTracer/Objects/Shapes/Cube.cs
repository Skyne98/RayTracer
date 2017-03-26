using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Objects.Shapes
{
    public class Cube : Object3D
    {
        public Cube()
        {
            Units.Add(new BaseUnit());
        }
    }
}
