using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBody : MonoBehaviour
{
    public Robot robot;
    // If a character enter in collision with the robot body the heroes must take a damage
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.layer == LayerMask.NameToLayer("Characters")){
            if(robot.isAlive){
                FindObjectOfType<GameManager>().HeroesTakeDamage();            
            }
        }
        //If the robot got hit by a fireball, he must die
        if(collision.gameObject.layer == LayerMask.NameToLayer("Fireball")){
            if(robot.isAlive){
                FindObjectOfType<Robot>().RobotDie(robot);            
            }
        }
    }
}
