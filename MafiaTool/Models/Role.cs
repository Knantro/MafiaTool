namespace MafiaTool.Models; 

/// <summary>
/// Модель роли игрока
/// </summary>
public class Role {
    /// <summary>
    /// Тип роли
    /// </summary>
    public RoleType RoleType { get; set; }
    
    /// <summary>
    /// Приоритет роли
    /// </summary>
    /// <remarks>
    /// Чем ниже целочисленное значение приоритета, тем выше приоритет роли и, тем самым, раньше будет ход игрока с этой ролью.<br/>
    /// Исключение с приоритетом 0 - эти игроки ночью не ходят вообще
    /// </remarks>
    public int Priority { get; set; }
    
    /// <summary>
    /// Название роли
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Название роли, просклонённый во всех падежах
    /// </summary>
    public Dictionary<Case, string> CasedNames { get; set; }
    
    /// <summary>
    /// Фракция роли
    /// </summary>
    /// <remarks>
    /// Фракций две - красные (мирные и дружественные к ним) и чёрные (мафия, маньяк, якудза и др.)
    /// </remarks>
    public RoleSide Side { get; set; }
    
    /// <summary>
    /// Может ли быть несколько игроков в одной игре с данной ролью
    /// </summary>
    public bool CanBeMultiple { get; set; }
    
    /// <summary>
    /// Описание роли
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Способности роли
    /// </summary>
    public List<Ability> Abilities { get; set; }

    /// <summary>
    /// Эффект, накладываемый ролью
    /// </summary>
    [JsonIgnore]
    public Affect Affect { get; set; }

    /// <summary>
    /// Возвращает строковое представление роли для отображения на страницах
    /// </summary>
    /// <returns>Строковое представление роли</returns>
    public override string ToString() =>
        $"{Priority}. {Name}";
}