using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlateform : MonoBehaviour
{
    public Switch switchOpener; //The switch which command the plateform
    public Transform initialPosition; //Its initial position
    public Transform finalPosition; //Its final position
    private bool isOpened = false; //State of the plateform

    // Check the state of the switch and compare with the state of the plateform => Order the changement if needed
    private void Update() { 
        if(switchOpener.GetSwitchState() && !isOpened){ //if the switch tell to open and its not already openen => launch the opening
            Open();
        }
        if(!switchOpener.GetSwitchState() && isOpened){//if the switch tell to clode and its not already closed => launch the closure
            Close();
        }
    }

    //Launch the animation of opening after stopping anny other animation
    public void  Open(){
        StopAllCoroutines();
        isOpened = true;
        StartCoroutine(OpenTransition(new Vector3(initialPosition.position.x, initialPosition.position.y, this.transform.position.z),new Vector3(finalPosition.position.x, finalPosition.position.y, this.transform.position.z)));
    }

    //Launch the animation of closure after stopping anny other animation
    public void Close(){
        StopAllCoroutines();
        isOpened = false;
        StartCoroutine(OpenTransition(new Vector3(finalPosition.position.x, finalPosition.position.y, this.transform.position.z),new Vector3(initialPosition.position.x, initialPosition.position.y, this.transform.position.z)));
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
