using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using ImageStudio.Entities;
using ImageStudio.Services;
using ImageStudio.ViewModels;

namespace ImageStudio
{
    public class WallhavenViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Detail> GridData { get; }

        WallhavenService WallhavenService { get; set; }


        public ICommand SaveCommand { get; }


        private async Task Save(object obj)
        {
            JsonResult result = await WallhavenAPI.Search(new SearchParameter());

            foreach (Detail item in result.Data)
            {
                await WallhavenService.AddDetail(item);
            }
        }


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

        public WallhavenViewModel()
        {
            SaveCommand = new AnotherCommandImplementation(async (o) => await Save(o));
            WallhavenService = new WallhavenService();
            var items = WallhavenService.GetAllDetail().ToList();
            GridData = new ObservableCollection<Detail>(items);
        }
    }
}
