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

namespace Entrega1Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly int N_BOXS_ROW = 45;
        readonly int N_BOXS_COL = 45;
        int snakeColumn;
        int snakeRow;

        List<int[]> directions;
        int direction;
        bool hasMove;
        int LEFT = -1;
        int RIGHT = 1;

        List<Ellipse> snake;
        DispatcherTimer timer;
        public MainWindow()
        {
            Loaded += MainWindow_Loaded;
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            snake = new List<Ellipse>();
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };
            timer.Tick += Timer_Tick;

            directions = new List<int[]>();
            directions.Add(new int[] { 0, 1 });
            directions.Add(new int[] { 1, 0 });
            directions.Add(new int[] { 0, -1 });
            directions.Add(new int[] { -1, 0 });

            direction = 0;
            hasMove = true;

            this.KeyDown += keyDown;

            DrawInitialSnake();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            updateSnake();
            hasMove = false;
        }

        #region Serpiente

        private void DrawInitialSnake()
        {
            snakeColumn = 0;
            snakeRow = 10;
            snake.Add(drawNewEllipse(snakeRow, snakeColumn));
            timer.Start();
        }

        private void updateSnake()
        {
            var head = snake.Last();
            snake.RemoveAt(snake.Count - 1);
            snake.Add(head);
            Canvas.SetTop(head, MyCanvas.Width / N_BOXS_COL * snakeRow + 1);
            Canvas.SetLeft(head, MyCanvas.Height / N_BOXS_ROW * snakeColumn + 1);


            snakeRow = snakeRow + directions[direction][0];
            snakeColumn = snakeColumn + directions[direction][1];
        }

        #endregion

        #region Dibujo
        private Ellipse drawNewEllipse(int i, int j)
        {
            var elipse = new Ellipse()
            {
                Width = MyCanvas.Width / N_BOXS_COL - 2,
                Height = MyCanvas.Height / N_BOXS_ROW - 2,
                Stroke = Brushes.Black,
                Fill = Brushes.Orange
            };
            Canvas.SetTop(elipse, MyCanvas.Width / N_BOXS_COL * i + 1);
            Canvas.SetLeft(elipse, MyCanvas.Height / N_BOXS_ROW * j + 1);
            MyCanvas.Children.Add(elipse);
            return elipse;
        }

        #endregion

        #region Movimiento
        private void keyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("Pressed");
            switch (e.Key)
            {
                case Key.A:
                    updateMovement(LEFT);
                    break;
                case Key.D:
                    updateMovement(RIGHT);
                    break;
            }


        }

        private void updateMovement(int m)
        {
            Console.WriteLine("Moved");
            if (hasMove) return;
            direction = Math.Max(0, (direction + m) % 4);
            hasMove = true;
        }

        #endregion




    }
}
