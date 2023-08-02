using System.Collections.ObjectModel;
using System.ComponentModel;
using ImageStudio.Entities;
using ImageStudio.Services;

namespace MahAppsDragablzDemo
{
    public class MahViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Detail> GridData { get; }

        WallhavenService WallhavenService { get; set; }

        private int _UpDownValue;
        public int UpDownValue
        {
            get
            {
                return _UpDownValue;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Value must be positive");
                }
                if (_UpDownValue != value)
                {
                    _UpDownValue = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UpDownValue)));
                }
            }
        }

        public MahViewModel()
        {
            WallhavenService = new WallhavenService();
            var items = WallhavenService.GetAllDetail().ToList();
            GridData = new ObservableCollection<Detail>(items);
        }
    }
}
