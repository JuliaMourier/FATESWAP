using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    //Attributs :
    // for the movement :
    public float speed = 1.0f; // Speed of the movement
    public float impulsionForce = 3.2f;
    // for Animator :
    public Animator animator; // Animator for the different animations of the character => idle, walk, transfor, walkpower and idlepower
    public bool power { get; private set;} = false; // Parameter of the animator : true when the character is in the power state, false otherwise
    public bool move {get; private set;} // Parameter of the animator : true when the player press a key to move, false otherwise
    public Character character; //character who's moving
    void Update(){
        // get the movement from the player's inputs
        float horizontalMovement = Input.GetAxis("Horizontal");
        // transform it into a 3D vector :
        Vector3 horizontal = new Vector3(horizontalMovement, 0.0f, 0.0f);

        // Change the position of the character
        this.transform.position = this.transform.position + horizontal * Time.deltaTime * speed;
        
        // Change the oriantation of the sprite in function of the direction
        if(horizontalMovement > 0.5){ // if the character goes right the sprite need to stay at 0 rotation
            this.transform.rotation = Quaternion.Euler(new Vector3(this.transform.rotation.x, 0, this.transform.rotation.z));
            character.SetDirection(new Vector3(1, 0, 0)); //Direction to the right
            move = true; 
        }
        else if(horizontalMovement < -0.5){ // if the character goes left we have to rotate its sprite of 180°
            this.transform.rotation = Quaternion.Euler(new Vector3(this.transform.rotation.x, 180, this.transform.rotation.z));
            character.SetDirection(new Vector3(-1, 0, 0)); //Direction to the left
            move = true;
        }
        else {
            // the character is not going left or right => no movement 
            move = false;
        }

        // if T is pressed : launch the transform state of the animator 
        if(Input.GetKeyUp(KeyCode.T)){
            animator.SetTrigger("transformation");
            power = !power;
            if(power){
                GetComponent<Character>().OnPowerActivate();
            }
            else {
                GetComponent<Character>().OnPowerDeactivate();
            }
        }
        // if Space is pressed and the character is using his power : jump
        if(Input.GetKeyUp(KeyCode.Space)){
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, impulsionForce), ForceMode2D.Impulse);
        }
        
        // send the parameters of the character state to the animator
        animator.SetBool("move", move);
        animator.SetBool("power", power);
    }

}
