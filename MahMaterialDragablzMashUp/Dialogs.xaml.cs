using Microsoft.Extensions.DependencyInjection;

namespace MahMaterialDragablzMashUp
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
