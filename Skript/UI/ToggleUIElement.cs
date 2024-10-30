using DG.Tweening;
using UnityEngine;

public class ToggleUIElement : MonoBehaviour
{
    [SerializeField] private GameObject playMenu;
    [SerializeField] private GameObject restartMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject endMenu; 
    [SerializeField] private Health health;
    [SerializeField] private EndGame endGame;
    [SerializeField] private Score score;
    [SerializeField] private SceneSwitcher sceneSwitcher;

    private void Start()
    {
        Time.timeScale = 1;

        score.OnMaxScore += EndMenu;
        
        health.OnDeath += RestartMenu;
        restartMenu.SetActive(false);
        pauseMenu.SetActive(false);
        endMenu.SetActive(false);
        playMenu.SetActive(true);
    }

    public void PauseGame()
    {
        playMenu.SetActive(false);
        pauseMenu.SetActive(true);
        
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        pauseMenu.SetActive(false);
        playMenu.SetActive(true);

        Time.timeScale = 1;
    }

    public void RestartGame(int amount)
    {
        pauseMenu.SetActive(false);
        restartMenu.SetActive(false);
        playMenu.SetActive(false);
        
        sceneSwitcher.RestartLevel(amount);
        
        DOTween.KillAll();
    }

    private void RestartMenu()
    {
        Time.timeScale = 0;
        
        playMenu.SetActive(false);
        restartMenu.SetActive(true);
    }

    public void EndMenu()
    {
        playMenu.SetActive(false);
        
        endMenu.SetActive(true);
        endGame.UpdateStarsImage();
        
        Time.timeScale = 0;
    }

    private void OnDestroy()
    {
        health.OnDeath -= RestartMenu;
        
        score.OnMaxScore -= EndMenu;
    }


}
