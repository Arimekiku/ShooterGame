using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUIHandler : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Slider ProgressBarSlider;
    [SerializeField] private Text CoinsEarned;
    
    [Header("UI Panels")]
    [SerializeField] private DisposableUIPanel RestartScreen;
    [SerializeField] private DisposableUIPanel WinScreen;
    [SerializeField] private DisposableUIPanel PauseScreen;

    public void Init(PlayerBehaviour player, LevelBehaviour levelBehaviour, PlayerInput playerInput, PauseInput pauseInput)
    {
        CoinsEarned.text = "0";
        
        RestartScreen.Dispose();
        WinScreen.Dispose();
        PauseScreen.Dispose();

        player.OnDeath += RestartScreen.Init;
        levelBehaviour.OnLevelEnd += WinScreen.Init;
        playerInput.OnEscapePressed += PauseScreen.Init;
        pauseInput.OnEscapePressed += PauseScreen.Dispose;
    }
    
    public void UpdateSliderBar(float newValue)
    {
        ProgressBarSlider.value = newValue;
    }
    
    public void UpdateCoinTextLabel(int count)
    {
        CoinsEarned.text = count.ToString();
    }

    public void OnRestartPressed()
    {
        SceneManager.LoadScene((int)SceneIndexes.Level);
    }

    public void OnMainMenuPressed()
    {
        SceneManager.LoadScene((int)SceneIndexes.MainMenu);
    }
}