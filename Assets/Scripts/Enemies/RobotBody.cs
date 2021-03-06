using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBody : MonoBehaviour
{

    public Robot robot;
    public AudioSource forPunch;
    public AudioSource againstPunch;

    // If a character enter in collision with the robot body the heroes must take a damage
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.layer == LayerMask.NameToLayer("Characters")){
            if(robot.isAlive){
                FindObjectOfType<GameManager>().HeroesTakeDamage();
                againstPunch.Play();
            }
        }
        //If the robot got hit by a fireball, he must die
        if(robot.ennemi != null && FindObjectOfType<Victoria>() != null){
            if(robot.ennemi == FindObjectOfType<Victoria>().gameObject){
                if(collision.gameObject.layer == LayerMask.NameToLayer("Fireball")){
                    if(robot.isAlive){
                        robot.RobotDie();
                        forPunch.Play();
                    }
                }
            }
        }
        else {
            if(collision.gameObject.layer == LayerMask.NameToLayer("Fireball")){
                if(robot.isAlive && robot.isKillable){
                    robot.RobotDie();
                    forPunch.Play();
                }
            }
        }
    }

}
