using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public AudioSource pauseSound;
    public AudioSource unpauseSound;

    // If ESCAPE is pressed and depending on the GameIsPaused value,
    // it calls either the Resume() method or the Pause() method
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused) {
                unpauseSound.Play();
                Resume();
            } else {
                pauseSound.Play();
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

    public void ResetGameIsPausedValue() {
        GameIsPaused = false;
    }
}
