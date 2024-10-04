using System.IO;
using UnityEngine;

public class MapSaver
{
    private string _savePath;

    public MapSaver()
    {
        _savePath = Path.Combine(Application.persistentDataPath, "map.json");
    }

    public void SaveMap(MapData mapData)
    {
        string json = JsonUtility.ToJson(mapData, true);
        File.WriteAllText(_savePath, json);
    }

    public MapData LoadMap()
    {
        if (File.Exists(_savePath))
        {
            string json = File.ReadAllText(_savePath);
            return JsonUtility.FromJson<MapData>(json);
        }
        return null;
    }
}
