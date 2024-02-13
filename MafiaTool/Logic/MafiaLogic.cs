using MafiaTool.Models;

namespace MafiaTool.Logic; 

public class MafiaLogic {
    /// <summary>
    /// Список игроков последней игры, если таковые имеются
    /// </summary>
    public List<Player> LastGamePlayers { get; private set; } = new();
    
    /// <summary>
    /// Список доступных ролей в игре
    /// </summary>
    public List<Role> Roles { get; private set; } = new();
    
    /// <summary>
    /// Текущая ночь игры
    /// </summary>
    public GameNight CurrentNight { get; private set; }
    
    /// <summary>
    /// Текущие игроки
    /// </summary>
    public List<Player> CurrentPlayers { get; private set; } = new();
    
    /// <summary>
    /// История ночей текущей игры
    /// </summary>
    public List<GameNight> GameNightsHistory { get; private set; } = new();

    public MafiaLogic() {
        RestoreAllData();
    }

    /// <summary>
    /// Загружает все данные (роли, способности, последние игроки) в память
    /// </summary>
    private void RestoreAllData() {
        LastGamePlayers = DataStorage.LoadData<List<Player>>(DataPaths.GetFullPath(DataPaths.LAST_GAME_PLAYERS_FILENAME)) ?? new List<Player>();
        Roles = DataStorage.LoadData<List<Role>>(DataPaths.GetFullPath(DataPaths.ROLES_FILENAME)) ?? new List<Role>();
    }

    /// <summary>
    /// Начинает игру "Мафия"
    /// </summary>
    public void StartGame() {
        
    }

    /// <summary>
    /// Фиксирует игроков в предстоящей игре
    /// </summary>
    /// <param name="players">Список игроков</param>
    public void SetPlayers(List<Player> players) {
        CurrentPlayers = players;
    }

    /// <summary>
    /// Начинает ночь игры
    /// </summary>
    public void StartNight() {
        if (CurrentNight is not null) {
            GameNightsHistory.Add(CurrentNight);
        }

        CurrentNight = new GameNight();
    }

    /// <summary>
    /// Очищает все данные последней игры
    /// </summary>
    public void Clear() {
        CurrentNight = null;
        CurrentPlayers = new List<Player>();
        GameNightsHistory = new List<GameNight>();
    }
}