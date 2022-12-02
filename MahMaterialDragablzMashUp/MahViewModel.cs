using System.Collections.ObjectModel;
using System.ComponentModel;
using MahAppsDragablzDemo.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MahAppsDragablzDemo
{
    public class MahViewModel : INotifyPropertyChanged
    {
        IFilesService FilesService { get; }
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private PLCOptions _plcOptions;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<GridRowData> GridData { get; }

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

        public MahViewModel(IFilesService filesService, IConfiguration configuration, ILogger<MahViewModel> logger, IOptions<PLCOptions> plcOptions)
        {
            FilesService= filesService;
            _configuration = configuration;
            _logger = logger;
            _plcOptions = plcOptions.Value;

            var connectionString = _configuration.GetConnectionString("SqlDb");  //从配置文件中读取oeeDb connectionString 
            _logger.LogInformation(connectionString);

            GridData = new ObservableCollection<GridRowData> {
                new GridRowData {
                    IsChecked = false,
                    Text =FilesService.Content,
                    EnumValue = EnumValues.ValueA,
                    IntValue = 4879
                },
                new GridRowData {
                    IsChecked = false,
                    Text = "Venus",
                    EnumValue = EnumValues.ValueB,
                    IntValue = 12104
                },
                new GridRowData {
                    IsChecked = true,
                    Text = "Earth",
                    EnumValue = EnumValues.ValueC,
                    IntValue = 12742
                },
                new GridRowData {
                    IsChecked = true,
                    Text = "Merkur",
                    EnumValue = EnumValues.ValueA,
                    IntValue = 6779
                },
                new GridRowData {
                    IsChecked = true,
                    Text = "Jupiter",
                    EnumValue = EnumValues.ValueD,
                    IntValue = 139822
                },
                new GridRowData {
                    IsChecked = true,
                    Text = "Saturn",
                    EnumValue = EnumValues.ValueC,
                    IntValue = 116464
                },
                new GridRowData {
                    IsChecked = true,
                    Text = "Uranus",
                    EnumValue = EnumValues.ValueC,
                    IntValue = 50724
                },
                new GridRowData {
                    IsChecked = true,
                    Text = "Neptun",
                    EnumValue = EnumValues.ValueB,
                    IntValue = 49244
                },
                new GridRowData {
                    IsChecked = true,
                    Text = "Pluto",
                    EnumValue = EnumValues.ValueA,
                    IntValue = 2370
                }
            };
        }
    }

    public class GridRowData
    {
        public bool IsChecked { get; set; }
        public string? Text { get; set; }
        public EnumValues EnumValue { get; set; }
        public int IntValue { get; set; }
    }

    public enum EnumValues
    {
        ValueA, ValueB, ValueC, ValueD
    }
}
