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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<List<Invader>> Invaders = new List<List<Invader>>();
        List<Shield> shields = new List<Shield>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            MainCanvas.Children.Clear();
            Invaders.Clear();
            for(int i = 0; i < 5; i++)
            {
                Invaders.Add(new List<Invader>());

                for(int j = 0; j < 10; j++)
                {
                    Invaders[i].Add(new Invader(MainCanvas, i, j));
                }
            }

            shields.Add(new Shield(MainCanvas, 100, 700));
            shields.Add(new Shield(MainCanvas, 400, 700));
            shields.Add(new Shield(MainCanvas, 700, 700));

            Height = 800;
            Width = 850;
        }
    }
}
