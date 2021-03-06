﻿using RayTracer.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Scene
{
    public class Scene
    {
        private Camera _camera;

        public Camera Camera
        {
            get { return _camera; }
            set { _camera = value; }
        }

        private Renderer _renderer;

        public Renderer Renderer
        {
            get { return _renderer; }
            set { _renderer = value; }
        }

        private List<Object3D> _objects;

        public List<Object3D> Objects
        {
            get { return _objects; }
        }

        public Scene()
        {
            _camera = new Camera();
            _renderer = new Renderer(this, 10);
            _objects = new List<Object3D>();
        }
    }
}
