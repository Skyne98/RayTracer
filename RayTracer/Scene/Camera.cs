using Nexus;
using RayTracer.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RayTracer.Scene
{
    public class Camera
    {
        private int _width;
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        private int _height;
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        private Transform _transform;

        public Transform Transform
        {
            get { return _transform; }
            set { _transform = value; }
        }


        public Camera()
        {
            _transform = new Transform();
        }

        public void OnViewportSizeChanged(int width, int height)
        {
            _width = width;
            _height = height;

            Render();
        }

        public void Render()
        {
            //render stuff
            Console.WriteLine($"Rendered to viewport {_width} / {_height}");
        }
    }
}
