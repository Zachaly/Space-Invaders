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
    /// Logika interakcji dla klasy HealthBar.xaml
    /// </summary>
    public partial class HealthBar : UserControl
    {
        int currentHP = 0;

        Label currentHeart
        {
            get => MainGrid.Children.Cast<Label>().First(element => Grid.GetColumn(element) == currentHP);
        }

        public HealthBar()
        {
            InitializeComponent();
        }

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
