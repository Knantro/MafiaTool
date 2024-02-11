using System.Windows.Controls;
using System.Windows.Input;
using MafiaTool.ViewModels;

namespace MafiaTool.Views;

public partial class MainMenu : UserControl {
    public MainMenu() {
        InitializeComponent();
    }

    private void AddRoleCount_OnMouseDoubleClick(object sender, MouseButtonEventArgs e) {
        ((MainMenuVM)DataContext).AddRoleToGenerationList();
    }

    private void RemoveRoleCount_OnMouseDoubleClick(object sender, MouseButtonEventArgs e) {
        ((MainMenuVM)DataContext).RemoveRoleFromGenerationList();
    }
}