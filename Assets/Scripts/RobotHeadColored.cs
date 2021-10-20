using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotHeadColored : MonoBehaviour
{
    //precise the ennemi
    public GameObject ennemi;
    //Get the parent
    public Robot ColoredRobot;
    //If the ennemi character jump on the head of a robot, the robot must die
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.Equals(ennemi)){ //The robot dies only if its ennemi kills it
            ColoredRobot.isAlive = false;
            ColoredRobot.RobotDie(ColoredRobot);

        }
    }
}