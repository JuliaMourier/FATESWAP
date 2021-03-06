using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementSolo : MonoBehaviour
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

    public KeyCode shootKey { get; private set; }
    public KeyCode shootKey2 { get; private set; }
    public KeyCode switchPressKey { get; private set; }
    public KeyCode switchPressKey2 { get; private set; }


    // Configure the controls for the beginning
    void Awake() {
        this.character = GetComponent<Character>();
        
        if (character != null) {
            setControlsToArrowKeys();
            Player2.transform.parent = Player1.transform;
            Player3.transform.parent = Player1.transform;
            Player4.transform.parent = Player1.transform;
        }
    }

    void Update(){
        // When a horizontal movement is detected (left or right)
        
        if (Input.GetAxis("Horizontal") == 1) {
            move = true;
            this.transform.rotation = Quaternion.Euler(new Vector3(this.transform.rotation.x, 0, this.transform.rotation.z));
            this.transform.Translate(Vector2.right * Time.deltaTime * speed);
            character.SetDirection(new Vector3(1, 0, 0)); //Direction to the right

        }
        else if (Input.GetAxis("Horizontal") == -1)
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
        if (Input.GetKeyDown(swap) || Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            Player2.transform.parent = null;
            Player1.transform.parent = Player2.transform;
            Player4.transform.parent = Player2.transform;
            Player3.transform.parent = Player2.transform;
            Player2.SetActive(true);
            Player1.SetActive(false);
            power = false;
        }


        // if the jumpKey is pressed and it's Lucie, you can jump and double jump
        if (character.name == "Lucie")
        {
        if (power){
                if ((Input.GetKeyDown(jumpKey) || Input.GetKeyDown(KeyCode.Joystick1Button1)) && (jumpCount != 0))
                {
                    jumpCount -= 1;
                    GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, impulsionForce), ForceMode2D.Impulse);
                }

            } // if it's not Lucie, you can jump once
            else if ((Input.GetKeyDown(jumpKey) || Input.GetKeyDown(KeyCode.Joystick1Button1)) && IsGrounded)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, impulsionForce), ForceMode2D.Impulse);
            }
        }
        else if ((Input.GetKeyDown(jumpKey) || Input.GetKeyDown(KeyCode.Joystick1Button1)) && IsGrounded){
            GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, impulsionForce), ForceMode2D.Impulse);
        }


        // if the powerKey is pressed : launch the transform state of the animator 
        if (Input.GetKeyDown(powerKey) || Input.GetKeyDown(KeyCode.Joystick1Button0)){
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


    // Method to set controls to the arrow keys
    private void setControlsToArrowKeys() {
        swap = KeyCode.C;
        jumpKey = KeyCode.UpArrow;
        powerKey = KeyCode.X;
        shootKey2 = KeyCode.Joystick1Button2;
        shootKey = KeyCode.W;
        switchPressKey2 = KeyCode.Joystick1Button2;
        switchPressKey = KeyCode.W;
        
    }
}