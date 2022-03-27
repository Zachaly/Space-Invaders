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
    /// Logika interakcji dla klasy Invader.xaml
    /// </summary>
    public partial class Invader : UserControl
    {
        MainWindow GameWindow
        {
            get => App.Current.MainWindow as MainWindow;
        }

        PositionHelper positionHelper;

        int rowPosition, columnPosition;

        Canvas _holder;

        public Invader()
        {
            InitializeComponent();
        }

        public Invader(Canvas holder, int row, int column)
        {
            InitializeComponent();
            _holder = holder;
            _holder.Children.Add(this);

            rowPosition = row;
            columnPosition = column;

            positionHelper = new PositionHelper(this);

            positionHelper.PositionX = 80 + (Width + 20) * column;
            positionHelper.PositionY = 80 + (Height + 20) * row;
        }

        void MoveLeft(int length)
        {
            
            if (positionHelper.PositionX <= 80)
            {
                GameWindow.Direction = Direction.Right;
                foreach(var row in GameWindow.Invaders)
                    foreach(var invader in row)
                        invader.MoveDown();
                return;
            }

            positionHelper.PositionX -= length;
        }

        void MoveRight(int lenght)
        {

            if (GameWindow.Width - positionHelper.PositionX <= 80)
            {
                GameWindow.Direction = Direction.Left;
                return;
            }

            positionHelper.PositionX += lenght;
        }

        void MoveDown()
        {
            positionHelper.PositionY += 10;
        }

        public void Move()
        {
            if(GameWindow.Direction == Direction.Left)
                MoveLeft(10);
            else if(GameWindow.Direction == Direction.Right)
                MoveRight(10);
        }

        public void Die()
        {
            _holder.Children.Remove(this);
            GameWindow.Invaders[rowPosition].Remove(this);

            if (GameWindow.ShootingAliens.Contains(this))
            {
                GameWindow.ShootingAliens.Remove(this);
                try
                {
                    GameWindow.ShootingAliens.Add(GameWindow.Invaders[rowPosition - 1][columnPosition]);
                }
                catch (Exception _) { }
            }

            GameWindow.Score += 100;
        }

        public void Shoot()
        {
            _holder.Children.Add(new Bullet(20, 100, this));
        }
    }
}
