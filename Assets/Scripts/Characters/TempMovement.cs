using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
// THIS SCRIPT IS A TEMPORARY ONE USED FOR TESTING OR DEMO.
public class TempMovement : MonoBehaviour
{
    // --- Attributes ---

    // Movement
    public float speed = 1.0f; // Speed of the movement
    public float impulsionForce = 3.2f;
    private bool IsGrounded = true;
    public Transform top_left;
    public Transform bottom_right;
    public LayerMask Obstacles;
    public int jumpCount = 1;
    // Animator
    public Animator animator; // Animator for the different animations of the character => idle, walk, transfor, walkpower and idlepower
    public bool power { get; private set;} = false; // Parameter of the animator : true when the character is in the power state, false otherwise
    public bool move {get; private set;} // Parameter of the animator : true when the player press a key to move, false otherwise
    
    // Character
    public Character character; // character who's moving

    // Controllers
    private KeyCode jumpKey;
    private KeyCode leftKey;
    private KeyCode rightKey;
    private KeyCode powerKey;


    // Configure the controls for the beginning
    void Awake() {
        //this.character = GetComponent<Character>();
        if (character != null) {
            // We set different controllers according to the character name
            switch(character.name) {
                // Fei is controlled by default with the arrow keys
                case "Fei":
                    setControlsToArrowKeys();
                    break;
                // Henrik is controlled by defautl with the ZQSD scheme
                case "Henrik":
                    setControlsToZQDEKeys();
                    break;
                // Victoria is controlled by default with IJLO scheme
                case "Victoria":
                    setControlsToIJLOKeys();
                    break;
                // Lucie is controlled by default with CVBSpace scheme
                case "Lucie":
                    setControlsToCVBSpaceKeys();
                    break;
            }
        }
    }

    void Update(){
        // When a horizontal movement is detected (left or right)
        if (Input.GetKey(leftKey) || Input.GetKey(rightKey)) {
            move = true;
            if(Input.GetKey(rightKey)){ // if the character goes right
                this.transform.Translate(Vector2.right * Time.deltaTime * speed);
                GetComponent<SpriteRenderer>().flipX = false;
                character.SetDirection(new Vector3(1, 0, 0)); //Direction to the right

            } else { // if the character goes left
                this.transform.Translate(Vector2.left * Time.deltaTime * speed);
                GetComponent<SpriteRenderer>().flipX = true;
                character.SetDirection(new Vector3(-1, 0, 0)); //Direction to the right

            }
        } else {
            // the character is not going left or right => no movement 
            move = false;
        }

        // if the jumpKey is pressed : jump
        if (character.name == "Lucie")
        {
        if (power){
                if (Input.GetKeyDown(jumpKey) && (jumpCount != 0))
                {
                    jumpCount -= 1;
                    Debug.Log(jumpCount);
                    GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, impulsionForce), ForceMode2D.Impulse);
                }

            }
            else if (Input.GetKeyDown(jumpKey) && IsGrounded)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, impulsionForce), ForceMode2D.Impulse);
            }
        }
        else if (Input.GetKeyDown(jumpKey)  && IsGrounded){
            GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, impulsionForce), ForceMode2D.Impulse);
        }
        /*
        if ((Input.GetButtonDown("Jump") && IsGrounded))
        {
            jumpKeyHeld = true;
            // animator.SetBool("isJumping", true);

            rb.AddForce(new Vector2(0, 2) * jumpForce * rb.mass, ForceMode2D.Impulse);
            IsGrounded = false;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            jumpKeyHeld = false;
        }*/

        // if the powerKey is pressed : launch the transform state of the animator 
        if (Input.GetKeyDown(powerKey)){
            animator.SetTrigger("transformation");
            power = !power;
            if(power) {
                character.OnPowerActivate();
            } else {
                character.OnPowerDeactivate();
            }
        }
        if (Physics2D.OverlapArea(top_left.position, bottom_right.position, Obstacles))
        {
            IsGrounded = true;
            if (character.name == "Lucie")
            {
                jumpCount = 1;
            }
        }
        else IsGrounded = false;
        
        // send the parameters of the character state to the animator
        animator.SetBool("move", move);
        animator.SetBool("power", power);
    }

    // Swap the controls of the selected character according to the int value passed in argument
    public void swapControls(int index) {
        switch(index) {
            case 0:
                setControlsToArrowKeys();
                break;
            case 1:
                setControlsToZQDEKeys();
                break;
            case 2:
                setControlsToIJLOKeys();
                break;
            case 3:
                setControlsToCVBSpaceKeys();
                break;
        }
    }

    // Method to set controls to the arrow keys
    private void setControlsToArrowKeys() {
        jumpKey = KeyCode.UpArrow;
        leftKey = KeyCode.LeftArrow;
        rightKey = KeyCode.RightArrow;
        powerKey = KeyCode.RightShift;
    }

    // Method to set controls to the ZQDE keys
    private void setControlsToZQDEKeys() {
        jumpKey = KeyCode.Z;
        leftKey = KeyCode.Q;
        rightKey = KeyCode.D;
        powerKey = KeyCode.E;
    }

    // Method to set controls to the IJLO keys
    private void setControlsToIJLOKeys() {
        jumpKey = KeyCode.I;
        leftKey = KeyCode.J;
        rightKey = KeyCode.L;
        powerKey = KeyCode.O;
    }

    // Method to set controls to the CVBSpace keys
    private void setControlsToCVBSpaceKeys() {
        jumpKey = KeyCode.Space;
        leftKey = KeyCode.V;
        rightKey = KeyCode.B;
        powerKey = KeyCode.C;
    }
    /*
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 ^collision.gameObject.layer == 8)
        {
            IsGrounded = false;
            // animator.SetBool("isJumping", true);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 ^ collision.gameObject.layer == 8)
        {
            IsGrounded = true;
            // animator.SetBool("isJumping", false);
        }
    }*/
}