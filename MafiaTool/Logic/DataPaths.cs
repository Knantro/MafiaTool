using System.Diagnostics;
using System.IO;

namespace MafiaTool.Logic; 

/// <summary>
/// Пути к хранилищу информации по типам
/// </summary>
public static class DataPaths {
    public const string ROLES_FILENAME = "roles.json";
    public const string GAME_HISTORY_FILENAME = "game_history.json";
    public const string LAST_GAME_PLAYERS_FILENAME = "last_game_players.json";

    /// <summary>
    /// Получить полный путь до файла с хранилищем информации определённого типа
    /// </summary>
    /// <param name="fileName">Имя файла (рекомендуется брать из полей <see cref="DataPaths"/> типа)</param>
    /// <returns>Абсолютный путь до файла</returns>
    public static string GetFullPath(string fileName) =>
        Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), "Data", fileName);
}