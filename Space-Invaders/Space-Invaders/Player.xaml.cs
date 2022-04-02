using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Space_Invaders
{
    public partial class Player : UserControl
    {
        PositionHelper positionHelper;
        DateTime lastShot; // time when player shot last

        // miliseconds from last shot
        double timeFromLastShot
        {
            get => (DateTime.Now - lastShot).TotalMilliseconds;
        }

        public Player()
        {
            InitializeComponent();
            positionHelper = new PositionHelper(this);
        }

        void MoveLeft()
        {
            positionHelper.PositionX -= 15;
        }

        void MoveRight()
        {
            positionHelper.PositionX += 15;
        }

        // player shoots a bullet if its not too early for next shot
        void Shot()
        {
            if(timeFromLastShot < 1000)
            {
                return;
            }
            positionHelper.MainCanvas.Children.Add(new Bullet(-15, 25, this));
            lastShot = DateTime.Now;
        }

        public void KeyPressed(Key key)
        {
            if (key == Key.Left)
                MoveLeft();
            else if (key == Key.Right)
                MoveRight();
            else if (key == Key.Up)
                Shot();
        }

        // Removes one heart
        public void GetHit()
        {
            (App.Current.MainWindow as MainWindow).HealthBar.RemoveHP();
        }
    }
}
