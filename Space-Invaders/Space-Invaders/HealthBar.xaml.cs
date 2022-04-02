using System.Linq;
using System.Windows.Controls;

namespace Space_Invaders
{
    public partial class HealthBar : UserControl
    {
        int currentHP = 0; // column heart that will be removed

        Label currentHeart
        {
            get => MainGrid.Children.Cast<Label>().First(element => Grid.GetColumn(element) == currentHP);
        }

        public HealthBar()
        {
            InitializeComponent();
        }

        //Removes one heart, player dies if he has none
        public void RemoveHP()
        {
            if (currentHP > 2)
                return;

            MainGrid.Children.Remove(currentHeart);
            currentHP++;
            if (currentHP > 2)
            {
                (App.Current.MainWindow as MainWindow).GameOver();
            }
        }
    }
}
