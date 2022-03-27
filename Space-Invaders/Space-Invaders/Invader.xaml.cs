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

        double PositionX 
        { 
            get => Canvas.GetLeft(this); 
            set => Canvas.SetLeft(this, value); 
        }
        double PositionY 
        { 
            get => Canvas.GetTop(this);
            set => Canvas.SetTop(this, value);
        }

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

            PositionX = 80 + (Width + 20) * column;
            PositionY = 80 + (Height + 20) * row;
        }

        void MoveLeft(int length)
        {
            
            if (PositionX <= 80)
            {
                GameWindow.Direction = Direction.Right;
                foreach(var row in GameWindow.Invaders)
                    foreach(var invader in row)
                        invader.MoveDown();
                return;
            }
            
            PositionX -= length;
        }

        void MoveRight(int lenght)
        {

            if (GameWindow.Width - PositionX <= 80)
            {
                GameWindow.Direction = Direction.Left;
                return;
            }

            PositionX += lenght;
        }

        void MoveDown()
        {
            PositionY += 10;
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

            GameWindow.Score += 100;
        }
    }
}
