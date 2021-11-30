using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectionMenu : MonoBehaviour {

    [SerializeField]
    private bool unlocked;

    private int currentLevelIndex;
    private Button button;
    public Image unlockImage;
    public GameObject[] stars;
    public Sprite starSprite;

    private void Awake() {
        this.currentLevelIndex = int.Parse(gameObject.name);
        this.button = GetComponent<Button>();
    }

    private void Update() {
        // The state of the button 0 will never change, that's why we call the update methods only for higher indexes
        if (currentLevelIndex > 0) {
            UpdateLevelStatus();
            UpdateLevelImage();
        }
    }

    // When the new game button is pressed, we reset all the stars
    // Only the level 1 will be unlocked after this button is pressed
    public void NewGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("StarsLevel0", 3);
    }

    public void NewGameSolo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 14);
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("StarsLevel0", 3);
    }

    // Update the level unlocked status
    // If the previous level has a stars number greater than 0, the current level is unlocked
    private void UpdateLevelStatus() {
        int previousLevelIndex = currentLevelIndex - 1;
        if (PlayerPrefs.GetInt("StarsLevel" + previousLevelIndex) > 0) {
            unlocked = true;
        }
    }

    // Update the level UI corresponding to the level state (unlocked or not)
    private void UpdateLevelImage() {
        if (!unlocked) {
            button.interactable = false;
            unlockImage.gameObject.SetActive(true);
            foreach (var star in stars) {
                star.SetActive(false);                
            }
        } else {
            button.interactable = true;
            unlockImage.gameObject.SetActive(false);
            foreach (var star in stars) {
                star.SetActive(true);                
            }
            for (int i = 0; i < PlayerPrefs.GetInt("StarsLevel" + currentLevelIndex); i++) {
                stars[i].gameObject.GetComponent<Image>().sprite = starSprite;
            }
        }
    }

    // Load the scene corresponding to the associated level
    public void PlayLevel(string levelName) {
        // PlayerPrefs.SetInt("StarsLevel" + currentLevelIndex, 2);
        SceneManager.LoadScene(levelName);
    }

}
