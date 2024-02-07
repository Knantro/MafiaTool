namespace MafiaTool.Models; 

public class GameNight {
    public Dictionary<Player, List<AffectType>> PlayersAffects { get; set; }
    public string Summary { get; private set; }

    public string GenerateSummary() {
        foreach (var affect in PlayersAffects) {
            
        }

        Summary = null;
        
        return null;
    }
}