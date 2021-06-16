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
using WiimoteLib;

namespace WiimoteButtons
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Wiimote wm = new Wiimote();
        public MainWindow()
        {
            Loaded += MainWindow_Loaded;
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            wm.Connect();
            wm.SetLEDs(3);
            wm.WiimoteChanged += Wm_WiimoteChanged;
            wm.SetReportType(InputReport.Buttons, true);
        }

        private void Wm_WiimoteChanged(object sender, WiimoteChangedEventArgs e)
        {
            WiimoteState estado = e.WiimoteState;
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, 
                new Action <bool, bool> (updateLabels), 
                estado.ButtonState.A, estado.ButtonState.B);
        }


        void updateLabels(bool lA, bool lB)
        {
            if (lA)
            {
                LabelA.Background = Brushes.DarkGreen;
                LabelA.Foreground = Brushes.White;
            } else
            {
                LabelA.Background = Brushes.White;
                LabelA.Foreground = Brushes.Black;
            }


            if (lB)
            {
                LabelB.Background = Brushes.DarkRed;
                LabelB.Foreground = Brushes.White;
            }
            else
            {
                LabelB.Background = Brushes.White;
                LabelB.Foreground = Brushes.Black;
            }

        }
    }
}
