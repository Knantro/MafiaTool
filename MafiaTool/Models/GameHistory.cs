namespace MafiaTool.Models; 

public class GameHistory {
    public int GlobalGameNumber { get; set; }
    public List<GameNight> Nights { get; set; }
}