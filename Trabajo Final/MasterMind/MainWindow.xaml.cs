
using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Ink;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using WiimoteLib;
using WiimoteGestureLib;
using System.Windows.Media.Animation;

namespace MasterMind
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DebugWindow debugWindow;
        Random r = new Random();
        int _cursorIndex;
        int CursorIndex
        {
            get { return _cursorIndex; }
            set
            {
                foreach (var border in _borders)
                {
                    border.BorderThickness = new Thickness(1);
                    border.BorderBrush = Brushes.Black;
                    border.IsEnabled = false;
                }
                _cursorIndex = value;
                if (_cursorIndex >= 0)
                {
                    _borders[_cursorIndex].BorderThickness = new Thickness(3);
                    _borders[_cursorIndex].BorderBrush = SystemColors.HighlightBrush;
                    _borders[_cursorIndex].IsEnabled = true;
                    BDelete.IsEnabled = !(_cursorIndex == 0);
                    BClear.IsEnabled = !(_cursorIndex == 0);
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        #region Initialize

        private void inicializeInk()
        {
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1200)
            };
            timer.Tick += Timer_Tick;

            allcanvas = new[] { InkN1, InkN2, InkN3, InkN4 };
            foreach (var canvas in allcanvas)
            {
                canvas.PreviewMouseUp += MyInkCanvas_MouseUp;
                canvas.PreviewMouseMove += MyInkCanvas_MouseMove;
                canvas.PreviewMouseDown += MyInkCanvas_MouseDown;
            }

            isDrawing = false;

        }

        private void inicializeSpeech()
        {
            // Cargar la gramática
            Grammar g = new Grammar("../../MiGramatica.xml");
            speechRecognizer = new SpeechRecognitionEngine();
            speechRecognizer.LoadGrammar(g);

            // Preparar el reconocedor de voz
            enableSpeech(true);
            speechRecognizer.SetInputToDefaultAudioDevice();
            speechRecognizer.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void inicializeVoice()
        {
            synth = new SpeechSynthesizer();
            synth.SelectVoice("Microsoft David Desktop");
            Console.WriteLine(synth.Voice.Name);
        }

        private void inicializeWiimote()
        {
            wm.Connect();
            wm.SetLEDs(3);
            wm.SetReportType(InputReport.ButtonsAccel, true);
            wm.WiimoteChanged += gm.OnWiimoteChanged;
            gm.GestureRecognized += Gm_GestureRecognized;
            gm.Load("./../../MisGestos.gst");
            gm.SetStateGestureCapure(GestureManager.StatesGestureCapure.Recog);
        }

        private void inicializeMenu()
        {
            foreach(InstalledVoice voice in synth.GetInstalledVoices())
            {
                MenuItem item = new MenuItem();
                item.Header = voice.VoiceInfo.Name;
                item.Click += ChangeVoice_Click;
                item.IsChecked = ((string)item.Header) == "Microsoft David Desktop";
                voiceSelectorM.Items.Add(item);
            }
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _digitsTB = new TextBlock[] { n1TB, n2TB, n3TB, n4TB };
            _borders = new Border[] { n1Border, n2Border, n3Border, n4Border };
            KeyDown += new KeyEventHandler(MainWindow_KeyDown);

            inicializeWiimote();
            inicializeSpeech();
            inicializeVoice();
            inicializeMenu();
            inicializeInk();

            NewGame();
        }

        #endregion

        #region Input Control

        private void inputDigit(string digit)
        {
            evaluateDigit(digit);
        }

        private void inputDigit(int digit)
        {
            inputDigit($"{digit}");
        }

        #region Keyboard
        Key[] _validKeys = new Key[] { Key.D0, Key.D1, Key.D2, Key.D3, Key.D4, Key.D5, Key.D6, Key.D7, Key.D8, Key.D9,
                                       Key.NumPad0, Key.NumPad1, Key.NumPad2, Key.NumPad3, Key.NumPad4, Key.NumPad5, Key.NumPad6, Key.NumPad7, Key.NumPad8, Key.NumPad9, };

        void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Back)
            {
                BDelete_Click(null, null);
                return;
            }

            if (!_validKeys.Contains(e.Key))
            {
                animateWrongInput();
                return;
            }
            var digit = e.Key.ToString().Last().ToString();
            inputDigit(digit);
        }

        #endregion

        #region Ink
        private Boolean isDrawing;
        private InkCanvas[] allcanvas;
        private DispatcherTimer timer;

        private void Timer_Tick(object sender, EventArgs e)
        {
            int n = -1;
            foreach (InkCanvas canvas in allcanvas)
            {
                n = recognizeDigit(canvas);
                if (n >= 0) break;
            }
            if (n >= 0 && n <= 9)
                inputDigit(n);
            else
                animateWrongInput();
            clearInk();

        }

        private int recognizeDigit(InkCanvas inkCanvas)
        {
            int dev = -3;
            using (MemoryStream ms = new MemoryStream())
            {
                inkCanvas.Strokes.Save(ms);
                var ink = new Ink();
                ink.Load(ms.ToArray());
                using (RecognizerContext context = new RecognizerContext())
                {
                    if (ink.Strokes.Count > 0)
                    {
                        context.Strokes = ink.Strokes;
                        RecognitionStatus status;
                        var result = context.Recognize(out status);
                        if (status == RecognitionStatus.NoError)
                        {
                            try
                            {
                                dev = int.Parse(result.TopString);
                            }
                            catch (Exception e)
                            {
                                dev = -4;
                            }

                        }
                        else
                            dev = -1;
                    }
                    else
                        dev = -2;
                }
            }
            return dev;
        }

        private void clearInk()
        {
            foreach (var canvas in allcanvas)
                canvas.Strokes.Clear();
            timer.Stop();
        }

        private void MyInkCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDrawing = false;
        }

        private void MyInkCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isDrawing = true;
        }

        private void MyInkCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDrawing) return;
            timer.Stop();
            timer.Start();
        }

        #endregion

        #region Speech
        SpeechRecognitionEngine speechRecognizer;
        private bool speechIsEnable = false;

        void SpeechDetected(object sender, SpeechDetectedEventArgs e)
        {
            if (debugWindow == null) return;
            debugWindow.setText("<Voz detectada>");
            debugWindow.setConf(" - ");
        }
        void SpeechRecognitionRejected(object s, SpeechRecognitionRejectedEventArgs e)
        {
            if (debugWindow == null) return;
            debugWindow.setText("<No le he oido bien. Repita por favor>");
            debugWindow.setConf(" - ");
        }
        void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (debugWindow != null)
            {
                debugWindow.setText(e.Result.Text);
                debugWindow.setConf(e.Result.Confidence.ToString());
            }
            if (e.Result.Semantics.ContainsKey("number"))
            {
                var ns = e.Result.Semantics["number"].Value.ToString();
                if (isValidNumber(int.Parse(ns)))
                {
                    if (_solution.Length - _numberToTest.Length >= ns.Length)
                        foreach (char c in ns)
                            inputDigit($"{c}");
                }
            }
        }
        private void enableSpeech(bool enable)
        {
            if (enable == true && speechIsEnable == false)
            {
                speechRecognizer.SpeechRecognitionRejected += SpeechRecognitionRejected;
                speechRecognizer.SpeechRecognized += SpeechRecognized;
                speechRecognizer.SpeechDetected += SpeechDetected;
                speechIsEnable = true;
            }
            if (enable == false && speechIsEnable == true)
            {
                speechRecognizer.SpeechRecognitionRejected -= SpeechRecognitionRejected;
                speechRecognizer.SpeechRecognized -= SpeechRecognized;
                speechRecognizer.SpeechDetected -= SpeechDetected;
                speechIsEnable = false;
            }
        }

        #endregion

        #region Wiimote
        private readonly GestureManager gm = new GestureManager();
        private readonly Wiimote wm = new Wiimote();

        private void Gm_GestureRecognized(string obj)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                new Action<string>(inputDigit), obj);
        }

        #endregion

        #region Buttons

        private void BDelete_Click(object sender, RoutedEventArgs e)
        {
            CursorIndex = CursorIndex - 1;
            _digitsTB[CursorIndex].Text = "";
            _numberToTest = _numberToTest.Remove(_numberToTest.Length - 1);
        }

        private void BClear_Click(object sender, RoutedEventArgs e)
        {
            foreach(var item in _digitsTB)
                item.Text = "";
            CursorIndex = 0;
            _numberToTest = "";
        }

        #endregion

        #endregion

        #region Output Control
        private bool textIsEnable = true;
        private bool voideIsEnable = true;

        private void ClearOutput()
        {
            historyTB.Text = "";
        }

        private void ProgramOutput(string text, string sep = "", bool cancel = true)
        {
            if (textIsEnable)
                OutputText(text, sep);
            if (voideIsEnable)
                OutputVoice(text, cancel);
        }

        #region Text

        private void OutputText(string text, string sep)
        {
            historyTB.Text += text + "\n" + sep;
            historyTB.ScrollToEnd();
        }

        #endregion

        #region Voice
        SpeechSynthesizer synth;
        Dictionary<Char, Char> FILTER_STRINGS = new Dictionary<Char, Char> {
            { '-', '.' },
            { '<', ' '},
            { '>', ' '},
        };

        private string filterTextVoice(string text) {
            string dev = text;
            foreach(char key in FILTER_STRINGS.Keys)
            {
                dev = dev.Replace(key, FILTER_STRINGS[key]);
            }
            return dev;
        }

        private void OutputVoice(string text, bool cancel)
        {
            if(cancel)
                synth.SpeakAsyncCancelAll();
            synth.SpeakAsync(filterTextVoice(text));
        }

        #endregion

        #endregion

        #region Motor Game
        TextBlock[] _digitsTB;
        string _numberToTest;
        Border[] _borders;
        string _solution;
        int _numTries;

        private void evaluateDigit(string digit)
        {
            if (_numberToTest.Contains(digit))
            {
                animateWrongInput(_numberToTest.Length);
                return;
            }
            _digitsTB[_numberToTest.Length].Text = digit;
            _numberToTest += digit;
            CursorIndex = (CursorIndex + 1) % _borders.Length;
            if (_numberToTest.Length == _solution.Length)
            {
                foreach (var digitTB in _digitsTB) digitTB.Text = "";
                TestNumber(_numberToTest);
                _numberToTest = "";
            }
        }

        void TestNumber(string number)
        {
            ++_numTries;
            ProgramOutput(string.Format("Try {0}: Number {1} -> Deaths: {2} - Injuries: {3}.", _numTries, number, NumDeaths(_solution, number), NumInjuries(_solution, number)));
            if (number == _solution)
                EndGame();
            
        }

        private void EndGame()
        {
            ProgramOutput(string.Format("You have found it in {0} tries.", _numTries), cancel: false);
            ProgramOutput(string.Format("Press \"New Game\" to continue.", _numTries), cancel: false);
            CursorIndex = -1;
            enableSpeech(false);
        }

        void NewGame()
        {
            enableSpeech(true);
            _solution = GenerateValidNumber();
            solutionTB.Text = _solution;
            _numberToTest = "";
            _numTries = 0;
            ClearOutput();
            ProgramOutput("Welcome and good luck", "---------------------\n");
            foreach (var digitTB in _digitsTB) digitTB.Text = "";
            CursorIndex = 0;
            ResetDebug();
        }

        int NumDeaths(string solution, string test)
        {
            return Enumerable.Range(0, solution.Length).Count(i => solution[i] == test[i]); //Esto es LINQ
        }

        int NumInjuries(string solution, string test)
        {
            return Enumerable.Range(0, solution.Length).Count(i => solution[i] != test[i] && solution.Contains(test[i])); //Esto es LINQ
        }

        string GenerateValidNumber() // cuatro números del 0 al 9, sin repetición
        {
            string n = "";
            while (n.Length < 4)
            {
                var d = r.Next(10).ToString();
                if (!n.Contains(d)) n += d;
            }
            return n;
        }

        bool isValidNumber(int n)
        {
            bool isValid = true;
            isValid = isValid && n <= 9999 && n >= 0;
            if (isValid)
            {
                HashSet<char> digits = new HashSet<char>();
                string sn = $"{n}";
                foreach (var sd in sn)
                {
                    digits.Add(sd);
                }
                isValid = digits.Count == sn.Length;
            }
            return isValid;
        }

        #endregion

        #region Menu

        void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }

        private void MenuDebugWindow_Click(object sender, RoutedEventArgs e)
        {
            if (debugWindow == null)
            {
                debugWindow = new DebugWindow(this);
                debugWindow.setSolution(_solution);
                debugWindow.Show();
            }
        }

        private void EnableVoice_Click(object sender, RoutedEventArgs e)
        {
            voideIsEnable = !voideIsEnable;
            VoiceEnableB.IsChecked = voideIsEnable;
        }

        private void ChangeVoice_Click(object sender, RoutedEventArgs e)
        {
            foreach (MenuItem item in voiceSelectorM.Items)
                item.IsChecked = false;
            ((MenuItem)sender).IsChecked = true;
            synth.SelectVoice((string)((MenuItem)sender).Header);
        }

        private void IVoiceEnableB_Click(object sender, RoutedEventArgs e)
        {
            IVoiceEnableB.IsChecked = !IVoiceEnableB.IsChecked;
            enableSpeech(IVoiceEnableB.IsChecked == true);
        }

        private void WiimoteEnableB_Click(object sender, RoutedEventArgs e)
        {
            WiimoteEnableB.IsChecked = !WiimoteEnableB.IsChecked;
            if (WiimoteEnableB.IsChecked == true)
                wm.WiimoteChanged += gm.OnWiimoteChanged;
            else
                wm.WiimoteChanged -= gm.OnWiimoteChanged;
        }

        #endregion

        #region Debug Windows

        public void ClosedDebugWindow()
        {
            debugWindow = null;
        }

        private void ResetDebug()
        {
            if (debugWindow == null) return;
            debugWindow.setSolution(_solution);
        }

        #endregion

        #region Animations

        private void animateWrongInput()
        {
            animateWrongInput(_numberToTest.Length);
        }

        private void animateWrongInput(int i)
        {
            _borders[i].BorderBrush = new SolidColorBrush(Colors.Red);
            ColorAnimation animation = new ColorAnimation();
            animation.From = Colors.Red;
            animation.To = SystemColors.HighlightColor;
            animation.Duration = new Duration(TimeSpan.FromSeconds(1));
            _borders[i].BorderBrush.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }

        #endregion

    }
}
