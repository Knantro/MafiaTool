namespace MafiaTool.Models; 

public class Player {
    public int Number { get; set; }
    public string Name { get; set; }
    public Role Role { get; set; }
    public bool IsDead { get; set; }
}