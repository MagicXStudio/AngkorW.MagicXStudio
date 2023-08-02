using System.ComponentModel;
using System.Windows.Media;
using ImageStudio.Services;

namespace ImageStudio
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
            var items = WallhavenService.GetAllDetail().ToList();
            MouseMoveCommand = new AnotherCommandImplementation((o) => MouseMoveHandler(o as TransformGroup));
            MouseWheelCommand = new AnotherCommandImplementation((scaleX) => MouseWheelHandler((double)scaleX));
            DragOverCommand = new AnotherCommandImplementation(_ => DragOverHandler());
        }

        public ScaleTransform _Scale = new ScaleTransform(0.1, 0.2, 0.3, 0.3);
        /// <summary>
        /// 缩放
        /// </summary>
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

        public SkewTransform _Skew = new SkewTransform(0.1, 0.2, 0.3, 0.3);
        /// <summary>
        /// 倾斜
        /// </summary>
        public SkewTransform Skew
        {
            get
            {
                return _Skew;
            }
            set
            {
                _Skew = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Skew)));
            }
        }


        public RotateTransform _Rotate = new RotateTransform(15);
        /// <summary>
        /// 旋转
        /// </summary>
        public RotateTransform Rotate
        {
            get
            {
                return _Rotate;
            }
            set
            {
                _Rotate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Rotate)));
            }
        }


        public TranslateTransform _Translate = new TranslateTransform(5, 18);
        /// <summary>
        /// 平移
        /// </summary>
        public TranslateTransform Translate
        {
            get
            {
                return _Translate;
            }
            set
            {
                _Translate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Translate)));
            }
        }

        private void MouseMoveHandler(TransformGroup obj)
        {

        }

        private void MouseWheelHandler(double scaleX)
        {
            Scale.ScaleX += 0.2;
            Translate.X += 10;
            Translate.Y += 10;
            Skew.AngleX += 5;
            Skew.AngleY += 5;
        }

        private void DragOverHandler()
        {
        }
    }
}
