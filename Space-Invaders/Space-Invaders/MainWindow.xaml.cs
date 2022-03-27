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
        DispatcherTimer MoveTimer;
        Player player;

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
            Invaders.Clear();
            for(int i = 0; i < 5; i++)
            {
                Invaders.Add(new List<Invader>());

                for(int j = 0; j < 10; j++)
                {
                    Invaders[i].Add(new Invader(MainCanvas, i, j));
                }
            }

            Shields.Add(new Shield(MainCanvas, 100, 700));
            Shields.Add(new Shield(MainCanvas, 400, 700));
            Shields.Add(new Shield(MainCanvas, 700, 700));

            Height = 1000;
            Width = 1000;

            Score = 0;

            player = new Player();
            Canvas.SetLeft(player, 450);
            Canvas.SetTop(player, 850);
            MainCanvas.Children.Add(player);
            KeyDown += (s, e) => player.KeyPressed(e.Key);

            Direction = Direction.Right;
            MoveTimer = new DispatcherTimer();
            MoveTimer.Tick += (_, __) =>
            {
                foreach (var row in Invaders)
                    foreach (var invader in row)
                        invader.Move();
            };
            MoveTimer.Interval = new TimeSpan(0, 0, 0, 0, 200);

            MoveTimer.Start();
        }
    }
}
