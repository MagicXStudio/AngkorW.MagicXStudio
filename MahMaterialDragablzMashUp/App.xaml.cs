using System.Windows.Media;
using MahAppsDragablzDemo;
using MahAppsDragablzDemo.Services;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using ShowMeTheXAML;

namespace MahMaterialDragablzMashUp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();
            InitializeComponent();
        }

        public new static App Current => (App)Application.Current;
        public IServiceProvider Services { get; }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IFilesService, FilesService>();
            services.AddSingleton<ISettingsService, SettingsService>();
            services.AddSingleton<IClipboardService, ClipboardService>();

            // Viewmodels
            services.AddTransient<MahViewModel>();

            return services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            XamlDisplay.Init();
            base.OnStartup(e);

            //Add/Update brush used by Dragablz when the theme changes
            //Solution for https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit/issues/2349
            PaletteHelper helper = new PaletteHelper();
            if (helper.GetThemeManager() is { } themeManager)
            {
                themeManager.ThemeChanged += ThemeManager_ThemeChanged;
            }
        }

        private void ThemeManager_ThemeChanged(object? sender, ThemeChangedEventArgs e)
        {
            Resources["SecondaryAccentBrush"] = new SolidColorBrush(e.NewTheme.SecondaryMid.Color);
        }
    }
}
