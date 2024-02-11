using System.Windows.Input;

namespace MafiaTool.Commands; 

/// <summary>
/// Общий вид команд для связки модели с моделью-представлением
/// </summary>
public class RelayCommand : ICommand {
    private Action<object> execute;
    private Func<object, bool> canExecute;
 
    /// <summary>
    /// Событие изменения возможности активации команды
    /// </summary>
    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
 
    public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
    {
        this.execute = execute;
        this.canExecute = canExecute;
    }
 
    /// <summary>
    /// Может ли команда быть запущена
    /// </summary>
    /// <param name="parameter">Параметр команды</param>
    /// <returns>True, если команда может быть запущена, иначе False</returns>
    public bool CanExecute(object parameter)
    {
        return canExecute == null || canExecute(parameter);
    }
 
    /// <summary>
    /// Запуск команды на исполнение
    /// </summary>
    /// <param name="parameter">Параметр команды</param>
    public void Execute(object parameter)
    {
        execute(parameter);
    }
}