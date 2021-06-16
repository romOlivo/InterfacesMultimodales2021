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

namespace WiimoteLevel
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
            wm.SetReportType(InputReport.ButtonsAccel, true);
        }

        private void Wm_WiimoteChanged(object sender, WiimoteChangedEventArgs e)
        {
            WiimoteState estado = e.WiimoteState;
            // Aceleraci on en X
            double x = estado.AccelState.Values.X - 0.04;
            // Ajustamos a -1,1
            if (x > 1.0)
                x = 1.0;
            else if (x < -1.0)
                x = -1.0;
            // Calculamos el  ́angulo:
            var angulo = 180 * Math.Acos(-x) / Math.PI;
            Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                new Action<double>(updateLabel),
                angulo);
        }

        void updateLabel(double n)
        {
            LabelDegrees.Content = $"{n:000.0}º";
        }
    }
}
