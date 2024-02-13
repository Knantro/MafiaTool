using System.Windows;
using MafiaTool.Logic;
using MafiaTool.ViewModels;

namespace MafiaTool;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application {
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    
    public static IServiceProvider ServiceProvider { get; private set; }

    public App() {
        logger.SignedInfo("Application starting");
        Dispatcher.UnhandledException += OnDispatcherUnhandledException;
        DispatcherUnhandledException += OnDispatcherUnhandledException;
        AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
        TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
    }

    private void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e) {
        logger.SignedFatal(e.Exception, "Unhandled exception occured! Shutdown an application!");
    }

    private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e) {
        logger.SignedFatal((Exception)e.ExceptionObject, "Unhandled exception occured! Shutdown an application!");
    }

    private void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e) {
        logger.SignedFatal(e.Exception, "Unhandled exception occured! Shutdown an application!");
    }
    
    protected override void OnStartup(StartupEventArgs e) {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        ServiceProvider = serviceCollection.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services) {
        services.AddTransient(typeof(MainWindow));
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<MafiaLogic>();
    }
}