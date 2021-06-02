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

namespace Entrega2Calculadora
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer timer;
        public MainWindow()
        {
            Loaded += MainWindow_Loaded;
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            myInkCanvas.MouseUp += MyInkCanvas_MouseUp;
            myInkCanvas.MouseMove += MyInkCanvas_MouseMove;
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(2000)
            };
            timer.Tick += Timer_Tick;
        }

        private void MyInkCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            myLabel.Content = "Drawed";
            timer.Stop();
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            myLabel.Content = "Recognized";
        }

        private void MyInkCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            myInkCanvas.Strokes.Clear();
            myLabel.Content = "-";
        }
    }
}
