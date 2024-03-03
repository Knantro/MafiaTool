namespace MafiaTool.Models;

/// <summary>
/// Тип способности
/// </summary>
public enum AbilityType {
    /// <summary>
    /// Убийство
    /// </summary>
    Kill,
    
    /// <summary>
    /// Единственное убийство (одно за игру)
    /// </summary>
    SingleKill,

    /// <summary>
    /// Поиск комиссара
    /// </summary>
    CheckCommissar,

    /// <summary>
    /// Лечение
    /// </summary>
    Heal,

    /// <summary>
    /// Поиск чёрного
    /// </summary>
    CheckBlack,

    /// <summary>
    /// Полная отмена
    /// </summary>
    CancelAll,
}