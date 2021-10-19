using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LevelStatus { Start,Play,Pause,Finish }

public class LevelManager : MonoBehaviour
{
    public LevelStatus levelStatus = LevelStatus.Start;
    public Animator playerAnimator;
    public PlayerController playerControllerScript;
    public PlayerPreferences playerPreferencesScript;
    public GameObject startCanvas;
    public GameObject playCanvas;
    public GameObject pauseCanvas;
    public GameObject finishCanvas;
    public GameObject settingsCanvas;
    public GameObject previousCanvas;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeLevelStatusToPlaying()
    {
        levelStatus = LevelStatus.Play;
        SetPreviousCanvas(startCanvas);
        startCanvas.SetActive(false);
        playCanvas.SetActive(true);
        playerAnimator.SetBool("Run", true);
        playerControllerScript.playerStatus = PlayerStatus.Run;
    }

    public void SetPreviousCanvas(GameObject currentCanvas)
    {
        previousCanvas = currentCanvas;
    }

    public void ChangeLevelStatusToPause()
    {
        levelStatus = LevelStatus.Pause;
        Time.timeScale = 0;
        SetPreviousCanvas(playCanvas);
        playCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
    }

    public void ContinueWithGame()
    {
        levelStatus = LevelStatus.Play;
        Time.timeScale = 1;
        SetPreviousCanvas(pauseCanvas);
        pauseCanvas.SetActive(false);
        playCanvas.SetActive(true);
    }

    public void ChangeLevelStatusToFinish()
    {
        levelStatus = LevelStatus.Finish;
        Time.timeScale = 0;
        SetPreviousCanvas(finishCanvas);
        playCanvas.SetActive(false);
        finishCanvas.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        levelStatus = LevelStatus.Start;
        SetPreviousCanvas(finishCanvas);
        finishCanvas.SetActive(false);
        playerAnimator.SetBool("Dead", false);
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Settings()
    {
        previousCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
        playerPreferencesScript.UpdateUI();
    }

    public void SettingsBack()
    {
        settingsCanvas.SetActive(false);
        previousCanvas.SetActive(true);
    }

    public void Apply()
    {
        playerPreferencesScript.SetData();
    }
}
