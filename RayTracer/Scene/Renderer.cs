using Nexus;
using Nexus.Graphics.Colors;
using RayTracer.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Scene
{
    public class Renderer
    {
        private Scene _scene;

        public Scene Scene
        {
            get { return _scene; }
        }

        private float _length;

        public float Length
        {
            get { return _length; }
            set { _length = value; }
        }

        private Color _ambientColor;

        public Color AmbientColor
        {
            get { return _ambientColor; }
            set { _ambientColor = value; }
        }

        public Renderer(Scene scene, float length)
        {
            _scene = scene;
            _length = length;
        }

        public Color RenderPixel(int x, int y)
        {
            var pixelPosition = new Vector3D(x, y, 0);
            var transformedPixelPosition = _scene.Camera.Transform.TransformationMatrix.Transform(pixelPosition);

            Ray pixelRay = new Ray(transformedPixelPosition, _scene.Camera.Transform.Forward, _length);
            pixelRay.Cast(_scene);

            if (pixelRay.Vectors.Count == 1)
            {
                return Colors.Black;
            }
            else if(pixelRay.Vectors.Count > 1)
            {
                return _ambientColor;
            }
            //TODO: Add more color logics
            return _ambientColor;
        }
    }
}
