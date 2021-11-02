using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{  
    public Sprite dead; //grey heart
    public Sprite alive; //red heart
    public SpriteRenderer heartRenderer; 

    // Set the sprite of the lost live
    public void LooseLive(){
        this.heartRenderer.sprite = dead;
    }

    //Reset the sprite to the red heart
    public void Reset(){
        this.heartRenderer.sprite = alive;
    }
}
