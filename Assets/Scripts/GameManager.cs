using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int lives = 3;
    public bool hasKey {get; private set;} = false;
    public Text textEndLvl;
    public SpriteRenderer exit;

    // when a character find a collectable
    public void CollectableFound(Collectables collectable){
        hasKey = true;
        collectable.gameObject.SetActive(false);
        //Set the door's Color to black
        exit.color = Color.black;
        //TODO
    }

    public void DoorEnter(Door door){
        if(hasKey){
            // display the text "Well done"
           this.textEndLvl.enabled = true;
        }
    }
    //TODO
}
