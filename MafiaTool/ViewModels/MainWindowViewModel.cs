using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using MafiaTool.Views;
using NLog;

namespace MafiaTool.ViewModels; 

public class MainWindowViewModel : INotifyPropertyChanged {

    private Logger logger = LogManager.GetCurrentClassLogger();
    private UserControl currentPage;
    public UserControl CurrentPage {
        get => currentPage;
        set => SetField(ref currentPage, value);
    }

    public MainWindowViewModel() {
        CurrentPage = Activator.CreateInstance<MainMenu>();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null) {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}