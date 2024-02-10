namespace MafiaTool.Models; 

public class GameNight {
    public Dictionary<Player, List<AbilityType>> PlayersAffects { get; set; }
    public string Summary { get; private set; }

    public string GenerateSummary() {
        foreach (var affect in PlayersAffects) {
            
        }

        Summary = null;
        
        return null;
    }
}