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

namespace MouseDrag
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public bool ObjPressed { get; set; }
        public Ellipse EllipsePressed { get; set; }

        public Point PointPressed { get; set; }
        public MainWindow()
        {
            Loaded += MainWindow_Loaded;
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ObjPressed = false;
            MyCanvas.MouseDown += MyCanvas_MouseDown;
            MyCanvas.MouseUp += MyCanvas_MouseUp;
            MyCanvas.MouseMove += MyCanvas_MouseMove;
            // Mouse.Capture(MyCanvas, CaptureMode.Element);
        }

        private void MyCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!ObjPressed)
                return;
            Canvas.SetTop(EllipsePressed, e.GetPosition(MyCanvas).Y - PointPressed.Y);
            Canvas.SetLeft(EllipsePressed, e.GetPosition(MyCanvas).X - PointPressed.X);
        }

        private void MyCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (ObjPressed)
            {
                EllipsePressed.Fill = Brushes.Orange;
            }
            ObjPressed = false;
        }

        private void MyCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ObjPressed)
            {
                return;
            }
            var elipse = new Ellipse()
            {
                Width = 80,
                Height = 80,
                Stroke = Brushes.Black,
                Fill = Brushes.Orange
            };
            Canvas.SetTop(elipse, e.GetPosition(MyCanvas).Y - (elipse.Height / 2));
            Canvas.SetLeft(elipse, e.GetPosition(MyCanvas).X - (elipse.Width / 2));
            MyCanvas.Children.Add(elipse);
            elipse.MouseDown += Elipse_MouseDown;
        }

        private void Elipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var elipse = (Ellipse)sender;
            elipse.Fill = Brushes.Purple;
            ObjPressed = true;
            EllipsePressed = elipse;
            PointPressed = new Point(e.GetPosition(elipse).X, e.GetPosition(elipse).Y);
            MyCanvas.Children.Remove(elipse);
            MyCanvas.Children.Add(elipse);
            e.Handled = true;
        }

    }
}
