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
        readonly int N_BOXS_ROW = 30;
        readonly int N_BOXS_COL = 45;
        private readonly Random rand = new Random();


        int snakeColumn;
        int snakeRow;
        int appleColumn;
        int appleRow;
        Ellipse apple;

        List<int[]> directions;
        int direction;
        bool hasMove;
        int LEFT = -1;
        int RIGHT = 1;

        Ellipse head;
        int score;

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
                Interval = TimeSpan.FromMilliseconds(200)
            };
            timer.Tick += Timer_Tick;

            directions = new List<int[]>();
            directions.Add(new int[] { 0, 1 });
            directions.Add(new int[] { 1, 0 });
            directions.Add(new int[] { 0, -1 });
            directions.Add(new int[] { -1, 0 });

            this.KeyDown += keyDown;

            startGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            updateSnake();
            if (hasDied())
                died();
            else
            {
                if (appleRow == snakeRow && appleColumn == snakeColumn)
                    appleEated();
                hasMove = false;
            }

        }

        #region Serpiente

        private void DrawInitialSnake()
        {
            snakeColumn = 0;
            snakeRow = 10;
            snake.Add(drawNewEllipse(snakeRow, snakeColumn-2));
            snake.Add(drawNewEllipse(snakeRow, snakeColumn-1));
            snake.Add(drawNewEllipse(snakeRow, snakeColumn));
            snake[2].Fill = Brushes.Yellow;
            snake[1].Fill = Brushes.Yellow;
        }

        private void updateSnake()
        {
            if (head != null)
                head.Fill = Brushes.Yellow;
            head = snake.First();
            head.Fill = Brushes.Orange;
            snake.RemoveAt(0);
            snakeRow = snakeRow + directions[direction][0];
            snakeColumn = snakeColumn + directions[direction][1];
            snake.Add(head);
            Canvas.SetTop(head, MyCanvas.Width / N_BOXS_COL * snakeRow + 1);
            Canvas.SetLeft(head, MyCanvas.Height / N_BOXS_ROW * snakeColumn + 1);
        }
        #endregion

        #region Estados de Juego

        private void startGame()
        {
            DrawInitialSnake();
            generateApple();
            direction = 0;
            score = 0;
            hasMove = true;
            timer.Start();
        }

        private void died()
        {
            timer.Stop();
            var results = MessageBox.Show($"Tu puntuación es de {score}. \n ¿Deseas comenzar una nueva partida?",
                "¡Has muerto!", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (results == MessageBoxResult.Yes)
                resetGame();
            else
                this.Close();
        }

        private void resetGame()
        {
            snake.Clear();
            head = null;
            apple = null;
            MyCanvas.Children.Clear();
            startGame();
        }

        private bool hasDied()
        {
            bool dev = snakeRow < 0 || snakeRow >= N_BOXS_ROW;
            dev = dev || snakeColumn < 0 || snakeColumn >= N_BOXS_COL;
            if (!dev)
            {
                int i = 0;
                while (!dev && i < snake.Count - 1)
                {
                    Ellipse e = snake[i];
                    int row = (int)Math.Round((Canvas.GetTop(e)) * N_BOXS_COL / MyCanvas.Width);
                    int col = (int)Math.Round((Canvas.GetLeft(e)) * N_BOXS_ROW / MyCanvas.Height);
                    dev = dev || (snakeColumn == col && snakeRow == row);
                    i++;
                }
            }
            return dev;
        }

        #endregion

        #region Manzanas

        private void generateApple()
        {
            appleRow = rand.Next(N_BOXS_ROW);
            appleColumn = rand.Next(N_BOXS_COL);
            if (apple == null)
            {
                apple = drawNewEllipse(appleRow, appleColumn);
                apple.Fill = Brushes.Red;
            }

            else
            {
                Canvas.SetTop(apple, MyCanvas.Width / N_BOXS_COL * appleRow + 1);
                Canvas.SetLeft(apple, MyCanvas.Height / N_BOXS_ROW * appleColumn + 1);
            }
        }

        private void appleEated()
        {
            score++;
            generateApple();
            snake.Insert(0, drawNewEllipse(snakeRow, snakeColumn));
        }

        #endregion

        #region Dibujo
        private Ellipse drawNewEllipse(int i, int j)
        {
            var elipse = new Ellipse()
            {
                Width = MyCanvas.Width / N_BOXS_COL,
                Height = MyCanvas.Height / N_BOXS_ROW,
                Stroke = Brushes.Black,
                Fill = Brushes.Orange
            };
            Canvas.SetTop(elipse, MyCanvas.Width / N_BOXS_COL * i);
            Canvas.SetLeft(elipse, MyCanvas.Height / N_BOXS_ROW * j);
            MyCanvas.Children.Add(elipse);
            return elipse;
        }

        #endregion

        #region Movimiento
        private void keyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    updateMovement(LEFT);
                    break;
                case Key.Right:
                    updateMovement(RIGHT);
                    break;
            }
        }

        private void updateMovement(int m)
        {
            if (hasMove) return;
            direction = (direction + m) % 4;
            if (direction == -1)
                direction = 3;
            hasMove = true;
        }

        #endregion

    }
}
