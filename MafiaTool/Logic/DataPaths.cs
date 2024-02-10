using System.IO;
using System.Reflection;

namespace MafiaTool.Logic; 

public static class DataPaths {
    public const string ROLES_FILENAME = "roles.json";
    public const string GAME_HISTORY_FILENAME = "game_history.json";
    public const string LAST_GAME_PLAYERS_FILENAME = "last_game_players.json";

    public static string GetFullPath(string path) =>
        Path.Combine(Assembly.GetExecutingAssembly().Location, "Data", path);
}