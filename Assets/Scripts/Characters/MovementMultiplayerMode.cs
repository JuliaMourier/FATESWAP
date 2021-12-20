using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;


[RequireComponent(typeof(Rigidbody2D))]
//This script is  used for the multiplayer mode with photon
public class MovementMultiplayerMode : MonoBehaviour
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

    private PhotonView view;

    private string nickname = "";

    // Configure the controls for the beginning
    void Awake() {
        view = GetComponent<PhotonView>();        

        switchPressKey = KeyCode.W;
        shootKey = KeyCode.W;
        // The label is displayed during 3 seconds
        // After that, we hide it
        //this.Invoke(nameof(hidePlayerLabel), 3f);
    }

    void Update(){ //Update
           if(view.IsMine){
            if(Input.GetAxis("Horizontal") > 0.9){ // if the character goes right
            move = true;
            this.transform.rotation = Quaternion.Euler(new Vector3(this.transform.rotation.x, 0, this.transform.rotation.z));
            this.transform.Translate(Vector2.right * Time.deltaTime * speed);
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("SetDirectionForAll", RpcTarget.All,new Vector3(1, 0, 0));

        } else if (Input.GetAxis("Horizontal") < -0.9){ // if the character goes left
            move = true;
            this.transform.rotation = Quaternion.Euler(new Vector3(this.transform.rotation.x, 180, this.transform.rotation.z));
            this.transform.Translate(Vector2.right * Time.deltaTime * speed);
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("SetDirectionForAll", RpcTarget.All,new Vector3(-1, 0, 0));
        } else {
            // the character is not going left or right => no movement 
            move = false;
        }

        // if the jumpKey is pressed : jump
        if (character.name == "Luciem"){
            if (power){
                if ((Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))&& (jumpCount != 0))
                {
                    jumpCount -= 1;
                    GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, impulsionForce), ForceMode2D.Impulse);
                }

            }
            else if ((Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && IsGrounded)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, impulsionForce), ForceMode2D.Impulse);
            }
        }
        else if ((Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))  && IsGrounded){
            GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, impulsionForce), ForceMode2D.Impulse);
        }
        
        // if the powerKey is pressed : launch the transform state of the animator 
        if ((Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.X))){
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
            if (character.name == "Luciem")
            {
                jumpCount = 1;
            }
        }
        else IsGrounded = false;
        
        // send the parameters of the character state to the animator
        animator.SetBool("move", move);
        animator.SetBool("power", power);

        // Update the label position above the character head
        //sign.transform.position = this.transform.position + Vector3.up * 0.4f;
        }   
    }


    // Method to hide the player label
    private void hidePlayerLabel() {
        sign.SetActive(false);
    }


    public void SetCorrectNickName(string playerName){
        nickname = playerName;
    }
}