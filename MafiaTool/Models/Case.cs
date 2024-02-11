namespace MafiaTool.Models; 

/// <summary>
/// Падежи
/// </summary>
public enum Case {
    /// <summary>
    /// Именительный
    /// </summary>
    Nominative,
    
    /// <summary>
    /// Родительный
    /// </summary>
    Genitive,
    
    /// <summary>
    /// Дательный
    /// </summary>
    Dative,
    
    /// <summary>
    /// Винительный
    /// </summary>
    Accusative,
    
    /// <summary>
    /// Творительный
    /// </summary>
    Instrumental,
    
    /// <summary>
    /// Предложный
    /// </summary>
    Prepositional
}