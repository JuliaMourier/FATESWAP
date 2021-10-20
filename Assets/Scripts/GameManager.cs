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
    public Character Victoria;
    public Character Fei;
    public Character Henrik;

    // COLLECTABLES
    public bool hasKey {get; private set;} = false;

    // TEXTS
    public Text textEndLvl;
    public Text textGameOver;

    // EXIT
    public SpriteRenderer exit;

    //Check if the heroes are out of the map
    public void Update(){
        if(CharactersOutOfMap()){
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
           DeactivateCharacters();
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
        DeactivateCharacters(); 
    }
    
    //Test if one of the character is out of the map
    private bool CharactersOutOfMap(){
        if(Lucie.transform.position.y < -5 || Victoria.transform.position.y < -5 || Fei.transform.position.y < -5 || Henrik.transform.position.y < -5 ){
            return true;
        }
        return false;
    }

    private void DeactivateCharacters(){
        this.Lucie.gameObject.SetActive(false);
        this.Fei.gameObject.SetActive(false);
        this.Victoria.gameObject.SetActive(false);
        this.Henrik.gameObject.SetActive(false);
    }
}
