using MahAppsDragablzDemo;
using Microsoft.Extensions.DependencyInjection;

namespace MahMaterialDragablzMashUp
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
