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

namespace DemoCanvas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Loaded += MainWindow_Loaded;
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs ea)
        {
            Rectangle r = new Rectangle
            {
                Width = 200,
                Height = 150,
                Stroke = Brushes.Black,
                StrokeThickness = 3,
                Fill = Brushes.Orange
            };
            Canvas.SetTop(r, 100);
            Canvas.SetLeft(r, 100);
            MyCanvas.Children.Add(r);

            r.MouseDown += R_MouseDown;

            Ellipse e = new Ellipse
            {
                Width = 200,
                Height = 150,
                Stroke = Brushes.Black,
                StrokeDashArray = new DoubleCollection() { 2, 3, 4 },
                StrokeThickness = 3,
                Fill = Brushes.Orange
            };
            Canvas.SetTop(e, 300);
            Canvas.SetLeft(e, 100);
            MyCanvas.Children.Add(e);

            Line l = new Line()
            {
                X1 = 0,
                Y1 = 0,
                X2 = 30,
                Y2 = 30,
                StrokeThickness = 4,
                Stroke = Brushes.Black
            };
            Canvas.SetTop(l, 300);
            Canvas.SetLeft(l, 300);
            MyCanvas.Children.Add(l);

            Slider slider = new Slider() { Minimum = 0, Maximum = MyCanvas.ActualWidth - r.Width };
            slider.Width = 200;
            MyCanvas.Children.Add(slider);
            Binding binding = new Binding("Value") { Source = slider };
            BindingOperations.SetBinding(r, Canvas.LeftProperty, binding);
        }

        private void R_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Me has pulsado");
        }
    }
}
