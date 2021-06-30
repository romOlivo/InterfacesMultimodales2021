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
using System.Windows.Threading;

namespace DemoGestureRecog
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ObservableCollection<string> Etiquetas { get; set; }

        private List<Gesture> gestures = new List<Gesture>();

        private int index = 0;

        private readonly Wiimote wm = new Wiimote();
        private readonly GerturerCapturer gc = new GerturerCapturer();
        private readonly GertureRecognizer gr = new GertureRecognizer();

        private readonly string TXT_START = "Start";
        private readonly string TXT_STOP = "Stop";

        private string ButtonText = "Start";
        private bool isTraining = true;
        private bool isRecognizing = false;

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

            // Configuramos la interfaz
            StartB.Content = TXT_START;

            // Inicializando Wiimote
            wm.Connect();
            wm.SetLEDs(3);
            wm.SetReportType(InputReport.ButtonsAccel, true);
            wm.WiimoteChanged += Wm_WiimoteChanged;
            gc.GestureCaptured += Gc_GestureCaptured;
            gr.GestureRecognized += Gr_GestureRecognized;
            gr.SetPrototypes(gestures);

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
            isRecognizing = RecogB.IsChecked == true;
            isTraining = TrainB.IsChecked == true;
        }

        private void StartB_Click(object sender, RoutedEventArgs e)
        {
            if ((string)StartB.Content == TXT_START)
            {
                StartB.Content = TXT_STOP;
                ButtonText = TXT_STOP;
                RecogB.IsEnabled = false;
                TrainB.IsEnabled = false;
                if (isTraining)
                {
                    index = 0;
                    SalidaL.Content = Etiquetas[index];
                }
                if (isRecognizing)
                    updateText("Recognizing");

            }
            else
            {
                RecogB.IsEnabled = true;
                TrainB.IsEnabled = true;
                StartB.Content = TXT_START;
                ButtonText = TXT_START;
                updateText("Done");
            }
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
        private void Wm_WiimoteChanged(object sender, WiimoteChangedEventArgs e)
        {
            if (ButtonText == TXT_START)
                return;
            gc.OnWiimoteChanged(sender, e);
        }

        private void Gc_GestureCaptured(Gesture obj)
        {
            if (isTraining == true)
            {
                obj.Name = Etiquetas[index % 2];
                Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                new Action<Gesture>(configurePrototype), obj);
            }
            else if (isRecognizing == true)
                gr.OnGestureCaptured(obj);
        }

        private void Gr_GestureRecognized(string obj)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                new Action<string>(updateText), obj);
        }

        #endregion

        #region Ventana

        private void configurePrototype(Gesture g)
        {
            gestures.Add(g);
            g.Name = Etiquetas[index];
            nextPrototype();
        }

        private void nextPrototype()
        {
            index += 1;
            if (index == Etiquetas.Count)
            {
                StartB_Click(null, null);
                return;
            }
            updateText(Etiquetas[index]);
        }

        private void updateText(string text)
        {
            SalidaL.Content = text;
        }

        #endregion

    }
}
