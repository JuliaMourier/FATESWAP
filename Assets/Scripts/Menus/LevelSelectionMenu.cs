using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectionMenu : MonoBehaviour {

    [SerializeField]
    private bool unlocked;

    private Button button;
    public Image unlockImage;
    public GameObject[] stars;

    private void Awake() {
        this.button = GetComponent<Button>();
    }

    private void Update() {
        UpdateLevelStatus();
        UpdateLevelImage();
    }

    // Update the level unlocked status
    // If the previous level has a stars number greater than 0, the current level is unlocked
    private void UpdateLevelStatus() {
        int previousLevelIndex = int.Parse(gameObject.name) - 1;
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
        }
    }

    // Load the scene corresponding to the associated level
    public void PlayLevel(string levelName) {
        SceneManager.LoadScene(levelName);
    }

}
