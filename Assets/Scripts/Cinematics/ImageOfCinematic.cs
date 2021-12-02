using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ImageOfCinematic : MonoBehaviour
{
    public Image image;
    public Text story;
    public AudioSource nextSlide;

    private void Update() { //If the players want to skip the cinematique
        if(Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Joystick3Button2)|| Input.GetKeyUp(KeyCode.Joystick1Button2)|| Input.GetKeyUp(KeyCode.Joystick2Button2)){
            nextSlide.Play();
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
