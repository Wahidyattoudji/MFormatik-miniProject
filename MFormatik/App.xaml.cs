using MFormatik.Application;
using MFormatik.Infrastructure;
using MFormatik.Services;
using MFormatik.Services.Abstracts;
using MFormatik.ViewModels;
using MFormatik.ViewModels.OrderVms;
using MFormatik.Views.OrderViews;
using MFormatik.Views.Pages;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Configuration;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;

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

    protected override async void OnStartup(StartupEventArgs e)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        ServiceProvider = serviceCollection.BuildServiceProvider();

        await PreWarmDataBase();

        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow?.Show();
        base.OnStartup(e);
    }

    private async Task PreWarmDataBase()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        var dbContextFactory = new DbContextFactory<MFormatikContext>(connectionString);
        using (var dbContext = dbContextFactory.CreateDbContext())
        {
            await dbContext.Orders.ToListAsync();
        }
    }

    public static void ConfigureServices(IServiceCollection services)
    {
        var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        services.AddInfrastructure(connectionString!);
        services.AddApplicationLayer();

        services.AddSingleton<INavigationService>(provider =>
                 new Services.NavigationService(new Frame())
        );

        services.AddSingleton<MainWindowVM>();
        services.AddSingleton<MainWindow>();

        // Singlton Pages 
        services.AddSingleton<HomePage>();
        services.AddSingleton<OrdersPage>();

        // 
        services.AddTransient<AddOrderVM>();
        services.AddTransient<AddOrderView>();

        services.AddSingleton<OrdersListVM>();

        // Registering ViewModels for OrdersPage
        services.AddSingleton<OrdersPageViewModel>();

        //services
        services.AddTransient<IOrderPrinter, OrderPrinter>();
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

