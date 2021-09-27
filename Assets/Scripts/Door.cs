using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other){
        //if the character hit the door
        if(other.gameObject.layer == LayerMask.NameToLayer("Characters")){
            OpenDoor();
        }
    }

    protected virtual void OpenDoor(){
        FindObjectOfType<GameManager>().DoorEnter(this);
    }
}
