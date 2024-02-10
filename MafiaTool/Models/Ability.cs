using System.ComponentModel;
using System.Reflection;

namespace MafiaTool.Models; 

public class Ability {
    public AbilityType Type { get; set; }
    public string Description { get; set; }

    public override string ToString() {
        var abilityName = Type.GetType().GetField(Type.ToString())?.GetCustomAttribute<DescriptionAttribute>()?.Description;
        return $" • {abilityName}";
    }
}