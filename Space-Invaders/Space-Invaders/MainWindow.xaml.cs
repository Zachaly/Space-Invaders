using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Space_Invaders
{ 
    // move direction of enemies
    public enum Direction
    {
        Left, Right
    }

    public partial class MainWindow : Window
    {
        int _score = 0; // initial score
        public List<List<Invader>> Invaders = new List<List<Invader>>(); // a matrix with enemies
        public List<Shield> Shields = new List<Shield>();
        DispatcherTimer MoveTimer, ShootTimer; // different timers for enemies to move and shoot
        public Player Player { get; set; }
        public List<Invader> ShootingAliens { get; set; } // enemies that are first in their columns will shoot to the player
        public Direction Direction { get; set; } // move direction of enemies 
        const int MoveTime = 200; // miliseconds between enemy moves

        // gamescore, updates the label with recent value
        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                ScoreLabel.Content = $"Score: {_score}";
            }
        }

        // checks if there are any enemies left
        bool WaveClear
        {
            get 
            {
                foreach (var row in Invaders)
                    if (row.Count > 0)
                        return false;

                return true;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            MainCanvas.Children.Remove(StartButton);

            Shields.Add(new Shield(MainCanvas, 120, 700));
            Shields.Add(new Shield(MainCanvas, 400, 700));
            Shields.Add(new Shield(MainCanvas, 680, 700));

            Height = 1000;
            Width = 1000;

            Score = 0; // sets value of the label with score

            Player = new Player();
            Canvas.SetLeft(Player, 450);
            Canvas.SetTop(Player, 850);
            MainCanvas.Children.Add(Player);
            KeyDown += (s, e) => Player.KeyPressed(e.Key);

            // sets healthbar in top right corner and makes it visible
            Canvas.SetTop(HealthBar, 0);
            Canvas.SetRight(HealthBar, 0);
            HealthBar.Visibility = Visibility.Visible;

            GenerateAliens(MoveTime);
        }

        // generates a new wave
        void GenerateAliens(int moveTime)
        {
            // adding invaders to game
            Invaders.Clear();
            for (int i = 0; i < 5; i++)
            {
                Invaders.Add(new List<Invader>());

                for (int j = 0; j < 10; j++)
                {
                    Invaders[i].Add(new Invader(i, j));
                }
            }

            // add lowest row to shooting enemies
            ShootingAliens = new List<Invader>();
            Invaders[4].ForEach(invader => ShootingAliens.Add(invader));

            // enemy shooting
            ShootTimer = new DispatcherTimer();
            ShootTimer.Tick += (_, __) =>
            {
                Invader shooter;
                try
                {
                    // picks enemies that are at least on the X position as player
                    var leftSide = from el in ShootingAliens
                                   where Canvas.GetLeft(el) >= Canvas.GetLeft(Player) - 50
                                   select el;

                    // picks enemy closect to the player
                    shooter = leftSide.First();

                    foreach(var item in leftSide)
                    {
                        if(Canvas.GetLeft(item) < Canvas.GetLeft(shooter))
                            shooter = item;
                    }
                }
                catch (Exception ___) { return; }

                shooter.Shoot();
            };
            ShootTimer.Interval = new TimeSpan(0, 0, 0, 0, 1500);

            // enemy movement
            Direction = Direction.Right;
            MoveTimer = new DispatcherTimer();
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

        // closes game when player dies
        public void GameOver()
        {
            MoveTimer.Stop();
            ShootTimer.Stop();

            try
            {
                var bullets = (from UserControl el in MainCanvas.Children where el is Bullet select el).ToArray();
                foreach(var bullet in bullets)
                    MainCanvas.Children.Remove(bullet);
            }
            catch(Exception _) { }

            MessageBox.Show("Game over");
            App.Current.Shutdown();
        }

        public void CheckWaveClear()
        {
            if (WaveClear)
            {
                MoveTimer.Stop();
                ShootTimer.Stop();
                GenerateAliens(MoveTime);
            }
        }
    }
}
