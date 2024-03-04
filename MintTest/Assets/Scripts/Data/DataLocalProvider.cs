using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class DataLocalProvider : IDataProvider
{
    private const string FileName = "PlayerData";
    private const string SaveFileExtesion = ".json";

    private IPersistentData _persistentData;

    public DataLocalProvider(IPersistentData persistentData)
    {
        _persistentData = persistentData;
    }

    private string SavePath => Application.persistentDataPath;
    private string FullPath => Path.Combine(SavePath, $"{FileName}{SaveFileExtesion}");

    public void Save()
    {
        File.WriteAllText(FullPath, JsonConvert.SerializeObject(_persistentData.PlayerData, Formatting.Indented, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        }));
    }

    public bool TryLoad()
    {
        if (IsDataAlreadyExist() == false)
            return false;

        _persistentData.PlayerData = JsonConvert.DeserializeObject<PlayerData>(File.ReadAllText(FullPath));
        return true;
    }

    private bool IsDataAlreadyExist() => File.Exists(FullPath);
}
