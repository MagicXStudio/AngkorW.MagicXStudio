using System.ComponentModel;
using System.Windows.Media;
using ImageStudio.Services;
using MahApps.Metro.Controls;

namespace MahMaterialDragablzMashUp
{
    public class PhotoViewerViewModel : INotifyPropertyChanged
    {
        public ICommand MouseMoveCommand { get; }

        public ICommand MouseWheelCommand { get; }

        public ICommand DragOverCommand { get; }


        WallhavenService WallhavenService { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public PhotoViewerViewModel()
        {
            WallhavenService = new WallhavenService();
            WallhavenService.Get("Hello");
            MouseMoveCommand = new AnotherCommandImplementation((o) => MouseMoveHandler(o as TransformGroup));
            MouseWheelCommand = new AnotherCommandImplementation((scaleX) => MouseWheelHandler((double)scaleX));
            DragOverCommand = new AnotherCommandImplementation(_ => DragOverHandler());
        }


        public int _AngleX = 20;

        public int AngleX
        {
            get
            {
                return _AngleX;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Value must be positive");
                }
                if (_AngleX != value)
                {
                    _AngleX = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AngleX)));
                }
            }
        }

        public int _AngleY = 20;

        public int AngleY
        {
            get
            {
                return _AngleY;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Value must be positive");
                }
                if (_AngleY != value)
                {
                    _AngleY = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AngleY)));
                }
            }
        }


        public ScaleTransform _Scale = new ScaleTransform(0.1, 0.2, 0.3, 0.3);

        public ScaleTransform Scale
        {
            get
            {
                return _Scale;
            }
            set
            {
                _Scale = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Scale)));
            }
        }

        public Flyout? LeftFlyout { get; set; }

        private void MouseMoveHandler(TransformGroup obj)
        {

        }

        private void MouseWheelHandler(double scaleX)
        {
            AngleX += 2;
            AngleY += 2;
            Scale.ScaleX += 0.2;
        }

        private void DragOverHandler()
        {
        }
    }
}
