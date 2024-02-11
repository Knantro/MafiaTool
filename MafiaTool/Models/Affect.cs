namespace MafiaTool.Models; 

/// <summary>
/// Делегат, представляющий общую модель метода эффекта на игрока
/// </summary>
public delegate void Affect(Player affectedPlayer, AbilityType type);