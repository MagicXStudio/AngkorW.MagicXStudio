using MaterialDesign3Demo.Domain;

namespace MaterialDesign3Demo
{
    public partial class Chips
    {
        public Chips()
        {
            InitializeComponent();
            this.DataContext = new ButtonsViewModel();
            MainWindow.Snackbar.MessageQueue?.Enqueue("Chip delete clicked!");
        }

        public int Value => 1 - 234 - 567 - 890;
    }
}
