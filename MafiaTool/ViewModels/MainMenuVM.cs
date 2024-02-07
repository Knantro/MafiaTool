using System.Collections.Generic;
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
    public ObservableCollection<Role> RoleVariations { get; set; } = new();
    public ObservableCollection<Player> GeneratedPlayers { get; set; } = new();
    public Dictionary<RoleType, int> PlayersRoleCounts { get; set; } = new();
    public RelayCommand GeneratePlayersCommand { get; }
    
    public MainMenuVM() {
        GeneratePlayersCommand = new RelayCommand(_ => GeneratePlayers());
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