using MFormatik.Helpers;
using MFormatik.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Diagnostics;
using System.Windows;

namespace MFormatik;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    public static IServiceProvider? ServiceProvider { get; private set; }

    public App()
    {
        ConfigureLogging();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        ServiceProvider = serviceCollection.BuildServiceProvider();

        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();

        if (mainWindow != null)
        {
            Debug.WriteLine("Showing MainWindow");
            mainWindow.Show();
        }
        base.OnStartup(e);
    }

    public static void ConfigureServices(IServiceCollection services)
    {
        AppSettingsManager appSettingsManager = new AppSettingsManager("appsettings.json");
        var connectionString = appSettingsManager.GetSetting("ConnectionStrings", "DefaultConnection");


        services.AddInfrastructure(connectionString!);
        //services.AddApplicationLayer();
        services.AddLogging();


        //services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<MainWindow>();

        //services.AddSingleton<INavigationService>(provider =>
        //{
        //    return new NavigationService(new System.Windows.Controls.Frame());
        //});
        //services.AddSingleton<ISettingNavigationService>(provider =>
        //{
        //    return new SettingNavigationService(new System.Windows.Controls.Frame());
        //});



        // Singlton Pages 
        // services.AddSingleton<DashboardPage>();


        // Add windows
        // services.AddTransient<AddFormationVM>();


        // Adding Navigation ViewModels As singlton (Manualy Rload UI) 
        // services.AddSingleton<FormationVM>();
    }

    #region Error hundling With Serilog
    private void ConfigureLogging()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()  // Set the minimum logging level to Debug
            .WriteTo.Console()   // Log messages to the console
            .WriteTo.File(
                "logs/log-.txt",
                rollingInterval: RollingInterval.Day,  // Create a new log file every day
                retainedFileCountLimit: 10  // Keep only the last 10 log files
            )
            .CreateLogger();

        // Handle unhandled exceptions
        AppDomain.CurrentDomain.UnhandledException += (s, e) =>
        {
            Log.Fatal(e.ExceptionObject as Exception, "An unhandled exception occurred.");
            Log.CloseAndFlush();
        };
    }

    protected override void OnExit(ExitEventArgs e)
    {
        Log.CloseAndFlush();
        base.OnExit(e);
    }
    #endregion
}

