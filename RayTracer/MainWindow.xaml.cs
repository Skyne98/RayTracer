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

        public MainWindow()
        {
            InitializeComponent();

            _scene = new Scene.Scene();
            _scene.Renderer.AmbientColor = Nexus.Graphics.Colors.Colors.Green;
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
    }
}
