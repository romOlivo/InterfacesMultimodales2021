using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
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

namespace DemoSynthesis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SpeechSynthesizer synth = new SpeechSynthesizer();
        public MainWindow()
        {
            Loaded += MainWindow_Loaded;
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
             
            var voices = synth.GetInstalledVoices();
            synth.SelectVoice("Microsoft Zira Desktop");   //Inglés
            // synth.SelectVoice(voice[0].VoiceInfo.Name); 

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PromptBuilder savedPrompt = new PromptBuilder();
            savedPrompt.AppendSsmlMarkup(myText.Text);
            synth.Speak(savedPrompt);
        }
    }
}
