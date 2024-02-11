namespace MafiaTool.Models; 

/// <summary>
/// Модель истории партии игры
/// </summary>
public class GameHistory {
    /// <summary>
    /// Глобальный номер игры
    /// </summary>
    public int GlobalGameNumber { get; set; }
    
    /// <summary>
    /// История игровых ночей
    /// </summary>
    public List<GameNight> Nights { get; set; }
}