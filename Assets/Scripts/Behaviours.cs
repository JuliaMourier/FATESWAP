using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]


public abstract class Behaviours : MonoBehaviour
{ // class base of any behaviour 
    public Character character {get; private set;}

    private void Awake(){
        this.character = GetComponent<Character>();
        this.enabled = false;
    }

    public virtual void Enable(){
        this.enabled = true;
    }

    public virtual void Disable(){
        this.enabled = false;
    }
}
