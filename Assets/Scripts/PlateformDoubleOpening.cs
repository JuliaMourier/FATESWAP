using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformDoubleOpening : MonoBehaviour
{
    public Switch switchOpener; //The switch which command the plateform
    public Switch secondSwitch;
    public Transform initialPosition; //Its initial position
    public Transform intermediaryPosition;
    public Transform finalPosition; //Its final position
    private bool isABitOpened = false; //State of the plateform
    private bool isCompletlyOpened = false; //State of the plateform

    // Check the state of the switch and compare with the state of the plateform => Order the changement if needed
    private void Update() { 
        if(secondSwitch.GetSwitchState() && !isCompletlyOpened){ //the second switch is on and it is not already opened => launch the opening
            Open();
        }
        if(!switchOpener.GetSwitchState() && !secondSwitch.GetSwitchState() && (isCompletlyOpened || isABitOpened)){//if both switchs are off and its ius still opened launch the closure
            Close();
        }
        if(switchOpener.GetSwitchState() && !secondSwitch.GetSwitchState() && !isABitOpened){//if its already not a bit opened and only the first swith is on => launch the animation for intermediate opening
            IntermediateOpen();
        }
        
    }

    //Launch the animation of opening after stopping anny other animation
    public void  Open(){
        StopAllCoroutines();
        isCompletlyOpened = true;
        isABitOpened = false;
        StartCoroutine(OpenTransition(this.transform.position,new Vector3(finalPosition.position.x, finalPosition.position.y, this.transform.position.z)));
    }

    //Launch the animation of closure after stopping anny other animation
    public void Close(){
        StopAllCoroutines();
        isABitOpened = false;
        isCompletlyOpened = false;
        StartCoroutine(OpenTransition(this.transform.position,new Vector3(initialPosition.position.x, initialPosition.position.y, this.transform.position.z)));
    }

    //Launch the animation of intermediary opening after stopping anny other animation
    public void IntermediateOpen(){
        StopAllCoroutines();
        isABitOpened = true;
        isCompletlyOpened = false;
        StartCoroutine(OpenTransition(this.transform.position,new Vector3(intermediaryPosition.position.x, intermediaryPosition.position.y, this.transform.position.z)));
    }


    //Set the position of the plateform to a new position given
    private void SetPosition(Vector3 position)
    {   
        //make sure to keep z position
        Vector3 newPosition = new Vector3(position.x, position.y, this.transform.position.z); 
        this.transform.position = newPosition;
    }

    private IEnumerator OpenTransition(Vector3 initPos, Vector3 finalPos){

        float duration = 1f; //Duration of the animation 
        float elapsed = 0.0f;

        // Animate the opening
        while (elapsed < duration)
        {
            //Gives the position between the initial and final position to make a smooth transition
            this.SetPosition(Vector3.Lerp(initPos, finalPos, elapsed / duration)); 
            elapsed += Time.deltaTime; //increment the time
            yield return null; //wait
        }

    }

    
}
