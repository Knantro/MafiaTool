using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using MafiaTool.Logic;

namespace MafiaTool.ViewModels; 

public class ViewModelBase : INotifyPropertyChanged {
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    protected static readonly MafiaLogic MafiaLogic = App.ServiceProvider.GetService<MafiaLogic>();
    private MainWindowViewModel mainWindowViewModel = App.ServiceProvider.GetService<MainWindowViewModel>();
    
    /// <summary>
    /// Поменять текущую страницу приложения
    /// </summary>
    /// <typeparam name="T">Тип новой страницы, на которую нужно поменять</typeparam>
    protected void ChangePage<T>() where T : UserControl {
        mainWindowViewModel.CurrentPage = Activator.CreateInstance<T>();
    }
    
    #region INPC

    /// <summary>
    /// Событие, что свойство поменяло своё значение
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Обрабатывает событие изменения свойства
    /// </summary>
    /// <param name="propertyName">Имя свойства</param>
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
        logger.SignedTrace($"Called for {propertyName}");
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Устанавливает значение свойства с вызовом события об его изменении
    /// </summary>
    /// <param name="field">Поддерживающее поле свойства</param>
    /// <param name="value">Новое значение свойства</param>
    /// <param name="propertyName">Имя свойства</param>
    /// <typeparam name="T">Тип свойства</typeparam>
    /// <returns>True, если свойство поменяло значение, иначе False</returns>
    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null) {
        logger.SignedTrace($"Called for {propertyName}");
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    #endregion
}