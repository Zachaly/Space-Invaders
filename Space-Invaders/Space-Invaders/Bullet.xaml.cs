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
    /// Logika interakcji dla klasy Bullet.xaml
    /// </summary>
    public partial class Bullet : UserControl
    {
        MainWindow GameWindow
        {
            get => App.Current.MainWindow as MainWindow;
        }

        int _moveLength;
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

            positionHelper.PositionX = Canvas.GetLeft(shooter) + shooter.ActualWidth/2;

            if (shooter is Player)
            {
                Width = 5;
                positionHelper.PositionY = Canvas.GetTop(shooter);
                isPlayer = true;
            }
            else
                positionHelper.PositionY = Canvas.GetBottom(shooter);


            moveTimer = new DispatcherTimer();
            moveTimer.Interval = new TimeSpan(0, 0, 0, 0, moveSpeed);
            moveTimer.Tick += (_, __) =>
            {
                Move();
            };
            moveTimer.Start();
        }

        public void Move()
        {
            positionHelper.PositionY += _moveLength;
            if(isPlayer)
                PlayerBulletDetectCollisions();

            if(positionHelper.PositionY < 0)
            {
                moveTimer.Stop();
                positionHelper.MainCanvas.Children.Remove(this);
            }
        }

        void PlayerBulletDetectCollisions()
        {
            var rect1 = new Rect(positionHelper.PositionX, positionHelper.PositionY, ActualWidth, ActualHeight);
            foreach(var row in GameWindow.Invaders)
                foreach(var invader in row)
                {
                    rect1 = new Rect(positionHelper.PositionX, positionHelper.PositionY, ActualWidth, ActualHeight);
                    var rect2 = new Rect(Canvas.GetLeft(invader), Canvas.GetTop(invader), invader.ActualWidth, invader.ActualHeight);
                    rect1.Intersect(rect2);
                    if (!rect1.IsEmpty)
                    {
                        positionHelper.MainCanvas.Children.Remove(this);
                        moveTimer.Stop();
                        invader.Die();
                        return;
                    }
                }

            foreach (var shield in GameWindow.Shields)
            {
                rect1 = new Rect(positionHelper.PositionX, positionHelper.PositionY, ActualWidth, ActualHeight);
                var rect2 = new Rect(Canvas.GetLeft(shield), Canvas.GetTop(shield), shield.ActualWidth, shield.ActualHeight);
                rect1.Intersect(rect2);

                if (!rect1.IsEmpty) 
                { 
                    positionHelper.MainCanvas.Children.Remove(this);
                    moveTimer.Stop();
                    return;
                }
            }
        }
    }
}
