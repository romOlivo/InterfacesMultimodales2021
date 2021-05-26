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
using System.Windows.Media.Animation;
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

            Slider slider3 = new Slider() { Minimum = 0, Maximum = 360, Width = 200 }; 
            TransformGroup tg = new TransformGroup(); 
            RotateTransform rt = new RotateTransform() { CenterX = r.Width / 2, CenterY = r.Height / 2 }; 
            tg.Children.Add(rt); r.RenderTransform = tg; 
            slider3.Height = 200;
            Canvas.SetTop(slider3, 250);
            MyCanvas.Children.Add(slider3); 
            Binding binding3 = new Binding("Value"); 
            binding3.Source = slider3; 
            BindingOperations.SetBinding(rt, RotateTransform.AngleProperty, binding3);

            Rectangle r2 = new Rectangle
            {
                Width = 200,
                Height = 150,
                Stroke = Brushes.Black,
                StrokeThickness = 3,
                Fill = Brushes.Orange
            };
            Canvas.SetTop(r2, 300);
            Canvas.SetLeft(r2, 500);
            MyCanvas.Children.Add(r2);

            TransformGroup tg2 = new TransformGroup(); 
            RotateTransform rt2 = new RotateTransform() { CenterX = r2.Width / 2, CenterY = r2.Height / 2 };
            tg2.Children.Add(rt2); 
            r2.RenderTransform = tg2; 
            DoubleAnimation an = new DoubleAnimation(); 
            an.Duration = new Duration(TimeSpan.FromSeconds(1)); 
            an.From = 0; an.To = 360; 
            an.FillBehavior = FillBehavior.Stop; 
            an.RepeatBehavior = RepeatBehavior.Forever; 
            rt2.BeginAnimation(RotateTransform.AngleProperty, an);
        }

        private void R_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Me has pulsado");
        }
    }
}
