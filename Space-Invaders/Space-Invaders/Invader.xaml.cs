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

        public void MoveLeft(int length)
        {
            PositionX += length;
        }

        public void MoveRight(int lenght)
        {
            PositionX -= lenght;
        }

        public void MoveDown()
        {
            PositionY += 10;
        }

        public void Die()
        {
            _holder.Children.Remove(this);
            MainWindow.Invaders[rowPosition].Remove(this);
        }
    }
}
