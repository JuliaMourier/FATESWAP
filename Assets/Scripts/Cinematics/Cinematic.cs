using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}
