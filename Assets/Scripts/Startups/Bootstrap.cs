using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private SaveDataHandler SaveGameHandler;

    private void Awake()
    {
        DontDestroyOnLoad(SaveGameHandler);
    }

    private void Start()
    {
        SceneManager.LoadScene((int)SceneIndexes.MainMenu);
    }
}