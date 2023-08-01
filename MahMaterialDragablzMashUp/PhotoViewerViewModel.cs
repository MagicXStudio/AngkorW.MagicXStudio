using System.ComponentModel;
using MahApps.Metro.Controls;

namespace MahMaterialDragablzMashUp
{
    public class PhotoViewerViewModel : INotifyPropertyChanged
    {
        public ICommand MouseMoveCommand { get; }

        public ICommand MouseWheelCommand { get; }

        public ICommand DragOverCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public PhotoViewerViewModel()
        {
            MouseMoveCommand = new AnotherCommandImplementation((o) => MouseMoveHandler(o));
            MouseWheelCommand = new AnotherCommandImplementation(_ => MouseWheelHandler());
            DragOverCommand = new AnotherCommandImplementation(_ => DragOverHandler());
        }

        public Flyout? LeftFlyout { get; set; }

        private void MouseMoveHandler(object obj)
        {

        }

        private void MouseWheelHandler()
        {

        }

        private void DragOverHandler()
        {
        }
    }
}
