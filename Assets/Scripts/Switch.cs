using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public Sprite switchOn;
    public Sprite switchOff;

    private bool isSwitchedOn = false;

    private void OnTriggerEnter2D(Collider2D other){
        //if the character hit the door
        if(other.gameObject.layer == LayerMask.NameToLayer("Characters")){
            SwitchOn();
        }
    }

    private void SwitchOn(){
        if(isSwitchedOn){
            GetComponent<SpriteRenderer>().sprite = switchOff;
        }
        else {
            GetComponent<SpriteRenderer>().sprite = switchOn;
        }
        isSwitchedOn = !isSwitchedOn;
    }
}
