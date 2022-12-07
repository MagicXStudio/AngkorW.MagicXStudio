using System.Windows.Media;
using MahAppsDragablzDemo;
using MahAppsDragablzDemo.Services;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
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
        private static Logger Logger { get; set; }

        public new static App Current => (App)Application.Current;
        public IServiceProvider Services { get; }

        private static IServiceProvider ConfigureServices()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<IFilesService, FilesService>();
            services.AddSingleton<ISettingsService, SettingsService>();
            services.AddSingleton<IClipboardService, ClipboardService>();

            // Viewmodels
            services.AddTransient<MahViewModel>();
            services.AddTransient<DialogsViewModel>();

            //注册配置
            IConfigurationBuilder cfgBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("plc-settings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}.json", optional: true, reloadOnChange: false)
                ;
            IConfiguration configuration = cfgBuilder.Build();
            services.AddSingleton<IConfiguration>(configuration);

            //http://www.qb5200.com/article/478972.html
            services.AddOptions();
            //实例化一个对应 PlcDevices json 数组对象, 使用了 IConfiguration.Get<T>()
            var plcDeviceSettings = configuration.GetSection("plc").Get<List<PlcOptions>>();
            //或直接通过 service.Configure<T>() 将appsettings 指定 section 放入DI 容器, 这里的T 为 List<PlcDevice>
            services.Configure<List<PlcOptions>>(configuration.GetSection("plc"));

            //创建 logger
            Logger serilogLogger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

            //register logger
            services.AddLogging(builder =>
            {
                ILoggingBuilder p = builder.AddSerilog(logger: serilogLogger, dispose: true);
            });
            Logger=serilogLogger;
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
            Logger.Information("OnStartup->启动软件");
        }

        protected override void OnActivated(EventArgs e) => base.OnActivated(e);

        private void ThemeManager_ThemeChanged(object? sender, ThemeChangedEventArgs e)
        {
            Resources["SecondaryAccentBrush"] = new SolidColorBrush(e.NewTheme.SecondaryMid.Color);
        }
    }
}
