using System.Windows;
using MafiaTool.ViewModels;
using MafiaTool.Views;

namespace MafiaTool;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    public MainWindow() {
        DataContext = App.ServiceProvider.GetService<MainWindowViewModel>();

        InitializeComponent();

        ((MainWindowViewModel)DataContext).CurrentPage = Activator.CreateInstance<MainMenu>();
    }
}