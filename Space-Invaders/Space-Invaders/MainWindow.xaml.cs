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

namespace Space_Invaders
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public enum Direction
    {
        Left, Right
    }

    public partial class MainWindow : Window
    {
        int _score = 0;
        public List<List<Invader>> Invaders = new List<List<Invader>>();
        public List<Shield> Shields = new List<Shield>();
        DispatcherTimer MoveTimer, ShootTimer;
        public Player Player;
        public List<Invader> ShootingAliens;

        public Direction Direction { get; set; }

        public int Speed
        {
            get => MoveTimer.Interval.Milliseconds;
            set => MoveTimer.Interval = new TimeSpan(0, 0, 0, 0, value);
        }

        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                ScoreLabel.Content = $"Score: {_score}";
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            MainCanvas.Children.Remove(StartButton);

            Shields.Add(new Shield(MainCanvas, 100, 700));
            Shields.Add(new Shield(MainCanvas, 400, 700));
            Shields.Add(new Shield(MainCanvas, 700, 700));

            Height = 1000;
            Width = 1000;

            Score = 0;

            Player = new Player();
            Canvas.SetLeft(Player, 450);
            Canvas.SetTop(Player, 850);
            MainCanvas.Children.Add(Player);
            KeyDown += (s, e) => Player.KeyPressed(e.Key);

            Canvas.SetTop(HealthBar, 0);
            Canvas.SetRight(HealthBar, 0);
            HealthBar.Visibility = Visibility.Visible;

            MoveTimer = new DispatcherTimer();
            GenerateAliens(200);
        }

        void GenerateAliens(int moveTime)
        {
            Invaders.Clear();
            for (int i = 0; i < 5; i++)
            {
                Invaders.Add(new List<Invader>());

                for (int j = 0; j < 10; j++)
                {
                    Invaders[i].Add(new Invader(MainCanvas, i, j));
                }
            }

            ShootingAliens = new List<Invader>();
            Invaders[4].ForEach(invader => ShootingAliens.Add(invader));
            ShootTimer = new DispatcherTimer();
            ShootTimer.Tick += (_, __) =>
            {
                Invader shooter;
                //Random random = new Random();
                //shooter = ShootingAliens[random.Next(Invaders.Count)];
                try
                {
                    var leftSide = from el in ShootingAliens
                                   where Canvas.GetLeft(el) >= Canvas.GetLeft(Player) - 50
                                   select el;

                    shooter = leftSide.First();
                }
                catch (Exception ___) { return; }

                shooter.Shoot();
            };
            ShootTimer.Interval = new TimeSpan(0, 0, 0, 0, 750);

            Direction = Direction.Right;
            MoveTimer.Tick += (_, __) =>
            {
                foreach (var row in Invaders)
                    foreach (var invader in row)
                        invader.Move();
            };
            MoveTimer.Interval = new TimeSpan(0, 0, 0, 0, moveTime);

            MoveTimer.Start();
            ShootTimer.Start();
        }

        public void GameOver()
        {
            MoveTimer.Stop();
            ShootTimer?.Stop();
            MessageBox.Show("Game over");
        }
    }
}
