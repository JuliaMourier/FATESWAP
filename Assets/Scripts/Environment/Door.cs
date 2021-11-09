using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        //if all characters hit the door
        if(other.contactCount >= 5){
            OnDoorEntered();
        }
    }

    //Open the door
    protected virtual void OnDoorEntered(){
        FindObjectOfType<GameManager>().WinTheGame(this);
    }
}
