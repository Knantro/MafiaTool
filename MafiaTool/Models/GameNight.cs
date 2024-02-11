namespace MafiaTool.Models;

/// <summary>
/// Информация об игровой ночи
/// </summary>
public class GameNight {
    /// <summary>
    /// Игроки и наложенные на них эффекты
    /// </summary>
    public Dictionary<Player, List<AbilityType>> PlayersAffects { get; set; }

    /// <summary>
    /// Итоги завершившиеся ночи
    /// </summary>
    public string Summary { get; private set; }

    /// <summary>
    /// Генерирует отчёт ночи исходя из действий игроков
    /// </summary>
    /// <returns>Итог завершившиеся ночи</returns>
    public string GenerateSummary() {
        foreach (var affect in PlayersAffects) { }

        Summary = null;

        return null;
    }
}