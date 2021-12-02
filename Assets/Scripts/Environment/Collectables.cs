using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    //when a character collect an object Collectable
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Characters")){
            CollectableFound();
        }
    }  

    // Call gameManager function
    protected virtual void CollectableFound(){
        FindObjectOfType<GameManager>().CollectableFound(this); // this will only disable the collectable (for now)
    }
    
}
