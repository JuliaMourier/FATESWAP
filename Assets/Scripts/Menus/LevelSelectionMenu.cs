using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectionMenu : MonoBehaviour {

    public bool isSoloMode;
    private string STARS_LEVEL = "StarsLevel";

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
        if (isSoloMode) {
            STARS_LEVEL = "StarsLevelSolo";
        }
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
        // Reset all the stars for the selected mode (coop or solo)
        for (int i = 0; i < 5; i++) {
            PlayerPrefs.DeleteKey(STARS_LEVEL + i);
        }
        if (isSoloMode) {
            SceneManager.LoadScene("IntroCinematic_Solo");
            PlayerPrefs.SetInt("StarsLevelSolo0", 3);
        } else {
            SceneManager.LoadScene("IntroCinematic");
            PlayerPrefs.SetInt("StarsLevel0", 3);
        }
    }

    // Update the level unlocked status
    // If the previous level has a stars number greater than 0, the current level is unlocked
    private void UpdateLevelStatus() {
        int previousLevelIndex = currentLevelIndex - 1;
        if (PlayerPrefs.GetInt(STARS_LEVEL + previousLevelIndex) > 0) {
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
            for (int i = 0; i < PlayerPrefs.GetInt(STARS_LEVEL + currentLevelIndex); i++) {
                stars[i].gameObject.GetComponent<Image>().sprite = starSprite;
            }
        }
    }

    // Load the scene corresponding to the associated level
    public void PlayLevel(string levelName) {
        SceneManager.LoadScene(levelName);
    }

}
