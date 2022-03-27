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

namespace Space_Invaders
{
    /// <summary>
    /// Logika interakcji dla klasy Player.xaml
    /// </summary>
    public partial class Player : UserControl
    {
        PositionHelper positionHelper;
        DateTime lastShot;

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
            positionHelper.PositionX -= 10;
        }

        void MoveRight()
        {
            positionHelper.PositionX += 10;
        }

        void Shot()
        {
            if(timeFromLastShot < 1000)
            {
                return;
            }
            positionHelper.MainCanvas.Children.Add(new Bullet(-15, 50, this));
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

        public void GetHit()
        {
            (App.Current.MainWindow as MainWindow).HealthBar.RemoveHP();
        }
    }
}
