using System.Windows.Controls;
using System.Windows.Input;

namespace MafiaTool.Views; 

public partial class MainMenu : UserControl {
    public MainMenu() {
        InitializeComponent();
    }

    private void IntegerUpDown_OnPreviewKeyDown(object sender, KeyEventArgs args) {
        switch (args.Key) {
            case Key.D0: case Key.NumPad0: 
            case Key.D1: case Key.NumPad1: 
            case Key.D2: case Key.NumPad2: 
            case Key.D3: case Key.NumPad3: 
            case Key.D4: case Key.NumPad4: 
            case Key.D5: case Key.NumPad5: 
            case Key.D6: case Key.NumPad6: 
            case Key.D7: case Key.NumPad7: 
            case Key.D8: case Key.NumPad8: 
            case Key.D9: case Key.NumPad9:
            case Key.Back: case Key.Delete:
                args.Handled = false; 
                break;
            default:
                args.Handled = true;
                break;
        }
    }
}