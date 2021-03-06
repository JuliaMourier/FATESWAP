using System.Collections;
using UnityEngine;
using Photon.Pun;

public class Switch : MonoBehaviour
{
    public Sprite switchOn; //Sprite of the on switch
    public Sprite switchOff; //Sprite of the off switch

    private bool isSwitchedOn = false; //State of the switch
    private bool available = true; //Availability of the switch
    public GameManager GameManager;
    public Character theOneWhoCanSwitch; //If there is only one person who can switch the attribute is filled, else anyone can switch

    private Character[] characters;

    public AudioSource switchSound;

    private void Awake() {
        characters = FindObjectsOfType<Character>();
    }

    private void OnTriggerStay2D(Collider2D other) {
        //if the character hit the switch
        if(theOneWhoCanSwitch == null){
            if(other.gameObject.layer == LayerMask.NameToLayer("Characters")){ //if anyone can switch the switch no parameter theOneWhoCanSwitch is specified
                foreach(Character character in characters){ //Test for each character
                    if(other.gameObject.Equals(character.gameObject)){ //If its the character 
                        if(GameManager.multi){
                            if((Input.GetKey(character.GetComponent<MovementMultiplayerMode>().shootKey) || Input.GetKey(KeyCode.Joystick1Button2)) && available){ //and he wants to go through the door   
                                PhotonView photonView = PhotonView.Get(this);
                                photonView.RPC("MSwitchOn", RpcTarget.All);
                            }
                        }
                        else {
                            if(Input.GetKey(character.GetComponent<Movement>().shootKey) && available){ //and he wants to go through the door   
                                available = false; //Disable the switch
                                SwitchOn(); //Launch the change of state
                            }
                        }
                    }
                }
            }
        }
        else {
            if (GameManager.solo)
            {

                if (other.gameObject.Equals(theOneWhoCanSwitch.gameObject))
                { //if only one character can switch the switch, check if the collision is dur to this character
                    if (available && theOneWhoCanSwitch.isPowerActivate && (Input.GetKey(theOneWhoCanSwitch.GetComponent<MovementSolo>().switchPressKey) || Input.GetKey(theOneWhoCanSwitch.GetComponent<MovementSolo>().switchPressKey2)))
                    { //if Henrik is capable of switch the switch
                        available = false; //Disable the switch
                        SwitchOn(); //Launch the change of state
                    }
                }
            }
            else if(GameManager.multi){
                if (other.gameObject.Equals(theOneWhoCanSwitch.gameObject))
                { //if only one character can switch the switch, check if the collision is dur to this character
                    if (theOneWhoCanSwitch.GetComponent<PhotonView>().IsMine && available && theOneWhoCanSwitch.isPowerActivate && (Input.GetKey(theOneWhoCanSwitch.GetComponent<MovementMultiplayerMode>().switchPressKey) || Input.GetKey(KeyCode.Joystick1Button2)))
                    { //if Henrik is capable of switch the switch
                        PhotonView photonView = PhotonView.Get(this);
                        photonView.RPC("MSwitchOn", RpcTarget.All);
                    }
                }
            }
            else
            {

                if (other.gameObject.Equals(theOneWhoCanSwitch.gameObject))
                { //if only one character can switch the switch, check if the collision is dur to this character
                    if (available && theOneWhoCanSwitch.isPowerActivate && Input.GetKey(theOneWhoCanSwitch.GetComponent<Movement>().switchPressKey))
                    { //if Henrik is capable of switch the switch
                        available = false; //Disable the switch
                        SwitchOn(); //Launch the change of state
                    }
                }
            }
        }
       
    }

    //Switch the sprite of the switch and its state attribute
    private void SwitchOn(){
        
        switchSound.Play();
        if (isSwitchedOn){
            GetComponent<SpriteRenderer>().sprite = switchOff;
        }
        else {
            GetComponent<SpriteRenderer>().sprite = switchOn;
        }
        isSwitchedOn = !isSwitchedOn;
        StartCoroutine(WaitUntilAvailable());
    }

    //Get the state of the switch : true if On, false if Off
    public bool GetSwitchState(){ 
        return isSwitchedOn;
    }

    //Disable the switch for 0.5s
    private IEnumerator WaitUntilAvailable(){ 

        float duration = 0.7f; //Duration of the disability
        float elapsed = 0.0f;

        // wait
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            yield return null; //do nothing while waiting
        }

        available = true; //The switch is back to available

    }

    //Switch the sprite of the switch and its state attribute
    [PunRPC]
    private void MSwitchOn(){
        available = false;
        switchSound.Play();
        if (isSwitchedOn){
            GetComponent<SpriteRenderer>().sprite = switchOff;
        }
        else {
            GetComponent<SpriteRenderer>().sprite = switchOn;
        }
        isSwitchedOn = !isSwitchedOn;
        StartCoroutine(WaitUntilAvailable());
    }

   
}
