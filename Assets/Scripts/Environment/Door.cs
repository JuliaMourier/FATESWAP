using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Character Lucie;
    public Character Victoria;
    public Character Henrik;
    public Character Fei;

    private int numberCharacterWhoEnteredTheDoor = 0;

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.Equals(Lucie.gameObject)){
            if(Input.GetKey(Lucie.GetComponent<TempMovement>().shootKey)){
                Lucie.gameObject.SetActive(false);
                numberCharacterWhoEnteredTheDoor++;
            }
        }
        if(other.gameObject.Equals(Victoria.gameObject)){
            if(Input.GetKey(Victoria.GetComponent<TempMovement>().shootKey)){
                Victoria.gameObject.SetActive(false);
                numberCharacterWhoEnteredTheDoor++;
            }
        }
        if(other.gameObject.Equals(Henrik.gameObject)){
            if(Input.GetKey(Henrik.GetComponent<TempMovement>().shootKey)){
                Henrik.gameObject.SetActive(false);
                numberCharacterWhoEnteredTheDoor++;
            }
        }
        if(other.gameObject.Equals(Fei.gameObject)){
            if(Input.GetKey(Fei.GetComponent<TempMovement>().shootKey)){
                Fei.gameObject.SetActive(false);
                numberCharacterWhoEnteredTheDoor++;
            }
        }
        //if all characters hit the door
        if(numberCharacterWhoEnteredTheDoor >= 4){
            OnDoorEntered();
        }
    }

    //Open the door
    protected virtual void OnDoorEntered(){ //When the door is entered by all the characters
        FindObjectOfType<GameManager>().WinTheGame(this);
    }
}
