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

namespace Entrega1Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly int N_BOXS_ROW = 45;
        readonly int N_BOXS_COL = 45;
        public MainWindow()
        {
            Loaded += MainWindow_Loaded;
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DrawInitialSnake();
        }

        private void DrawInitialSnake()
        {
            var elipse = new Ellipse()
            {
                Width = MyCanvas.Width / N_BOXS_COL,
                Height = MyCanvas.Height / N_BOXS_ROW,
                Stroke = Brushes.Black,
                Fill = Brushes.Orange
            };
            Canvas.SetTop(elipse, 0);
            Canvas.SetLeft(elipse, 0);
            MyCanvas.Children.Add(elipse);
        }
    }
}
