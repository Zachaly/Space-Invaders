using System.Windows.Controls;

namespace Space_Invaders
{
    public partial class Shield : UserControl
    {
        int _hitPoints;
        // how many times shield can be hit, changes the value visible in game, removes shield when value is  0
        int HitPoints 
        { 
            get => _hitPoints;
            set
            {
                _hitPoints = value;
                ContentLabel.Content = value.ToString();
                if (value <= 0)
                {
                    _holder.Children.Remove(this);
                    (App.Current.MainWindow as MainWindow).Shields.Remove(this);
                }
            }
        }

        Canvas _holder; // canvas containing the shield

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

        // removes a hitpoint
        public void GetHit()
        {
            HitPoints--;
        }
    }
}
