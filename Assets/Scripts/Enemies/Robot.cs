using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public GameObject ennemi ; //Ennemi of the robot => Null if all characters can kill him
    public Animator animator; //Animator
    public Vector2 initialDirection; //Initial direction => to set 
    public float speed; //Speed factor for its movements
    private Vector2 direction; //Direction of the robot
    private bool robotMove = false; //is robot moving
    public bool isAlive = true; //is robot alive, set to false when dies
    public AudioSource explosionSound;//Explosion sound
    public bool isKillable = true;

    // initialisation of the direction of the robot
    private void Awake(){
        direction = initialDirection;
        robotMove = true;
        if(isAlive){
            animator.SetBool("robotMove", robotMove);
        }
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
        if(isAlive){
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
        else {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);//new Vector3(4.57f, -0.75f, this.transform.position.z);
        }
        
    }

    //when robot dies launch the animation of explosion and deactivate the robot after 1s
    public void RobotDie(){
        //Invoke("Disable", 1);
        if(isAlive){
            StartCoroutine(DeactivateRobot());
        }
    }

    //deactivate the robot
    public void Disable(){
        this.gameObject.SetActive(false);
    }

    // If we want to put less than 1s for the deactivation
    protected IEnumerator DeactivateRobot(){
        isAlive = false;
        explosionSound.Play();
        animator.SetTrigger("boom");
        float duration = 1f;
        float elapsed = 0.0f;

        // Animate to the starting point
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        if(this.name == "PrJavier"){
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            this.transform.rotation = Quaternion.Euler(new  Vector3(this.transform.rotation.x, this.transform.rotation.y, 70));
            this.transform.position =  new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        }
        else {
            this.Disable();
        }
    }

    
}
