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
    /// Logika interakcji dla klasy Shield.xaml
    /// </summary>
    public partial class Shield : UserControl
    {
        int _hitPoints;
        int HitPoints 
        { 
            get => _hitPoints;
            set
            {
                _hitPoints = value;
                ContentLabel.Content = value.ToString();
                if( value <= 0)
                    _holder.Children.Remove(this);
            }
        }

        Canvas _holder;

        public Shield()
        {
            InitializeComponent();
        }

        public Shield(Canvas holder, double positionX, double positionY)
        {
            InitializeComponent();
            HitPoints = 25;
            _holder = holder;
            holder.Children.Add(this);
            Canvas.SetTop(this, positionY);
            Canvas.SetLeft(this, positionX);
        }

        public void GetHit()
        {
            HitPoints--;
        }
    }
}
