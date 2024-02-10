using System.Collections.ObjectModel;
using MafiaTool.Commands;
using MafiaTool.Models;

namespace MafiaTool.ViewModels; 

public class MainMenuVM : ViewModelBase {
    private Random rand = new Random();

    private Player currentPlayer;
    public Player CurrentPlayer {
        get => currentPlayer;
        set => SetField(ref currentPlayer, value);
    }
    
    private Role selectedRole;
    public Role SelectedRole {
        get => selectedRole;
        set => SetField(ref selectedRole, value);
    }
    
    public ObservableCollection<Role> RoleVariations { get; set; } = new();
    public ObservableCollection<Player> GeneratedPlayers { get; set; } = new();
    public Dictionary<Role, int> PlayersRoleCounts { get; set; } = new();
    public RelayCommand GeneratePlayersCommand { get; }
    
    public MainMenuVM() {
        GeneratePlayersCommand = new RelayCommand(_ => GeneratePlayers());
        RoleVariations = new ObservableCollection<Role> {
            new() { Name = "Мирный житель", Priority = 1 },
            new() { Name = "Мафия", Priority = 2, Abilities = new List<Ability> {
                new() { Type = AbilityType.Kill, Description = "УБИВАТЬ УБИВАТЬ УБИВАТЬ" },
                new() { Type = AbilityType.Heal, Description = "ЛЕЧИТЬ ЛЕЧИТЬ ЛЕЧИТЬ" },
                new() { Type = AbilityType.CancelAll, Description = "ШЛЮХА ШЛЮХА ШЛЮХА" },
            }},
            new() { Name = "Доктор", Priority = 3 }
        };
        GeneratedPlayers = new ObservableCollection<Player> {
            new() { Role = new Role { Name = "Мирный житель" }, Name = "Дима", Number = 1 },
            new() { Role = new Role { Name = "Мафия" }, Name = "Миша", Number = 2 },
            new() { Role = new Role { Name = "Доктор" }, Name = "Ваня", Number = 3 },
        };
        PlayersRoleCounts = new Dictionary<Role, int> {
            { new Role { Name = "Мирный житель" }, 4 },
            { new Role { Name = "Мафия" }, 2 },
            { new Role { Name = "Доктор" }, 1 }
        };
    }

    public void GeneratePlayers() {
        var playersCount = PlayersRoleCounts.Sum(x => x.Value);
        for (var i = 1; i <= playersCount; i++) {
            var roleTypesCount = PlayersRoleCounts.Count;
            var role = RoleVariations[rand.Next(roleTypesCount)].RoleType;
            GeneratedPlayers.Add(new Player {
                Number = i,
                Role = RoleVariations.FirstOrDefault(x => x.RoleType == role)
            });
        }
    }
}