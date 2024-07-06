namespace MafiaTool.Models; 

public class RoleStatistic {
    public int PlayerNumber { get; set; }
    public Dictionary<RoleType, int> GenerationHistory { get; set; }
}