using Nexus;
using RayTracer.Objects;
using RayTracer.Objects.Shapes;
using RayTracer.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RayTracer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Scene.Scene _scene;
        Cube _cube;

        public MainWindow()
        {
            InitializeComponent();

            _scene = new Scene.Scene();
            _scene.Renderer.AmbientColor = Nexus.Graphics.Colors.Colors.Green;

            //_scene.Camera.Transform.Position = new Vector3D(0, 100, 0);
            _scene.Camera.Transform.Rotation += new Vector3D(0, 0, 0);

            _cube = new Cube();
            _cube.Transform.Position = new Vector3D(125, 75, 20);
            _cube.Transform.Scale = new Vector3D(20, 40, 20);
            //cube.Transform.Rotation = new Vector3D(45, 45, 45);
            _scene.Objects.Add(_cube);
        }

        private void renderPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _scene.Camera.OnViewportSizeChanged((int)e.NewSize.Width, (int)e.NewSize.Height);

            RenderToImage();
        }

        void RenderToImage()
        {
            var width = _scene.Camera.Width;
            var height = _scene.Camera.Height;

            var bitmap = BitmapFactory.New(width, height);

            using (bitmap.GetBitmapContext())
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        var renderedColor = _scene.Renderer.RenderPixel(x, y);
                        var systemColor = new Color();
                        systemColor.R = renderedColor.R;
                        systemColor.G = renderedColor.G;
                        systemColor.B = renderedColor.B;
                        systemColor.A = renderedColor.A;

                        bitmap.SetPixel(x, y, systemColor);
                    }
                }
            }

            renderedImage.Source = bitmap;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                _cube.Transform.Rotation += new Vector3D(0, 15, 15);

                RenderToImage();
            }
        }
    }
}
