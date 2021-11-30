using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Cinematic : MonoBehaviour
{
    public List<ImageOfCinematic> listOfImages;

    private int index = 0;
    
    public void Next(){ //Display the next image (or scene) of the cinematic
        index++;
        if(listOfImages.Count > index){ //If there is a next image
            listOfImages[index - 1].Deactivate(); //Deactivate previous image
            listOfImages[index].Activate(); //Activate the next one
        }
        else {
            Debug.Log(SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //LOad next scene
        }
    }
}
