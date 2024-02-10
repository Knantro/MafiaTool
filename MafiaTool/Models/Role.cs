namespace MafiaTool.Models; 

public class Role {
    public RoleType RoleType { get; set; }
    public RoleSide Side { get; set; }
    public string Name { get; set; }
    public Dictionary<Case, string> CasedNames { get; set; }
    public bool CanBeMultiple { get; set; }
    public string Description { get; set; }
    public List<Ability> Abilities { get; set; }
    public int Priority { get; set; }
    public Affect Affect { get; set; }

    public override string ToString() =>
        $"{Priority}. {Name}";
}