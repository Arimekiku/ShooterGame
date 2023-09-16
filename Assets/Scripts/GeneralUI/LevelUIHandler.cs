using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUIHandler : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Slider _progressBarSlider;
    [SerializeField] private Text _coinsEarned;
    
    [Header("UI Panels")]
    [SerializeField] private DisposableUIPanel _restartScreen;
    [SerializeField] private DisposableUIPanel _winScreen;
    [SerializeField] private DisposableUIPanel _pauseScreen;

    public void Init(PlayerBehaviour player, LevelBehaviour levelBehaviour, PlayerInput playerInput, PauseInput pauseInput)
    {
        _coinsEarned.text = "0";
        
        _restartScreen.Dispose();
        _winScreen.Dispose();
        _pauseScreen.Dispose();

        player.OnDeath += _restartScreen.Init;
        levelBehaviour.OnLevelEnd += _winScreen.Init;
        playerInput.OnEscapePressed += _pauseScreen.Init;
        pauseInput.OnEscapePressed += _pauseScreen.Dispose;
    }
    
    public void UpdateSliderBar(float newValue)
    {
        _progressBarSlider.value = newValue;
    }
    
    public void UpdateCoinTextLabel(int count)
    {
        _coinsEarned.text = count.ToString();
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