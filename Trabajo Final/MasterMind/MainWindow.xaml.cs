
using Microsoft.Ink;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

namespace MasterMind
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private InkCanvas[] allcanvas;
        private Boolean isDrawing;
        DispatcherTimer timer;

        string _solution;
        string _numberToTest;
        TextBlock[] _digitsTB;
        Border[] _borders;
        int _numTries;
        Random r = new Random();
        Key[] _validKeys = new Key[] { Key.D0, Key.D1, Key.D2, Key.D3, Key.D4, Key.D5, Key.D6, Key.D7, Key.D8, Key.D9,
                                       Key.NumPad0, Key.NumPad1, Key.NumPad2, Key.NumPad3, Key.NumPad4, Key.NumPad5, Key.NumPad6, Key.NumPad7, Key.NumPad8, Key.NumPad9, };
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

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _digitsTB = new TextBlock[] { n1TB, n2TB, n3TB, n4TB };
            _borders = new Border[] { n1Border, n2Border, n3Border, n4Border };
            KeyDown += new KeyEventHandler(MainWindow_KeyDown);
            //solutionSP.Visibility = Visibility.Hidden;   //Descomentar esta línea para ocultar la solución

            inicializeInk();

            NewGame();
        }

        #endregion

        #region Input Control

        #region Ink

        private void Timer_Tick(object sender, EventArgs e)
        {
            int n = -1;
            foreach (InkCanvas canvas in allcanvas)
            {
                n = recognizeDigit(canvas);
                if (n >= 0) break;
            }
            if (n >= 0)
                inputDigit(n);
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

        #endregion

        void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (!_validKeys.Contains(e.Key)) return;
            var digit = e.Key.ToString().Last().ToString();
            inputDigit(digit);
        }

        private void inputDigit(string digit)
        {
            if (_numberToTest.Contains(digit)) return;
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

        private void inputDigit(int digit)
        {
            inputDigit($"{digit}");
        }

        void TestNumber(string number)
        {
            ++_numTries;
            historyTB.Text += string.Format("{0}: {1} -> Deaths: {2} - Injuries: {3}.\n", _numTries, number, NumDeaths(_solution, number), NumInjuries(_solution, number));
            if (number == _solution)
            {
                historyTB.Text += string.Format("You have found it in {0} tries.\n", _numTries);
                historyTB.Text += string.Format("Press \"New Game\" to continue...\n", _numTries);
                CursorIndex = -1;
            }
            historyTB.ScrollToEnd();
        }

        void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }

        void NewGame()
        {
            _solution = GenerateValidNumber();
            solutionTB.Text = _solution;
            _numberToTest = "";
            _numTries = 0;
            historyTB.Text = "Welcome and good luck\n";
            historyTB.Text += "---------------------\n";
            foreach (var digitTB in _digitsTB) digitTB.Text = "";
            CursorIndex = 0;
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
    }
}
