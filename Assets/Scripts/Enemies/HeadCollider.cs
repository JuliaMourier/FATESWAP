using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollider : MonoBehaviour
{
    //Get the parent
    public Robot robot;
    //If the ennemi character jump on the head of a robot, the robot must die
    private void OnCollisionEnter2D(Collision2D collision){
        if(robot.ennemi != null){
            if(collision.gameObject.Equals(robot.ennemi)){ //The robot dies only if its ennemi kills it
                robot.isAlive = false;
                robot.RobotDie();
            }
        }
        else {
            if(collision.gameObject.layer == LayerMask.NameToLayer("Characters")){ //The robot dies only if its ennemi kills it
                robot.isAlive = false;
                robot.RobotDie();
            }
        }
        
    }
}
