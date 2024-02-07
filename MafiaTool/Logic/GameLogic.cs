using MafiaTool.Models;

namespace MafiaTool.Logic; 

public class GameLogic {
    public GameNight CurrentNight { get; private set; }
    public List<Player> CurrentPlayers { get; private set; } = new();
    public List<GameNight> GameNightsHistory { get; private set; } = new();

    public GameLogic() {
        
    }

    public void StartNight() {
        if (CurrentNight is not null) {
            GameNightsHistory.Add(CurrentNight);
        }

        CurrentNight = new GameNight();
    }

    public void Clear() {
        CurrentNight = null;
        CurrentPlayers = new List<Player>();
        GameNightsHistory = new List<GameNight>();
    }
}