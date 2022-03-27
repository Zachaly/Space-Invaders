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
    public struct PositionHelper
    {
        UserControl holder;
        public double PositionX
        {
            get => Canvas.GetLeft(holder);
            set => Canvas.SetLeft(holder, value);
        }
        public double PositionY
        {
            get => Canvas.GetTop(holder);
            set => Canvas.SetTop(holder, value);
        }

        public Canvas MainCanvas
        {
            get => (App.Current.MainWindow as MainWindow).MainCanvas;
        }

        public PositionHelper(UserControl holder)
        {
            this.holder = holder;
        }
    }
}
