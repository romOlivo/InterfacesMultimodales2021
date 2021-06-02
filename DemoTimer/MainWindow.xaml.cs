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
using System.Windows.Threading;

namespace DemoTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer timer = new DispatcherTimer();
        Ellipse circle;
        double circle_radious = 50;     // Radio del círculo
        double rotation_radious = 120;   // Distancia del centro
        private int step;
        public int Step
        {
            get => step; set
            {
                step = value;
                Canvas.SetTop(circle, MyCanvas.Height / 2 - (circle.Height / 2) +
                    rotation_radious * Math.Sin(step * 2 * Math.PI / 100));
                Canvas.SetLeft(circle, MyCanvas.Width / 2 - (circle.Width / 2) +
                    rotation_radious * Math.Cos(step * 2 * Math.PI / 100));
            }
        }
        public MainWindow()
        {
            Loaded += MainWindow_Loaded;
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            circle = new Ellipse()
            {
                Width = circle_radious / 2,
                Height = circle_radious / 2,
                Stroke = Brushes.Black,
                Fill = Brushes.Red
            };
            Canvas.SetTop(circle, MyCanvas.Height / 2 - (circle.Height / 2));
            Canvas.SetLeft(circle, MyCanvas.Width / 2 - (circle.Width / 2));
            MyCanvas.Children.Add(circle);

            circle = new Ellipse()
            {
                Width = circle_radious,
                Height = circle_radious,
                Stroke = Brushes.Black,
                Fill = Brushes.White
            };
            Canvas.SetTop(circle, MyCanvas.Height / 2 - (circle.Height / 2));
            Canvas.SetLeft(circle, MyCanvas.Width / 2 - (circle.Width / 2));
            MyCanvas.Children.Add(circle);

            Step = 0;
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Step += 1;
        }
    }
}
