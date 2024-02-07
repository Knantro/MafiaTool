namespace MafiaTool.Models; 

public class Role {
    public RoleType RoleType { get; set; }
    public RoleGeneralFraction Fraction { get; set; }
    public string Name { get; set; }
    public Dictionary<Case, string> CasedNames { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
    public Affect Affect { get; set; }
}