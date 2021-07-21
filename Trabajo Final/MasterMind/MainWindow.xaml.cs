using System;
using System.Collections.Generic;
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

namespace MasterMind
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _digitsTB = new TextBlock[] { n1TB, n2TB, n3TB, n4TB };
            _borders = new Border[] { n1Border, n2Border, n3Border, n4Border };
            KeyDown += new KeyEventHandler(MainWindow_KeyDown);
            //solutionSP.Visibility = Visibility.Hidden;   //Descomentar esta línea para ocultar la solución



            NewGame();
        }

        void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (!_validKeys.Contains(e.Key)) return;
            var digit = e.Key.ToString().Last().ToString();
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
            return Enumerable.Range(0, solution.Length).Count(i=>solution[i]==test[i]); //Esto es LINQ
        }

        int NumInjuries(string solution, string test)
        {
            return Enumerable.Range(0, solution.Length).Count(i => solution[i] != test[i] && solution.Contains(test[i])); //Esto es LINQ
        }

        string GenerateValidNumber() // cuatro números del 0 al 9, sin repetición
        {
            string n="";
            while (n.Length<4) {
                var d = r.Next(10).ToString();
                if (!n.Contains(d)) n += d;
            }
            return n;
        }
    }
}
