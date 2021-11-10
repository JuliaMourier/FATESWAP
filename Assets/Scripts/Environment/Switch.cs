using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public Sprite switchOn; //Sprite of the on switch
    public Sprite switchOff; //Sprite of the off switch

    private bool isSwitchedOn = false; //State of the switch
    private bool available = true; //Availability of the switch

    public Character theOneWhoCanSwitch; //If there is only one person who can switch the attribute is filled, else anyone can switch


    private void OnTriggerStay2D(Collider2D other) {
        Debug.Log("oui");
        //if the character hit the switch
        if(theOneWhoCanSwitch == null){
            if(other.gameObject.layer == LayerMask.NameToLayer("Characters")){ //if anyone can switch the switch no parameter theOneWhoCanSwitch is specified
                if(available){
                    available = false; //Disable the switch
                    SwitchOn(); //Launch the change of state
                }
            }
        }
        else {
            Debug.Log(Input.GetKey(theOneWhoCanSwitch.GetComponent<TempMovement>().switchPressKey));

            if(other.gameObject.Equals(theOneWhoCanSwitch.gameObject)){ //if only one character can switch the switch, check if the collision is dur to this character
                if(available && theOneWhoCanSwitch.isPowerActivate && Input.GetKey(theOneWhoCanSwitch.GetComponent<TempMovement>().switchPressKey)){ //if Henrik is capable of switch the switch
                    available = false; //Disable the switch
                    SwitchOn(); //Launch the change of state
                }
            }
        }
       
    }

    //Switch the sprite of the switch and its state attribute
    private void SwitchOn(){
        if(isSwitchedOn){
            GetComponent<SpriteRenderer>().sprite = switchOff;
        }
        else {
            GetComponent<SpriteRenderer>().sprite = switchOn;
        }
        isSwitchedOn = !isSwitchedOn;
        StartCoroutine(WaitUntilAvailable());
    }

    //Get the state of the switch : true if On, false if Off
    public bool GetSwitchState(){ 
        return isSwitchedOn;
    }

    //Disable the switch for 0.5s
    private IEnumerator WaitUntilAvailable(){ 

        float duration = 0.5f; //Duration of the disability
        float elapsed = 0.0f;

        // Animate the opening
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            yield return null; //do nothing while waiting
        }

        available = true; //The switch is back to available

    }
}
