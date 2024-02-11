namespace MafiaTool.Models; 

/// <summary>
/// Модель способности персонажа
/// </summary>
public class Ability {
    /// <summary>
    /// Тип способности
    /// </summary>
    public AbilityType Type { get; set; }
    
    /// <summary>
    /// Название способности
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Описание способности
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Возвращает строковое представление модели для отображения на странице
    /// </summary>
    /// <returns>Строковое представление модели</returns>
    public override string ToString() => $" • {Name}";
}