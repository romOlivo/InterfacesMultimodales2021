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

namespace VolumeMeter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private int vmWidth = 40;
        private int vmHeight = 230;
        private int vmNumber = 3;
        private int sepHeight = 2;
        private int sepVm = 8;
        private int stroke = 2;
        private int numberRect = 8;
        private int rectHeight;

        private List<List<Rectangle>> rects;

        private double firstSep = .6;
        private double secondSep = .9;

        public MainWindow()
        {
            Loaded += MainWindow_Loaded;
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MyCanvas.Height = vmHeight + 2 * stroke + (vmNumber - 1) * (vmHeight + 2 * stroke + sepVm);
            MyCanvas.Width = vmWidth;
            rectHeight = (vmHeight + sepHeight) / numberRect - sepHeight;
            MySlider.Maximum = numberRect * 100;
            rects = new List<List<Rectangle>>();
            for (int i = 0; i < vmNumber; i++)
            {
                var top = (sepVm + vmHeight + 2 * stroke) * i;
                addVolumeMeter(top);
            }


        }

        private void addVolumeMeter(int top)
        {
            Rectangle r = new Rectangle
            {
                Width = vmWidth,
                Height = vmHeight + 2 * stroke,
                Stroke = Brushes.Black,
                StrokeThickness = stroke,
                Fill = Brushes.LightGray
            };
            Canvas.SetTop(r, top);
            MyCanvas.Children.Add(r);

            List<Rectangle> obj = new List<Rectangle>();
            for (int i = 0; i < numberRect; i++)
            {
                var color = Brushes.Green;
                if ((double)(numberRect - i) / numberRect > firstSep)
                    color = Brushes.Yellow;
                if ((double)(numberRect - i) / numberRect > secondSep)
                    color = Brushes.Red;
                obj.Add(addRectangle(top + (rectHeight + sepHeight) * i + stroke, color));
            }

            rects.Add(obj);
        }

        private Rectangle addRectangle(int top, Brush color)
        {
            Rectangle r = new Rectangle
            {
                Width = vmWidth - 2 * stroke,
                Height = rectHeight,
                Fill = color,
                Visibility = Visibility.Hidden,
                Opacity = .5

            };
            Canvas.SetTop(r, top);
            Canvas.SetLeft(r, stroke);
            MyCanvas.Children.Add(r);

            return r;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var n = (int)Math.Round(e.NewValue / 100);
            foreach (var vm in rects)
            {
                for (int i = 0; i < vm.Count; i++)
                {
                    if (i < n)
                    {
                        vm[vm.Count - 1 - i].Visibility = Visibility.Visible;
                        vm[vm.Count - 1 - i].Opacity = 1;
                    }
                    else
                    {
                        if (i < (n + 1))
                        {
                            vm[vm.Count - 1 - i].Visibility = Visibility.Visible;
                            vm[vm.Count - 1 - i].Opacity = 1 - i + e.NewValue / 100;
                        }

                        else
                            vm[vm.Count - 1 - i].Visibility = Visibility.Hidden;
                    }
                }
            }
        }
    }
}
