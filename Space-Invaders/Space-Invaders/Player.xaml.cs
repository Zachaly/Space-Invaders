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
        double PositionX
        {
            get => Canvas.GetLeft(this);
            set => Canvas.SetLeft(this, value);
        }

        public Player()
        {
            InitializeComponent();
        }

        public void MoveLeft()
        {
            PositionX -= 10;
        }

        public void MoveRight()
        {
            PositionX += 10;
        }

        public void KeyPressed(Key key)
        {
            if (key == Key.Left)
                MoveLeft();
            else if (key == Key.Right)
                MoveRight();
        }
    }
}
