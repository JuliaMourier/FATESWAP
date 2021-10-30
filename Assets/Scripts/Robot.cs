using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public Animator animator;
    public Vector2 initialDirection;
    public float speed;
    private Vector2 direction;
    private bool robotMove = false;

    public bool isAlive = true;

    // initialisation of the direction of the robot
    private void Awake(){
        direction = initialDirection;
        robotMove = true;
        animator.SetBool("robotMove", robotMove);
    }
    
    //If the robot find a node
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer == LayerMask.NameToLayer("Nodes")){
            ChangeDirection();
        }
    }

    // Inverse the direction of the robot 
    private void ChangeDirection(){
        direction = -direction;
    }

    //update the position of the robot
    private void Update(){
        Vector3 direction3D = new Vector3(direction.x, direction.y, this.transform.position.z);
        this.transform.position = this.transform.position + direction3D * Time.deltaTime * speed;
        // Set the sprite direction of the robot
        if(direction.x > 0.5){ // if the character goes right the sprite need to stay at 0 rotation
            this.transform.rotation = Quaternion.Euler(new Vector3(this.transform.rotation.x, 0, this.transform.rotation.z));
        }
        else if(direction.x < -0.5){ // if the character goes left we have to rotate its sprite of 180°
            this.transform.rotation = Quaternion.Euler(new Vector3(this.transform.rotation.x, 180, this.transform.rotation.z));
        }
    }

    //when robot dies launch the animation of explosion and deactivate the robot after 1s
    public void RobotDie(Robot robotToDestroy){
        
        //Invoke("Disable", 1);
        StartCoroutine(DeactivateRobot(robotToDestroy));
    }

    //deactivate the robot
    public void Disable(){
        this.gameObject.SetActive(false);
    }

    // If we want to put less than 1s for the deactivation
    private IEnumerator DeactivateRobot(Robot robotToDeactivate){
        animator.SetTrigger("boom");
        float duration = 1f;
        float elapsed = 0.0f;

        // Animate to the starting point
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        robotToDeactivate.Disable();
    }

    
}
