using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Objects.Shapes
{
    public class Cube : Object3D
    {
        private BaseUnit _unit;

        public BaseUnit Unit
        {
            get { return _unit; }
        }

        public Cube()
        {
            _unit = new BaseUnit();
            Units.Add(_unit);
        }
    }
}
