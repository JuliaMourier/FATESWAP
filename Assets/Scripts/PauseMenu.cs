using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    // If ESCAPE is pressed and depending on the GameIsPaused value,
    // it calls either the Resume() method or the Pause() method
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    // Resume the game
    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    // Pause the game
    void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    // Load the settings menu
    public void LoadSettings() {
        // TODO : LOAD THE SETTINGS MENU
        Debug.Log("Load Settings...");
    }

    public void ResetGameIsPausedValue() {
        GameIsPaused = false;
    }
}
