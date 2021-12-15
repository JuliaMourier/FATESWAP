using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    // --- Attributes ---

    // Movement
    public float speed = 1.0f; // Speed of the movement
    public float impulsionForce = 3.2f;
    private bool IsGrounded = true;
    public Transform top_left;
    public Transform bottom_right;
    public LayerMask Obstacles;
    private int jumpCount = 1;
    // Animator
    public Animator animator; // Animator for the different animations of the character => idle, walk, transfor, walkpower and idlepower
    public bool power {get; private set;} = false; // Parameter of the animator : true when the character is in the power state, false otherwise
    public bool move {get; private set;} // Parameter of the animator : true when the player press a key to move, false otherwise
    
    // Character
    public Character character; // character who's moving
    private GameObject sign; // Player label above the character head
    private TextMesh tm; // Text of the label

    // Controllers
    private KeyCode jumpKey;

    private string axis;
    private KeyCode powerKey;

    public KeyCode shootKey {get; private set;}
    public KeyCode switchPressKey{get; private set;}

    // Audio
    public AudioSource jumpSound;
    public AudioSource powerupSound;
    public AudioSource powerdownSound;



    // Configure the controls for the beginning
    void Awake() {
        // Init the label sign/text above the character
        sign = new GameObject(character.name);
        sign.transform.rotation = Camera.main.transform.rotation;
        sign.layer = LayerMask.NameToLayer("UI");
        tm = sign.AddComponent<TextMesh>();
        tm.color = new Color(1f, 0f, 0f);
        tm.fontStyle = FontStyle.Bold;
        tm.alignment = TextAlignment.Center;
        tm.anchor = TextAnchor.MiddleCenter;
        tm.characterSize = 0.065f;
        tm.fontSize = 30;

        // The label is displayed during 3 seconds
        // After that, we hide it
        this.Invoke(nameof(hidePlayerLabel), 3f);

        //this.character = GetComponent<Character>();
        if (character != null) {
            // We set different controllers according to the character name
            switch(character.name) {
                // By default, Fei is controlled by J3
                case "Fei":
                    setControlsToJ3();
                    break;
                // By default, Henrik is controlled by J1
                case "Henrik":
                    setControlsToJ1();
                    break;
                // By default, Victoria is controlled by J2
                case "Victoria":
                    setControlsToJ2();
                    break;
                // By default, Lucie is controlled by J4
                case "Lucie":
                    setControlsToJ4();
                    break;
            }
        }
    }

    void Update(){
        if(Input.GetAxis(axis) == 1){ // if the character goes right
            move = true;
            this.transform.rotation = Quaternion.Euler(new Vector3(this.transform.rotation.x, 0, this.transform.rotation.z));
            this.transform.Translate(Vector2.right * Time.deltaTime * speed);
            character.SetDirection(new Vector3(1, 0, 0)); //Direction to the right

        } else if (Input.GetAxis(axis) == -1){ // if the character goes left
            move = true;
            this.transform.rotation = Quaternion.Euler(new Vector3(this.transform.rotation.x, 180, this.transform.rotation.z));
            this.transform.Translate(Vector2.right * Time.deltaTime * speed);
            character.SetDirection(new Vector3(-1, 0, 0)); //Direction to the right
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
                    jumpSound.Play();
                    GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, impulsionForce), ForceMode2D.Impulse);
                }

            }
            else if (Input.GetKeyDown(jumpKey) && IsGrounded)
            {
                jumpSound.Play();
                GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, impulsionForce), ForceMode2D.Impulse);
            }
        }
        else if (Input.GetKeyDown(jumpKey)  && IsGrounded){
            jumpSound.Play();
            GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, impulsionForce), ForceMode2D.Impulse);
        }
        
        // if the powerKey is pressed : launch the transform state of the animator 
        if (Input.GetKeyDown(powerKey)){
            animator.SetTrigger("transformation");
            power = !power;
            if(power) {
                powerupSound.Play();
                character.OnPowerActivate();
            } else {
                powerdownSound.Play();
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

        // Update the label position above the character head
        sign.transform.position = this.transform.position + Vector3.up * 0.4f;
    }

    // Swap the controls of the selected character according to the int value passed in argument
    public void swapControls(int index) {
        switch(index) {
            case 0:
                setControlsToJ1();
                break;
            case 1:
                setControlsToJ2();
                break;
            case 2:
                setControlsToJ3();
                break;
            case 3:
                setControlsToJ4();
                break;
        }

        // We display the label in order to indicate to the player which character he controls
        sign.SetActive(true);
        // We display the label during 5 seconds, after that we hide it
        this.Invoke(nameof(hidePlayerLabel), 5f);
    }

    // Method to set controls to the KeyBoard
    private void setControlsToJ4() {
        jumpKey = KeyCode.UpArrow;
        axis = "HorizontalJ4";
        powerKey = KeyCode.X;
        tm.text = "J4";
        shootKey = KeyCode.W;
        
        if(character.name == "Henrik"){
            switchPressKey = KeyCode.W;
        }
    }

    // Method to set controls to the third joystick
    private void setControlsToJ3() {
        jumpKey = KeyCode.Joystick3Button1;
        axis = "Joystick3Axis";
        powerKey = KeyCode.Joystick3Button0;
        tm.text = "J3";
        shootKey = KeyCode.Joystick3Button2;
        
        if(character.name == "Henrik"){
            switchPressKey = KeyCode.Joystick3Button2;
        }
    }

    // Method to set controls to the second joystick
    private void setControlsToJ2() {
        jumpKey = KeyCode.Joystick2Button1;
        axis = "Joystick2Axis";
        powerKey = KeyCode.Joystick2Button0;
        tm.text = "J2";
        shootKey = KeyCode.Joystick2Button2;
        
        if(character.name == "Henrik"){
            switchPressKey = KeyCode.Joystick2Button2;
        }
    }

    // Method to set controls to the first joystick
    private void setControlsToJ1() {
        jumpKey = KeyCode.Joystick1Button1;
        powerKey = KeyCode.Joystick1Button0;
        axis = "Joystick1Axis";
        tm.text = "J1";
        shootKey = KeyCode.Joystick1Button2;
        
        if(character.name == "Henrik"){
            switchPressKey = KeyCode.Joystick1Button2;
        }
    }

    // Method to hide the player label
    private void hidePlayerLabel() {
        sign.SetActive(false);
    }

}