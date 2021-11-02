using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
// THIS SCRIPT IS A TEMPORARY ONE USED FOR TESTING OR DEMO.
public class TempMovement : MonoBehaviour
{
    // --- Attributes ---

    // Movement
    public float speed = 1.0f; // Speed of the movement
    public float impulsionForce = 3.2f;

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

    // Swap delay
    private float swapDelay = 3.0f;


    // Configure the controls for the beginning
    void Awake() {
        this.character = GetComponent<Character>();
        if (character != null) {
            // We set different controllers according to the character name
            switch(character.name) {
                // Fei is controlled by the arrow keys
                case "Fei":
                    setControlsToArrowKeys();
                    break;
                // Henrik is controlled by the ZQSD scheme
                case "Henrik":
                    setControlsToZQSDKeys();
                    break;
            }
        }
    }

    // Call the swap method according to the delay value
    void Start() {
        InvokeRepeating(nameof(swapControls), swapDelay, swapDelay);
    }

    void Update(){
        // When a horizontal movement is detected (left or right)
        if (Input.GetKey(leftKey) || Input.GetKey(rightKey)) {
            move = true;
            if(Input.GetKey(rightKey)){ // if the character goes right
                this.transform.Translate(Vector2.right * Time.deltaTime * speed);
                GetComponent<SpriteRenderer>().flipX = false;
            } else { // if the character goes left
                this.transform.Translate(Vector2.left * Time.deltaTime * speed);
                GetComponent<SpriteRenderer>().flipX = true;
            }
        } else {
            // the character is not going left or right => no movement 
            move = false;
        }

        // if the jumpKey is pressed : jump
        if(Input.GetKeyDown(jumpKey)){
            GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, impulsionForce), ForceMode2D.Impulse);
        }

        // if the powerKey is pressed : launch the transform state of the animator 
        if(Input.GetKeyDown(powerKey)){
            animator.SetTrigger("transformation");
            power = !power;
            if(power){
                character.OnPowerActivate();
            }
            else {
                character.OnPowerDeactivate();
            }
        }
        
        // send the parameters of the character state to the animator
        animator.SetBool("move", move);
        animator.SetBool("power", power);
    }

    // Swap the controls of the selected character
    private void swapControls() {
        if (jumpKey == KeyCode.UpArrow) {
            setControlsToZQSDKeys();
        } else {
            setControlsToArrowKeys();
        }
    }

    // Method to set controls to the arrow keys
    private void setControlsToArrowKeys() {
        jumpKey = KeyCode.UpArrow;
        leftKey = KeyCode.LeftArrow;
        rightKey = KeyCode.RightArrow;
        powerKey = KeyCode.RightShift;
    }

    // Method to set controls to the ZQSD keys
    private void setControlsToZQSDKeys() {
        jumpKey = KeyCode.Z;
        leftKey = KeyCode.Q;
        rightKey = KeyCode.D;
        powerKey = KeyCode.E;
    }
}