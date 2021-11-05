using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Cinematic : MonoBehaviour
{
    public List<ImageOfCinematic> listOfImages;

    private int index = 0;
    
    public void Next(){
        index++;
        if(listOfImages.Count > index){
            listOfImages[index - 1].Deactivate();
            listOfImages[index].Activate();
        }
        else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
