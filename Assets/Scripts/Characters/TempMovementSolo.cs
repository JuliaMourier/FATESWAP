using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
// THIS SCRIPT IS A TEMPORARY ONE USED FOR TESTING OR DEMO.
public class TempMovementSolo : MonoBehaviour
{
    // --- Attributes ---

    // Movement
    public float speed = 1.4f; // Speed of the movement
    public float impulsionForce = 3.2f;
    private bool IsGrounded = true;
    public Transform top_left;
    public Transform bottom_right;
    public LayerMask Obstacles;
    private int jumpCount = 1;
    [SerializeField] private GameObject Player1;
    [SerializeField] private GameObject Player2;
    [SerializeField] private GameObject Player3;
    [SerializeField] private GameObject Player4;
    // Animator
    public Animator animator; // Animator for the different animations of the character => idle, walk, transfor, walkpower and idlepower
    public bool power { get; private set;} = false; // Parameter of the animator : true when the character is in the power state, false otherwise
    public bool move {get; private set;} // Parameter of the animator : true when the player press a key to move, false otherwise
    
    // Character
    public Character character; // character who's moving

    // Controllers
    private KeyCode jumpKey;
   
    private KeyCode powerKey;
    private KeyCode swap;

    private string axis;

    public KeyCode shootKey { get; private set; }
    public KeyCode switchPressKey { get; private set; }


    // Configure the controls for the beginning
    void Awake() {
        //this.character = GetComponent<Character>();
        
        if (character != null) {
            // We set different controllers according to the character name
            switch(character.name) {
                // Fei is controlled by default with the arrow keys
                case "Fei":
                    setControlsToArrowKeys();
                    Player2.transform.parent = Player1.transform;
                    Player3.transform.parent = Player1.transform;
                    Player4.transform.parent = Player1.transform;
                    break;
                // Henrik is controlled by defautl with the ZQSD scheme
                case "Henrik":
                    setControlsToArrowKeys();
                    Player2.transform.parent = Player1.transform;
                    Player3.transform.parent = Player1.transform;
                    Player4.transform.parent = Player1.transform;
                    break;
                // Victoria is controlled by default with IJLO scheme
                case "Victoria":
                    setControlsToArrowKeys();
                    Player2.transform.parent = Player1.transform;
                    Player3.transform.parent = Player1.transform;
                    Player4.transform.parent = Player1.transform;
                    break;
                // Lucie is controlled by default with CVBSpace scheme
                case "Lucie":
                    setControlsToArrowKeys();
                    Player2.transform.parent = Player1.transform;
                    Player3.transform.parent = Player1.transform;
                    Player4.transform.parent = Player1.transform;
                    break;
            }
        }
    }

    void Update(){
        // When a horizontal movement is detected (left or right)
        if (Input.GetAxis(axis) == 1) {
            move = true;
            this.transform.rotation = Quaternion.Euler(new Vector3(this.transform.rotation.x, 0, this.transform.rotation.z));
            this.transform.Translate(Vector2.right * Time.deltaTime * speed);
            character.SetDirection(new Vector3(1, 0, 0)); //Direction to the right

        }
        else if (Input.GetAxis(axis) == -1)
        { // if the character goes left
            move = true;
            this.transform.rotation = Quaternion.Euler(new Vector3(this.transform.rotation.x, 180, this.transform.rotation.z));
            this.transform.Translate(Vector2.right * Time.deltaTime * speed);
            character.SetDirection(new Vector3(-1, 0, 0)); //Direction to the right
        }
        else
        {
            // the character is not going left or right => no movement 
            move = false;
        }
        if (Input.GetKeyDown(swap))
        {
            Player2.transform.parent = null;
            Player1.transform.parent = Player2.transform;
            Player4.transform.parent = Player2.transform;
            Player3.transform.parent = Player2.transform;
            Player2.SetActive(true);
            Player1.SetActive(false);
        }


        // if the jumpKey is pressed and it's Lucie, you can jump and double jump
        if (character.name == "Lucie")
        {
        if (power){
                if (Input.GetKeyDown(jumpKey) && (jumpCount != 0))
                {
                    jumpCount -= 1;
                    GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, impulsionForce), ForceMode2D.Impulse);
                }

            } // if it's not Lucie, you can jump once
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
        swap = KeyCode.RightControl;
        jumpKey = KeyCode.UpArrow;
        axis = "Horizontal";
        powerKey = KeyCode.X;
        if (character.name == "Victoria")
        {
            shootKey = KeyCode.W;
        }
        if (character.name == "Henrik")
        {
            switchPressKey = KeyCode.W;
        }
    }

    // Method to set controls to the ZQDE keys
    private void setControlsToZQDEKeys() {
        jumpKey = KeyCode.Z;

        powerKey = KeyCode.E;
    }

    // Method to set controls to the IJLO keys
    private void setControlsToIJLOKeys() {
        jumpKey = KeyCode.I;

        powerKey = KeyCode.O;
    }

    // Method to set controls to the CVBSpace keys
    private void setControlsToCVBSpaceKeys() {
        jumpKey = KeyCode.Space;

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