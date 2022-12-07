using System.ComponentModel;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahAppsDragablzDemo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MahMaterialDragablzMashUp
{
    public class DialogsViewModel : NotifyPropertyChanged
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private PlcOptions _plcOptions;

        public ICommand ShowInputDialogCommand { get; }

        public ICommand ShowProgressDialogCommand { get; }

        public ICommand ShowLeftFlyoutCommand { get; }

        private ResourceDictionary DialogDictionary = new ResourceDictionary()
        {
            Source = new Uri("pack://application:,,,/MaterialDesignThemes.MahApps;component/Themes/MaterialDesignTheme.MahApps.Dialogs.xaml")
        };

        public event PropertyChangedEventHandler PropertyChanged;

        public DialogsViewModel(IConfiguration configuration, ILogger<DialogsViewModel> logger, IOptions<PlcOptions> plcOptions)
        {
            _configuration = configuration;
            _logger = logger;
            _plcOptions = plcOptions.Value;

            ShowInputDialogCommand = new AnotherCommandImplementation(_ => InputDialog());
            ShowProgressDialogCommand = new AnotherCommandImplementation(_ => ProgressDialog());
            ShowLeftFlyoutCommand = new AnotherCommandImplementation(_ => ShowLeftFlyout());
        }

        public Flyout? LeftFlyout { get; set; }

        private void InputDialog()
        {
            var metroDialogSettings = new MetroDialogSettings
            {
                CustomResourceDictionary = DialogDictionary,
                NegativeButtonText = "取消"
            };

            DialogCoordinator.Instance.ShowInputAsync(this, "PLC", "登录口令", metroDialogSettings);
        }

        private async void ProgressDialog()
        {
            var metroDialogSettings = new MetroDialogSettings
            {
                CustomResourceDictionary = DialogDictionary,
                NegativeButtonText = "取消"
            };

            var controller = await DialogCoordinator.Instance.ShowProgressAsync(this, "PLC", "网络诊断", true, metroDialogSettings);
            controller.SetIndeterminate();
            await Task.Delay(3000);
            await controller.CloseAsync();
        }

        private void ShowLeftFlyout()
        {
            ((MainWindow)Application.Current.MainWindow).LeftFlyout.IsOpen = !((MainWindow)Application.Current.MainWindow).LeftFlyout.IsOpen;
        }
    }
}
