using System;
using System.Linq;
using System.Windows.Controls;

namespace Space_Invaders
{
    public partial class Invader : UserControl
    {
        // simple reference to main window
        MainWindow GameWindow
        {
            get => App.Current.MainWindow as MainWindow;
        }

        PositionHelper positionHelper;

        int rowPosition, columnPosition; // position od enemy in matrix

        public Invader()
        {
            InitializeComponent();
        }

        public Invader(int row, int column)
        {
            InitializeComponent();
            positionHelper.MainCanvas.Children.Add(this);

            rowPosition = row;
            columnPosition = column;

            positionHelper = new PositionHelper(this);

            positionHelper.PositionX = 80 + (Width + 20) * column;
            positionHelper.PositionY = 80 + (Height + 20) * row;
        }

        // moves enemy left, moves down and changes direction when its too far left
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

        // moves enemy right, changes direction when its too far right
        void MoveRight(int lenght)
        {
            if (GameWindow.Width - positionHelper.PositionX <= 80)
            {
                GameWindow.Direction = Direction.Left;
                return;
            }

            positionHelper.PositionX += lenght;
        }

        // moves enemy down, ends the game if its too close to the player
        void MoveDown()
        {
            positionHelper.PositionY += 10;
            if (positionHelper.PositionY >= Canvas.GetTop(GameWindow.Player))
                GameWindow.GameOver();
        }

        // makes enemy move
        public void Move()
        {
            if(GameWindow.Direction == Direction.Left)
                MoveLeft(10);
            else if(GameWindow.Direction == Direction.Right)
                MoveRight(10);
        }

        // enemy is removed from the game, score is increased
        public void Die()
        {
            positionHelper.MainCanvas.Children.Remove(this);
            GameWindow.Invaders[rowPosition].Remove(this);

            if (GameWindow.ShootingAliens.Contains(this))
            {
                GameWindow.ShootingAliens.Remove(this);
                try
                {
                    GetNewShootingAlien(rowPosition);
                }
                catch (Exception _) { }
            }

            GameWindow.Score += 100;
            GameWindow.CheckWaveClear();
        }

        // enemy shoots
        public void Shoot()
        {
            positionHelper.MainCanvas.Children.Add(new Bullet(20, 200, this));
        }

        // gets a new alien that will shoot in the same row
        void GetNewShootingAlien(int row)
        {
            if (row < 1)
                return;

            try
            {
                var alien = (from el in GameWindow.Invaders[row - 1] where el.columnPosition == columnPosition select el).First();
                GameWindow.ShootingAliens.Add(alien);
            }
            catch(Exception _) 
            {
                GetNewShootingAlien(row - 1);
            }
        }
    }
}
