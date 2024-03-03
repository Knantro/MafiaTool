namespace MafiaTool.Models; 

/// <summary>
/// Тип роли
/// </summary>
public enum RoleType {
    /// <summary>
    /// Мирный житель
    /// </summary>
    Civilian,
    
    /// <summary>
    /// Мафия
    /// </summary>
    Mafia,
    
    /// <summary>
    /// Маньяк
    /// </summary>
    Maniac,
    
    /// <summary>
    /// Проститутка
    /// </summary>
    Prostitute,
    
    /// <summary>
    /// Дон мафии
    /// </summary>
    MafiaDon,
    
    /// <summary>
    /// Доктор
    /// </summary>
    Doctor,
    
    /// <summary>
    /// Комиссар
    /// </summary>
    Commissar,
    
    /// <summary>
    /// Шериф
    /// </summary>
    Sheriff,
}