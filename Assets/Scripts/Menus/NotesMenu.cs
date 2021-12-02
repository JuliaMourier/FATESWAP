using UnityEngine;
using UnityEngine.UI;

public class NotesMenu : MonoBehaviour
{
    
    public int noteNumber;
    public Image cadenas;
    private Button button;

    void Awake() {
        this.button = GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("Note" + noteNumber.ToString()) == "true") {
            button.interactable = true;
            cadenas.gameObject.SetActive(false);
        } else {
            button.interactable = false;
            cadenas.gameObject.SetActive(true);
        }  
    }
}
