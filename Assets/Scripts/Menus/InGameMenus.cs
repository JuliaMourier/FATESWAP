using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenus : MonoBehaviour {

    // Restart the current level
    public void RestartLevel() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Load the main menu scene
    public void LoadMainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    // Load the next scene
    public void LoadNextScene() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
