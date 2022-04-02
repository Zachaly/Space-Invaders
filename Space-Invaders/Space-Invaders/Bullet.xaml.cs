using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Space_Invaders
{
    public partial class Bullet : UserControl
    {
        // simple reference to main window
        MainWindow GameWindow
        {
            get => App.Current.MainWindow as MainWindow;
        }

        int _moveLength; // how many pixels the bullet moves
        PositionHelper positionHelper;
        DispatcherTimer moveTimer;
        bool isPlayer = false;

        public Bullet()
        {
            InitializeComponent();
        }

        public Bullet(int moveLength, int moveSpeed, UserControl shooter)
        {
            InitializeComponent();
            _moveLength = moveLength;
            positionHelper = new PositionHelper(this);

            // bullet spawns on the middle of shooter
            positionHelper.PositionX = Canvas.GetLeft(shooter) + shooter.ActualWidth / 2;

            // if shooter is player the bullet is more narrow, and starts on top of the shooter
            if (shooter is Player)
            {
                Width = 5;
                positionHelper.PositionY = Canvas.GetTop(shooter);
                isPlayer = true;
            }
            // of shooter is enemy bullet starts on bottom of shooter
            else
                positionHelper.PositionY = Canvas.GetTop(shooter) + shooter.Height;

            moveTimer = new DispatcherTimer();
            moveTimer.Interval = new TimeSpan(0, 0, 0, 0, moveSpeed);
            moveTimer.Tick += (_, __) =>
            {
                Move();
            };
            moveTimer.Start();
        }

        // moves bullet, checks of it collides with something or goes beyond the window
        public void Move()
        {
            positionHelper.PositionY += _moveLength;
            if(isPlayer)
                PlayerBulletDetectCollisions();
            else
                InvaderBulletDetectCollisions();

            if(positionHelper.PositionY < 0 || positionHelper.PositionY > Canvas.GetBottom(GameWindow.Player))
            {
                DeleteBullet();
            }
        }

        void PlayerBulletDetectCollisions()
        {
            var rect1 = new Rect(positionHelper.PositionX, positionHelper.PositionY, ActualWidth, ActualHeight);
            // checks if bullet collides with enemy
            foreach(var row in GameWindow.Invaders)
                foreach(var invader in row)
                {
                    rect1 = new Rect(positionHelper.PositionX, positionHelper.PositionY, ActualWidth, ActualHeight);
                    var rect2 = new Rect(Canvas.GetLeft(invader), Canvas.GetTop(invader), invader.ActualWidth, invader.ActualHeight);
                    rect1.Intersect(rect2);
                    if (!rect1.IsEmpty)
                    {
                        DeleteBullet();
                        invader.Die();
                        return;
                    }
                }

            // checks if the bullet collides with shield
            foreach (var shield in GameWindow.Shields)
            {
                rect1 = new Rect(positionHelper.PositionX, positionHelper.PositionY, ActualWidth, ActualHeight);
                var rect2 = new Rect(Canvas.GetLeft(shield), Canvas.GetTop(shield), shield.ActualWidth, shield.ActualHeight);
                rect1.Intersect(rect2);

                if (!rect1.IsEmpty) 
                {
                    DeleteBullet();
                    return;
                }
            }
        }

        void InvaderBulletDetectCollisions()
        {
            // checks if the bullet collides with the player
            var rect1 = new Rect(positionHelper.PositionX, positionHelper.PositionY, ActualWidth, ActualHeight);
            var rect2 = new Rect(Canvas.GetLeft(GameWindow.Player), Canvas.GetTop(GameWindow.Player),
                GameWindow.Player.ActualWidth, GameWindow.Player.ActualHeight);

            rect1.Intersect(rect2);

            if (!rect1.IsEmpty)
            {
                GameWindow.Player.GetHit();
                DeleteBullet();
                return;
            }

            // checks if the bullet collides with shield
            foreach (var shield in GameWindow.Shields)
            {
                rect1 = new Rect(positionHelper.PositionX, positionHelper.PositionY, ActualWidth, ActualHeight);
                rect2 = new Rect(Canvas.GetLeft(shield), Canvas.GetTop(shield), shield.ActualWidth, shield.ActualHeight);
                rect1.Intersect(rect2);

                if (!rect1.IsEmpty)
                {
                    shield.GetHit();
                    DeleteBullet();
                    return;
                }
            }
        }

        // removes bullet from the game
        void DeleteBullet()
        {
            positionHelper.MainCanvas.Children.Remove(this);
            moveTimer.Stop();
        }
    }
}
