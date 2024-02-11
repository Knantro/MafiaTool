using System.Collections.ObjectModel;
using MafiaTool.Commands;
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

    /// <summary>
    /// Набор ролей с их количеством, включённые в игру
    /// </summary>
    public ObservableCollection<KeyValuePair<Role, int>> PlayersRoleCounts { get; set; } = new();

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
    /// Команда начала игры
    /// </summary>
    public RelayCommand StartGameCommand => new(_ => StartGame());

    public MainMenuVM() {
        logger.SignedDebug("ctor");
        RoleVariations = new ObservableCollection<Role> {
            new() {
                RoleType = RoleType.Civilian,
                Priority = 0,
                Name = "Мирный житель",
                CasedNames = new Dictionary<Case, string> {
                    { Case.Nominative, "мирный житель" },
                    { Case.Genitive, "мирного жителя" },
                    { Case.Dative, "мирному жителю" },
                    { Case.Accusative, "мирного жителя" },
                    { Case.Instrumental, "мирным жителем" },
                    { Case.Prepositional, "о мирном жителе" }
                },
                Side = RoleSide.Red,
                CanBeMultiple = true,
                Description = "Мирные жители являются обычными игроками, которые не имеют никаких способностей. Они просто спят ночью и голосуют днём наряду с остальными игроками",
                Abilities = null
            },
            new() {
                RoleType = RoleType.Mafia,
                Priority = 1,
                Name = "Мафия",
                CasedNames = new Dictionary<Case, string> {
                    { Case.Nominative, "мафия" },
                    { Case.Genitive, "мафии" },
                    { Case.Dative, "мафии" },
                    { Case.Accusative, "мафию" },
                    { Case.Instrumental, "мафией" },
                    { Case.Prepositional, "о мафии" }
                },
                Side = RoleSide.Black,
                CanBeMultiple = true,
                Description = "Мафия имеет лишь одну цель - убивать. Их задача - убить всех красных (мирных и их друзей) игроков. Мафия коварна, хитра и очень опасна. Они ни перед чем не остановятся ради достижения своих целей",
                Abilities = new List<Ability> {
                    new() {
                        Type = AbilityType.Kill,
                        Name = "Убийство",
                        Description = "Способность убивать других игроков. Выбранная жертва подвергается убийству и, если жертва никем не будет спасена, на следующее утро не проснётся"
                    }
                }
            },
            new() {
                RoleType = RoleType.MafiaDon,
                Priority = 2,
                Name = "Дон мафии",
                CasedNames = new Dictionary<Case, string> {
                    { Case.Nominative, "дон мафии" },
                    { Case.Genitive, "дона мафии" },
                    { Case.Dative, "дону мафии" },
                    { Case.Accusative, "дона мафии" },
                    { Case.Instrumental, "доном мафии" },
                    { Case.Prepositional, "о доне мафии" }
                },
                Side = RoleSide.Black,
                CanBeMultiple = false,
                Description = "Дон мафии поумнее обычной мафии, на то он и дон, чтобы командовать мафией. Дон указывает мафии, кого нужно убить и параллельно с этим пытается найти потенциального врага всей мафиозной шайки - комиссара",
                Abilities = new List<Ability> {
                    new() {
                        Type = AbilityType.Kill,
                        Name = "Убийство",
                        Description = "Способность убивать других игроков. Выбранная жертва подвергается убийству и, если жертва никем не будет спасена, на следующее утро не проснётся"
                    },
                    new() {
                        Type = AbilityType.CheckCommissar,
                        Name = "Поиск комиссара",
                        Description = "Способность искать среди игроков комиссара. Выбранная жертва подвергается убийству и, если жертва никем не будет спасена, на следующее утро не проснётся"
                    }
                }
            },
            new() {
                RoleType = RoleType.Doctor,
                Priority = 3,
                Name = "Доктор",
                CasedNames = new Dictionary<Case, string> {
                    { Case.Nominative, "доктор" },
                    { Case.Genitive, "доктора" },
                    { Case.Dative, "доктору" },
                    { Case.Accusative, "доктора" },
                    { Case.Instrumental, "доктором" },
                    { Case.Prepositional, "о докторе" }
                },
                Side = RoleSide.Red,
                CanBeMultiple = false,
                Description = "Доктор является своеобразным ангелом-хранителем, отчаянно пытающийся спасти потенциальных жертв от их убийства мафией. Он в какой-то степени является альтруистом, ведь себя он может лечить лишь однажды...",
                Abilities = new List<Ability> {
                    new() {
                        Type = AbilityType.Heal,
                        Name = "Лечение",
                        Description = "Способность лечить других игроков. Выбранный игрок будет вылечен и спасён от убийства чёрными игроками. За игру можно лечить себя лишь один раз. Два раза подряд никого лечить нельзя"
                    }
                }
            },
            new() {
                RoleType = RoleType.Commissar,
                Priority = 4,
                Name = "Комиссар",
                CasedNames = new Dictionary<Case, string> {
                    { Case.Nominative, "комиссар" },
                    { Case.Genitive, "комиссара" },
                    { Case.Dative, "комиссару" },
                    { Case.Accusative, "комиссара" },
                    { Case.Instrumental, "комиссаром" },
                    { Case.Prepositional, "о комиссаре" }
                },
                Side = RoleSide.Red,
                CanBeMultiple = false,
                Description = "Комиссар является грозой всех чёрных игроков (мафии и других). Его боятся все нечистые игроки, а для дона мафии является главной целью для устранения. Особый любимец среди мирных жителей",
                Abilities = new List<Ability> {
                    new() {
                        Type = AbilityType.CheckBlack,
                        Name = "Поиск чёрного",
                        Description = "Способность проверять других игроков на принадлежность \"чёрной\" фракции. Выбранный игрок проверяется, является ли он \"чёрным\" игроком (например, мафией или маньяком) или нет. О результатах проверки сообщается после ночи обезличено, чтобы личность игрока знал лишь игрок с данной способностью"
                    }
                }
            },
            new() {
                RoleType = RoleType.Prostitute,
                Priority = 5,
                Name = "Проститутка",
                CasedNames = new Dictionary<Case, string> {
                    { Case.Nominative, "проститутка" },
                    { Case.Genitive, "проститутки" },
                    { Case.Dative, "проститутке" },
                    { Case.Accusative, "проститутку" },
                    { Case.Instrumental, "проституткой" },
                    { Case.Prepositional, "о проститутке" }
                },
                Side = RoleSide.Red,
                CanBeMultiple = false,
                Description = "Проститутка хоть и является сторонником мирных жителей, но сама может поневоле спасти чёрных от проверки их комиссаром или не позволить доктору вылечить потенциальную жертву. Проститутка может перевернуть всю игру верх дном",
                Abilities = new List<Ability> {
                    new() {
                        Type = AbilityType.CancelAll,
                        Name = "Полная отмена",
                        Description = "Способность отменять все эффекты. Выбранный игрок теряет все эффекты, накладываемые данным игроком и эффекты, наложенные на этого игрока. Два раза подряд одного и того же игрока выбирать нельзя"
                    }
                }
            },
        };

        // DataStorage.SaveData(RoleVariations, DataPaths.ROLES_FILENAME);
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
            logger.SignedDebug($"Adding player with role {SelectedRole.RoleType}. {(currentRoleCount.Key != null ? $"Remaining players with this role count: {PlayersRoleCounts[PlayersRoleCounts.IndexOf(currentRoleCount)].Value + 1}" : "First player with this role")}");
            if (currentRoleCount.Key != null) {
                if (currentRoleCount.Key.CanBeMultiple) {
                    PlayersRoleCounts[PlayersRoleCounts.IndexOf(currentRoleCount)] =
                        new KeyValuePair<Role, int>(currentRoleCount.Key, currentRoleCount.Value + 1);
                }
            }
            else {
                PlayersRoleCounts.Add(new KeyValuePair<Role, int>(SelectedRole, 1));
            }

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

            OnPropertyChanged(nameof(AddingRoleAvailable));
        }
    }

    /// <summary>
    /// Генерирует игроков согласно списку ролей и их количества
    /// </summary>
    private void GeneratePlayers() {
        logger.SignedInfo($"Generate {PlayersRoleCounts.Sum(x => x.Value)} players");
        
        GeneratedPlayers.Clear();

        var playersCount = PlayersRoleCounts.Sum(x => x.Value);
        var dictionaryPlayersRoleCounts = PlayersRoleCounts.ToDictionary(k => k.Key, v => v.Value);
        for (var i = 1; i <= playersCount; i++) {
            var roleTypesCount = dictionaryPlayersRoleCounts.Count;
            var roleCount = dictionaryPlayersRoleCounts.ElementAt(rand.Next() % roleTypesCount);
            var role = roleCount.Key;

            if (roleCount.Value == 1) {
                dictionaryPlayersRoleCounts.Remove(role);
            }
            else {
                dictionaryPlayersRoleCounts[role]--;
            }

            GeneratedPlayers.Add(new Player {
                Number = i,
                Role = role
            });
        }
    }
    
    /// <summary>
    /// Начинает игру с текущим набором игроков
    /// </summary>
    private void StartGame() {
        logger.SignedInfo($"Start game with {GeneratedPlayers.Count} now!");
        // MafiaLogic.SetPlayers(GeneratedPlayers);
        // ChangePage<MafiaGamePage>();
    }
}