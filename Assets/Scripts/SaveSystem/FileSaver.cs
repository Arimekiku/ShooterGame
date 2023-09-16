using System.IO;
using UnityEngine;

public class FileSaver
{
    private readonly string _saveDataPath;
    private readonly string _saveDataFilename;

    private string DirectoryPath => Path.Combine(_saveDataPath, _saveDataFilename);

    public FileSaver(string newDataFilename)
    {
        _saveDataPath = Application.persistentDataPath;
        _saveDataFilename = newDataFilename;
    }

    public SaveDataInfo Load()
    {
        SaveDataInfo loadData = null;

        if (File.Exists(DirectoryPath))
        {
            using FileStream fs = new(DirectoryPath, FileMode.Open);
            using StreamReader sr = new(fs);

            string readData = sr.ReadToEnd();

            loadData = JsonUtility.FromJson<SaveDataInfo>(readData);
        }

        return loadData;
    }

    public void Save(SaveDataInfo saveData)
    {
        if (Path.GetDirectoryName(DirectoryPath) is null)
            throw new($"Invalid directory path with name {DirectoryPath}.");

        Directory.CreateDirectory(Path.GetDirectoryName(DirectoryPath));
        string serializableData = JsonUtility.ToJson(saveData, true);

        using FileStream fs = new(DirectoryPath, FileMode.Create);
        using StreamWriter sw = new(fs);
        
        sw.Write(serializableData);
    }
}