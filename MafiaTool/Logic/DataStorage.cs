using System.IO;
using System.Text.Encodings.Web;

namespace MafiaTool.Logic;

/// <summary>
/// Класс-утилита управления сохранением/загрузкой данных
/// </summary>
public static class DataStorage {
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Настройки серилазиации
    /// </summary>
    private static readonly JsonSerializerOptions JsonSerializerOptions = new() {
        Converters = { new JsonStringEnumConverter() },
        WriteIndented = true,
        PropertyNamingPolicy = null,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        PropertyNameCaseInsensitive = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true
    };

    /// <summary>
    /// Сохраняет данные в файл в формате json
    /// </summary>
    /// <param name="data">Данные для сохранения</param>
    /// <param name="path">Путь сохранения</param>
    /// <typeparam name="T">Тип данных для сохранения</typeparam>
    public static void SaveData<T>(T data, string path) {
        try {
            logger.SignedInfo($"Save data to file. Path: {path}");
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, JsonSerializer.Serialize(data, JsonSerializerOptions));
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
            
            if (!File.Exists(path)) {
                logger.SignedWarn(message: "No data available to load");
                return default;
            }
            
            return JsonSerializer.Deserialize<T>(File.ReadAllText(path), JsonSerializerOptions);
        }
        catch (Exception e) {
            logger.SignedError(e, "Load data failed");
        }

        return default;
    }
}