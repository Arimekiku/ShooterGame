using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUIHandler : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Slider _progressBarSlider;
    [SerializeField] private Text _coinsEarned;
    
    [Header("UI Panels")]
    [SerializeField] private GameObject _restartScreen;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _pauseScreen;

    private const int MainMenuIndex = 0;
    private const int LevelIndex = 1;

    public void Init(PlayerBehaviour player, LevelHandler levelHandler, PlayerInput playerInput, PauseInput pauseInput)
    {
        _coinsEarned.text = "0";
        DisableScreen(_restartScreen);
        DisableScreen(_winScreen);
        DisableScreen(_pauseScreen);

        player.OnDeath += EnableRestartScreen;
        levelHandler.OnLevelEnd += EnableWinScreen;
        playerInput.OnEscapePressed += EnablePauseScreen;
        pauseInput.OnEscapePressed += DisablePauseScreen;
    }

    private void EnableRestartScreen()
    {
        _restartScreen.SetActive(true);
    }

    private void EnableWinScreen()
    {
        _winScreen.SetActive(true);
    }

    private void DisableScreen(GameObject screen)
    {
        screen.SetActive(false);
    }

    private void EnablePauseScreen()
    {
        _pauseScreen.SetActive(true);
    }

    public void DisablePauseScreen()
    {
        _pauseScreen.SetActive(false);
    }
    
    public void UpdateSlider(float newValue)
    {
        _progressBarSlider.value = newValue;
    }
    
    public void UpdateCoins(int count)
    {
        _coinsEarned.text = count.ToString();
    }

    public void OnRestartPressed()
    {
        SceneManager.LoadScene(LevelIndex);
    }

    public void OnMainMenuPressed()
    {
        SceneManager.LoadScene(MainMenuIndex);
    }
}