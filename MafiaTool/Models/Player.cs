namespace MafiaTool.Models; 

/// <summary>
/// Модель игрока
/// </summary>
public class Player {
    /// <summary>
    /// Номер игрока
    /// </summary>
    public int Number { get; set; }
    
    /// <summary>
    /// Имя игрока
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Роль игрока
    /// </summary>
    public Role Role { get; set; }

    /// <summary>
    /// Флаг, обозначающий, жив ли игрок
    /// </summary>
    public bool IsAlive { get; set; } = true;
}