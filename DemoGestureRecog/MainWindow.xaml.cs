using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WiimoteLib;
using WiimoteGestureLib;

namespace DemoGestureRecog
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ObservableCollection<string> Etiquetas { get; set; }

        private readonly Wiimote wm = new Wiimote();
        private readonly GerturerCapturer gc = new GerturerCapturer();
        private readonly GertureRecognizer gr = new GertureRecognizer();
        public MainWindow()
        {
            Loaded += MainWindow_Loaded;
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Inicializamos las etiquetas del prototipo
            Etiquetas = new ObservableCollection<string>();
            etiquetasLB.ItemsSource = Etiquetas;
            NombreTB.TextChanged += NombreTB_TextChanged;
            Etiquetas.CollectionChanged += Etiquetas_CollectionChanged;

            // Inicializando Wiimote
            wm.Connect();
            wm.SetLEDs(3);
            wm.SetReportType(InputReport.ButtonsAccel, true);

            // Configuramos el foco por defecto
            NombreTB.Focus();
        }

        #region Eventos Interfaz

        private void Etiquetas_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            StartB.IsEnabled = Etiquetas.Count() > 0;
        }

        private void NombreTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            AddB.IsEnabled = !(NombreTB.Text == null || NombreTB.Text == "");
        }

        #endregion

        #region Botones

        private void RadB_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StartB_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (NombreTB.Text == null || NombreTB.Text == "")
                return;
            Etiquetas.Add(NombreTB.Text);
            NombreTB.Text = "";
            NombreTB.Focus();
        }

        #endregion

        #region Eventos Wiimote

        #endregion

    }
}
