using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBody : MonoBehaviour
{
    // If a character enter in collision with the robot body the heroes must take a damage
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.layer == LayerMask.NameToLayer("Characters")){
            FindObjectOfType<GameManager>().HeroesTakeDamage();            
        }
    }
}
