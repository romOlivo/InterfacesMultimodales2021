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
        public MainWindow()
        {
            Loaded += MainWindow_Loaded;
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MyCanvas.MouseDown += MyCanvas_MouseDown;
        }

        private void MyCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
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
        }
    }
}
