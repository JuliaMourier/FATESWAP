using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ImageOfCinematic : MonoBehaviour
{
    public Image image;
    public Text story;

    private void Update() {
        if(Input.GetKeyUp(KeyCode.Space)){
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
