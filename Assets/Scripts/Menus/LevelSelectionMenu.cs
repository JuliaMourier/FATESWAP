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
        UpdateLevelImage();
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
