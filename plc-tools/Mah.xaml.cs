using ImageStudio;
using Microsoft.Extensions.DependencyInjection;

namespace ImageStudio
{
    /// <summary>
    /// Interaction logic for MahApps.xaml
    /// </summary>
    public partial class Mah : UserControl
    {
        public Mah()
        {
            InitializeComponent();
            this.DataContext = App.Current.Services.GetService<MahViewModel>();
        }
    }
}
