using System.Collections.ObjectModel;
using MafiaTool.Commands;
using MafiaTool.Logic;
using MafiaTool.Models;

namespace MafiaTool.ViewModels;

public class MainMenuVM : ViewModelBase {
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private Random rand = new();

    /// <summary>
    /// Список доступных ролей для добавления в игру
    /// </summary>
    public ObservableCollection<Role> RoleVariations { get; set; } = new();

    private Role selectedRole;
    
    /// <summary>
    /// Текущая выбранная роль
    /// </summary>
    public Role SelectedRole {
        get => selectedRole;
        set {
            SetField(ref selectedRole, value);
            OnPropertyChanged(nameof(AddingRoleAvailable));
        }
    }

    private KeyValuePair<Role, int>? selectedRoleCount;
    
    /// <summary>
    /// Текущая выбранная пара (роль - количество игроков с этой ролью)
    /// </summary>
    public KeyValuePair<Role, int>? SelectedRoleCount {
        get => selectedRoleCount;
        set => SetField(ref selectedRoleCount, value);
    }
    
    private bool datingNightIsActivated;
    
    /// <summary>
    /// Включена ли ночь знакомства в предстоящей игре
    /// </summary>
    public bool DatingNightIsActivated {
        get => datingNightIsActivated;
        set => SetField(ref datingNightIsActivated, value);
    }

    /// <summary>
    /// Существует ли последняя сыгранная партия в "Мафию"
    /// </summary>
    public bool HasLastGame => MafiaLogic.LastGamePlayers.Count != 0;

    /// <summary>
    /// Можно ли начать игру
    /// </summary>
    public bool CanStartGame => GeneratedPlayers != null && GeneratedPlayers.Count != 0;

    /// <summary>
    /// Набор ролей с их количеством, включённые в игру
    /// </summary>
    public ObservableCollection<KeyValuePair<Role, int>> PlayersRoleCounts { get; set; } = new();

    /// <summary>
    /// Общее количество игроков в игре
    /// </summary>
    public int PlayersCount => PlayersRoleCounts.Sum(x => x.Value);

    /// <summary>
    /// Есть ли включённые в игру игроки
    /// </summary>
    public bool HasPlayers => PlayersCount > 0;

    /// <summary>
    /// Сгенерированные игроки по ролям
    /// </summary>
    public ObservableCollection<Player> GeneratedPlayers { get; set; } = new();

    /// <summary>
    /// Доступна ли кнопка добавления роли в игру
    /// </summary>
    public bool AddingRoleAvailable => SelectedRole != null && (SelectedRole.CanBeMultiple ||
                                                                PlayersRoleCounts.FirstOrDefault(x => x.Key.RoleType == SelectedRole.RoleType).Key == null);

    /// <summary>
    /// Команда генерации игроков по ролям
    /// </summary>
    public RelayCommand GeneratePlayersCommand => new(_ => GeneratePlayers());
    
    /// <summary>
    /// Команда добавления роли в игру
    /// </summary>
    public RelayCommand AddRoleToGenerationListCommand => new(_ => AddRoleToGenerationList());
    
    /// <summary>
    /// Команда удаления роли из игры
    /// </summary>
    public RelayCommand RemoveRoleFromGenerationCommand => new(_ => RemoveRoleFromGenerationList());
    
    /// <summary>
    /// Команда очистки всех ролей из списка
    /// </summary>
    public RelayCommand ClearPlayersListCommand => new(_ => ClearPlayers());
    
    /// <summary>
    /// Команда восстановления игроков последней партии игры
    /// </summary>
    public RelayCommand RestorePlayersCommand => new(_ => RestorePlayers());
    
    /// <summary>
    /// Команда начала игры
    /// </summary>
    public RelayCommand StartGameCommand => new(_ => StartGame());

    public MainMenuVM() {
        logger.SignedDebug("ctor");
        RoleVariations = new ObservableCollection<Role>(MafiaLogic.Roles.OrderBy(x => x.Priority));
    }

    /// <summary>
    /// Добавляет роль в игру
    /// </summary>
    /// <remarks>
    /// Добавляет по одному игроку с выбранной ролью<br/>
    /// Если игрок с выбранной ролью уже есть в списке, то количество игроков с этой ролью увеличится на один, иначе роль будет добавлена в список ролей для генерации
    /// </remarks>
    public void AddRoleToGenerationList() {
        if (SelectedRole != null) {
            var currentRoleCount = PlayersRoleCounts.FirstOrDefault(x => x.Key.RoleType == SelectedRole.RoleType);
            if (currentRoleCount.Key != null) {
                if (currentRoleCount.Key.CanBeMultiple) {
                    logger.SignedDebug($"Adding player with role {SelectedRole.RoleType}. Remaining players with this role count: {PlayersRoleCounts[PlayersRoleCounts.IndexOf(currentRoleCount)].Value + 1}");
                    PlayersRoleCounts[PlayersRoleCounts.IndexOf(currentRoleCount)] =
                        new KeyValuePair<Role, int>(currentRoleCount.Key, currentRoleCount.Value + 1);
                }
            }
            else {
                logger.SignedDebug($"Adding player with role {SelectedRole.RoleType}. First player with this role");
                PlayersRoleCounts.Add(new KeyValuePair<Role, int>(SelectedRole, 1));
            }

            OnPropertyChanged(nameof(PlayersCount));
            OnPropertyChanged(nameof(HasPlayers));
            OnPropertyChanged(nameof(AddingRoleAvailable));
        }
    }

    /// <summary>
    /// Удаляет роль из игры
    /// </summary>
    /// <remarks>
    /// Удаляет по одному игроку с выбранной ролью<br/>
    /// Если игроков больше одного для выбранной роли, то количество игроков с этой ролью уменьшится на один, иначе роль будет полностью удаления из списка ролей для генерации
    /// </remarks>
    public void RemoveRoleFromGenerationList() {
        if (SelectedRoleCount.HasValue) {
            logger.SignedDebug($"Removing player with role {SelectedRoleCount.Value.Key.RoleType}. {(SelectedRoleCount.Value.Value > 1 ? $"Remaining players with this role count: {SelectedRoleCount.Value.Value - 1}" : "No players with this role existing")}");
            
            var currentRoleCount = PlayersRoleCounts.FirstOrDefault(x => x.Key.RoleType == SelectedRoleCount.Value.Key.RoleType);
            if (currentRoleCount.Key != null) {
                if (currentRoleCount.Value > 1) {
                    SelectedRoleCount = PlayersRoleCounts[PlayersRoleCounts.IndexOf(currentRoleCount)] =
                        new KeyValuePair<Role, int>(currentRoleCount.Key, currentRoleCount.Value - 1);
                }
                else {
                    PlayersRoleCounts.Remove(currentRoleCount);
                }
            }

            OnPropertyChanged(nameof(PlayersCount));
            OnPropertyChanged(nameof(HasPlayers));
            OnPropertyChanged(nameof(AddingRoleAvailable));
        }
    }

    /// <summary>
    /// Удаляет все роли из списка ролей для генерации
    /// </summary>
    public void ClearPlayers() {
        PlayersRoleCounts.Clear();
        
        OnPropertyChanged(nameof(PlayersCount));
        OnPropertyChanged(nameof(HasPlayers));
    }

    /// <summary>
    /// Генерирует игроков согласно списку ролей и их количества
    /// </summary>
    private void GeneratePlayers() {
        logger.SignedInfo($"Generate {PlayersCount} players");
        
        GeneratedPlayers.Clear();
        OnPropertyChanged(nameof(CanStartGame));

        PlayersRoleCounts.SelectMany(x => Enumerable.Repeat(x.Key, x.Value))
                         .Select(x => new Player { Role = x })
                         .Shuffle()
                         .ToList()
                         .ForEach(x => GeneratedPlayers.Add(x));

        for (var i = 0; i < GeneratedPlayers.Count; i++) {
            GeneratedPlayers[i].Number = i + 1;
        }
        
        OnPropertyChanged(nameof(CanStartGame));
    }
    
    /// <summary>
    /// Восстанавливает игроков (с перемешиванием ролей) из предыдущей партии игры для игры с теми же игроками в новой партии
    /// </summary>
    private void RestorePlayers() {
        logger.SignedDebug("Restoring players from the last game");

        GeneratedPlayers = new ObservableCollection<Player>(MafiaLogic.LastGamePlayers);
        OnPropertyChanged(nameof(GeneratedPlayers));

        var roles = GeneratedPlayers.Select(x => x.Role).Shuffle().ToList();

        for (var i = 0; i < GeneratedPlayers.Count; i++) {
            GeneratedPlayers[i].Role = roles[i];
        }
        
        OnPropertyChanged(nameof(CanStartGame));
        
        logger.SignedDebug($"Restored {GeneratedPlayers.Count} players");
    }
    
    /// <summary>
    /// Начинает игру с текущим набором игроков
    /// </summary>
    private void StartGame() {
        logger.SignedInfo($"Start game with {GeneratedPlayers.Count} players now!");
        DataStorage.SaveData(GeneratedPlayers, DataPaths.GetFullPath(DataPaths.LAST_GAME_PLAYERS_FILENAME));
        // MafiaLogic.SetPlayers(GeneratedPlayers);
        // ChangePage<MafiaGamePage>();
    }
}