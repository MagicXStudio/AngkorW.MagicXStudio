Microsoft.Extensions.DependencyInjection, 依赖注入的类库
Microsoft.Extensions.Configuration, 包含IConfiguration接口 和 Configuration类
Microsoft.Extensions.Configuration.Json, 为 IConfiguration 增加了读取 Json 文件功能,
Microsoft.Extensions.Hosting,  提供 Host 静态类,  有能力从 appsettings.{env.EnvironmentName}.json 加载相应 env  的设定值,  并将设定值用于IConfiguration/ILoggerFactory中, 同时增加 Console/EventSourceLogger 等 logger. 仅适用于 Asp.Net core 和 Console 类应用
Microsoft.Extensions.Logging,  包含 ILogger 和 ILoggerFactory 接口
Serilog.Extensions.Logging, 为DI 容器提供 AddSerilog() 方法.
Serilog.Sinks.File, 提供 Serilog rolling logger
Serilog.Sinks.Console, 增加 serilog console logger
Serilog.Settings.Configuration, 允许在 appsetting.json  配置 Serilog, 顶层节点要求是 Serilog. 
Serilog.Enrichers.Thread 和 Serilog.Enrichers.Environment库,  为输出日志文本增加 Thread和 env 信息

补充库:
Microsoft.Extensions.Options.ConfigurationExtensions库,  为DI容器增加了从配置文件中实例化对象的能力, 即  serviceCollection.Configure<TOptions>(IConfiguration)
Microsoft.Extensions.Options库,  提供以强类型的方式读取configuration文件, 这是.Net中首选的读取configuration文件方式.
