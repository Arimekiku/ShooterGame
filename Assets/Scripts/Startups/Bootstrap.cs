using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private SaveDataHandler SaveGameHandler;

    private void Awake()
    {
        InitSaveSystem();
    }
    
    private void Start()
    {
        SceneManager.LoadScene((int)SceneIndexes.MainMenu);
    }

    private void InitSaveSystem()
    {
        DontDestroyOnLoad(SaveGameHandler);
    }
}