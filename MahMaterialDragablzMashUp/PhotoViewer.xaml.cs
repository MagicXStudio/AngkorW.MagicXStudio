using MahMaterialDragablzMashUp;

namespace MahAppsDragablzDemo
{
    /// <summary>
    /// PhotoViewer.xaml 的交互逻辑
    /// </summary>
    public partial class PhotoViewer : UserControl
    {
        public PhotoViewer()
        {
            InitializeComponent();
            DataContext = new PhotoViewerViewModel();
        }
    }
}
