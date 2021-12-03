using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ImageOfCinematic : MonoBehaviour
{
    public Image image;
    public Text story;
    public AudioSource nextSlide;

    private void Update() { //If the players want to skip the cinematique
        if(Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Joystick3Button2)|| Input.GetKeyUp(KeyCode.Joystick1Button2)|| Input.GetKeyUp(KeyCode.Joystick2Button2)){
            // If the active scene is not a cinematic, we play a transition audio
            if (!SceneManager.GetActiveScene().name.Contains("Cinematic")) {
                nextSlide.Play();
            }
            FindObjectOfType<Cinematic>().Next();
        }
    }

    public void Activate(){
        this.gameObject.SetActive(true);
    }

    public void Deactivate(){
        this.gameObject.SetActive(false);
    }
}
