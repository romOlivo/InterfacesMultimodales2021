using Microsoft.Ink;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Entrega2Calculadora
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer timer;

        private Boolean isDrawing;
        private string[] delimiters = { "=", "z", "Z" };
        public MainWindow()
        {
            Loaded += MainWindow_Loaded;
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            myInkCanvas.PreviewMouseUp += MyInkCanvas_MouseUp;
            myInkCanvas.PreviewMouseMove += MyInkCanvas_MouseMove;
            myInkCanvas.PreviewMouseDown += MyInkCanvas_MouseDown;
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(2000)
            };
            timer.Tick += Timer_Tick;
            isDrawing = false;
            selectedText.IsChecked = true;
            selectedText.IsEnabled = false;
        }

        #region Eventos de Ratón

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

        #region Manejo de tinta
        private void Timer_Tick(object sender, EventArgs e)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                myInkCanvas.Strokes.Save(ms);
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
                            if (selectedText.IsChecked)
                                textManager(result.TopString);
                            if (selectedCharacter.IsChecked)
                                characterManager(result.TopString);
                        }
                        else
                            MessageBox.Show("Recognition failed");
                    }
                    else
                        MessageBox.Show("No stroke detected");
                }
            }
        }

        private void textManager(string drawedText)
        {
            if (drawedText.Length > 0 &&
                                Array.Exists(delimiters, elem => elem == $"{drawedText.Last()}"))
                showResult(drawedText.Substring(0, drawedText.Length - 1));
            else
                myLabel.Content = drawedText;
        }

        private void showResult(string s)
        {
            DataTable dt = new DataTable();
            var sp = s.Replace("x", "*");
            var v = dt.Compute(sp, "");
            myLabel.Content = $"{s} = {v}";
            timer.Stop();
        }

        private void characterManager(string drawedText)
        {
            if ((string)myLabel.Content == "-")
                myLabel.Content = "";
            if (Array.Exists(delimiters, elem => elem == $"{drawedText.Last()}"))
            {
                showResult((string)myLabel.Content);
            }
            else
            {
                myLabel.Content = $"{myLabel.Content} {drawedText.Last()}";
            }
            myInkCanvas.Strokes.Clear();
            timer.Stop();
        }

        #endregion

        #region Menu
        private void MenuItem_Checked(object sender, RoutedEventArgs e)
        {
            selectedCharacter.IsChecked = false;
            selectedText.IsEnabled = false;
            selectedCharacter.IsEnabled = true;
            clear();
        }

        private void MenuItem_Checked_1(object sender, RoutedEventArgs e)
        {
            selectedText.IsChecked = false;
            selectedCharacter.IsEnabled = false;
            selectedText.IsEnabled = true;
            clear();
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }

        private void clear()
        {
            myInkCanvas.Strokes.Clear();
            myLabel.Content = "-";
            timer.Stop();
        }


    }
}
