using System.IO;
using MafiaTool.Extensions;

namespace MafiaTool.Logic;

/// <summary>
/// Класс-утилита управления сохранением/загрузкой данных
/// </summary>
public static class DataStorage {
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Сохраняет данные в файл в формате json
    /// </summary>
    /// <param name="data">Данные для сохранения</param>
    /// <param name="path">Путь сохранения</param>
    /// <typeparam name="T">Тип данных для сохранения</typeparam>
    public static void SaveData<T>(T data, string path) {
        try {
            logger.SignedInfo($"Save data to file. Path: {path}");
            File.WriteAllText(path, JsonSerializer.Serialize(data));
        }
        catch (Exception e) {
            logger.SignedError(e, "Save data failed");
        }
    }

    /// <summary>
    /// Загружает данные из файла json-формата
    /// </summary>
    /// <param name="path">Путь хранения данных для загрузки</param>
    /// <typeparam name="T">Тип данных для загрузки</typeparam>
    /// <returns>Выгруженные данные, если загрузка прошла успешно, иначе значение по умолчанию</returns>
    public static T LoadData<T>(string path) {
        try {
            logger.SignedInfo($"Load data from file. Path: {path}");
            return JsonSerializer.Deserialize<T>(File.ReadAllText(path));
        }
        catch (Exception e) {
            logger.SignedError(e, "Load data failed");
        }

        return default;
    }
}