using System.Windows.Controls;

namespace Space_Invaders
{
    // a struct helping with getting position of the given element on canvas
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
