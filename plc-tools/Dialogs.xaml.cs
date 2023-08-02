using Microsoft.Extensions.DependencyInjection;

namespace ImageStudio
{
    /// <summary>
    /// Interaction logic for Dialogs.xaml
    /// </summary>
    public partial class Dialogs : UserControl
    {
        public Dialogs()
        {
            InitializeComponent();
            this.DataContext = App.Current.Services.GetService<DialogsViewModel>();
        }
    }
}
