﻿using System;
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

namespace DemoRecog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        SpeechRecognitionEngine speechRecognizer;
        Grammar grammar;
        GrammarBuilder gb;
        Choices grammarChoices;

        public MainWindow()
        {
            Loaded += MainWindow_Loaded;
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            grammarChoices = new Choices(); 
            foreach (var colorName in new string[] { "rojo", "verde", "azul", "amarillo", "blanco", "negro"}) 
                grammarChoices.Add(colorName); 
            gb = new GrammarBuilder(grammarChoices); 
            grammar = new Grammar(gb);
            speechRecognizer = new SpeechRecognitionEngine();
            speechRecognizer.LoadGrammar(grammar); 
            speechRecognizer.SpeechRecognized += SpeechRecognized; 
            speechRecognizer.SpeechRecognitionRejected += SpeechRecognitionRejected; 
            speechRecognizer.SpeechDetected += SpeechDetected; 
            speechRecognizer.SetInputToDefaultAudioDevice(); 
            speechRecognizer.RecognizeAsync(RecognizeMode.Multiple);
        }

        void SpeechDetected(object sender, SpeechDetectedEventArgs e) { labelTextoReconocido.Content = "<Voz detectada>"; labelProbabilidad.Content = ""; }
        void SpeechRecognitionRejected(object s, SpeechRecognitionRejectedEventArgs e) { labelTextoReconocido.Content = "<No le he oidobien. Repita por favor>"; labelProbabilidad.Content = ""; }
        void SpeechRecognized(object sender, SpeechRecognizedEventArgs e) { labelTextoReconocido.Content = e.Result.Text; labelProbabilidad.Content = e.Result.Confidence.ToString(); }

    }
}
