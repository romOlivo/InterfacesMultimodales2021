using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
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

namespace Entregable4Tickets
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SpeechRecognitionEngine speechRecognizer;
        public MainWindow()
        {
            Loaded += MainWindow_Loaded;
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Cargar la gramática
            Grammar g = new Grammar("../../MiGramatica.xml");
            speechRecognizer = new SpeechRecognitionEngine();
            speechRecognizer.LoadGrammar(g);

            // Preparar el reconocedor de voz
            speechRecognizer.SpeechRecognized += SpeechRecognized;
            speechRecognizer.SpeechRecognitionRejected += SpeechRecognitionRejected;
            speechRecognizer.SpeechDetected += SpeechDetected;
            speechRecognizer.SetInputToDefaultAudioDevice();
            speechRecognizer.RecognizeAsync(RecognizeMode.Multiple);

            // Inicializar el texto de la interfaz
            ClearInterface();
        }

        #region Speech

        void SpeechDetected(object sender, SpeechDetectedEventArgs e) { labelTextoReconocido.Content = "<Voz detectada>"; labelProbabilidad.Content = ""; }
        void SpeechRecognitionRejected(object s, SpeechRecognitionRejectedEventArgs e) { labelTextoReconocido.Content = "<No le he oido bien. Repita por favor>"; labelProbabilidad.Content = ""; }
        void SpeechRecognized(object sender, SpeechRecognizedEventArgs e) {
            labelTextoReconocido.Content = e.Result.Text;
            labelProbabilidad.Content = e.Result.Confidence.ToString();
            ClearInterface();
            SearchTicket(e);
        }

        #endregion

        #region Menu

        private void ChangeDebugMode(object sender, RoutedEventArgs e)
        {
            GDebug.Visibility = bDebug.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
        }

        #endregion

        private void ClearInterface()
        {
            Lfrom.Content = "Cualquiera";
            Lto.Content = "Cualquiera";
        }

        private void SearchTicket(SpeechRecognizedEventArgs e)
        {
            if (e.Result.Semantics.ContainsKey("cityFrom"))
                Lfrom.Content = e.Result.Semantics["cityFrom"].Value.ToString();
            if (e.Result.Semantics.ContainsKey("cityTo"))
                Lto.Content = e.Result.Semantics["cityTo"].Value.ToString();
            if (e.Result.Semantics.ContainsKey("day"))
                Lday.Content = e.Result.Semantics["day"].Value.ToString();
            if (e.Result.Semantics.ContainsKey("ntickets"))
                Lntickets.Content = e.Result.Semantics["ntickets"].Value.ToString();
        }


    }
}
