using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // LIVES
    public int lives = 3;
    public Lives firstLive;
    public Lives secondLive;
    public Lives thirdLive;

    // CHARACTERS
    public Character Lucie;

    // COLLECTABLES
    public bool hasKey {get; private set;} = false;

    // TEXTS
    public Text textEndLvl;
    public Text textGameOver;

    // EXIT
    public SpriteRenderer exit;

    public void Update(){
        if(Lucie.gameObject.transform.position.y < -5){
            HeroesTakeDamage();
        }
    }

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

    // Set the display of lives and set GameOver when the heroes don't have any live left
    public void HeroesTakeDamage(){
        if(lives == 3){
            thirdLive.LooseLive();
        }
        else if(lives == 2){
            secondLive.LooseLive();
        }
        else if(lives == 1){
            firstLive.LooseLive();
            GameOver(); //The heroes have lost all their lives
        }
      
        lives--; //decrement the lives
    }

    // Game over : end of the level
    public void GameOver(){
        this.textGameOver.enabled = true;
        this.Lucie.gameObject.SetActive(false);
    }
    
}
