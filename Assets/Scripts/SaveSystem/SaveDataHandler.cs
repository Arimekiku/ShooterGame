using UnityEngine;

public class SaveDataHandler : MonoBehaviour
{
    [SerializeField] private string SaveDataFilename;

    public SaveDataInfo DataInfo;
    
    private FileSaver _fileSaver;

    public void Awake()
    {
        _fileSaver = new(SaveDataFilename);
    }
    
    public void LoadGame()
    {
        DataInfo = _fileSaver.Load() ?? new();
    }

    public void SaveGame()
    {
        _fileSaver.Save(DataInfo);
    }
}