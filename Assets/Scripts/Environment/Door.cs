using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Door : MonoBehaviour
{
    public List<Character> characters;
    private SpriteRenderer spriteRenderer;

    private int numberCharacterWhoEnteredTheDoor = 0;
    private bool solo = false;
    private bool multi = false;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        solo = FindObjectOfType<GameManager>().solo;
        multi = FindObjectOfType<GameManager>().multi;
        
    }

    private void OnTriggerStay2D(Collider2D other) {
        
        if (spriteRenderer.color == Color.black){ //If door is open
            if (solo)
            {
                    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Joystick1Button2))
                {
                    this.gameObject.SetActive(false);
                    OnDoorEntered();
                }
            }
            else if(multi){
                foreach(Character character in characters){
                    if(other.gameObject.Equals(character.gameObject)){ //If its the character 
                        if(character.GetComponent<PhotonView>().IsMine && (Input.GetKey(character.GetComponent<MovementMultiplayerMode>().shootKey) ||Input.GetKey(KeyCode.Joystick1Button2)))
                            {//and he wants to go through the door
                            PhotonView photonView = PhotonView.Get(this);
                            photonView.RPC("CharacterEntered", RpcTarget.All, characters.IndexOf(character));
                            
                        }
                    }
                }
            }
            else
            {
                foreach(Character character in characters){
                    if(other.gameObject.Equals(character.gameObject)){ //If its the character 
                        if(Input.GetKey(character.GetComponent<Movement>().shootKey))
                            {//and he wants to go through the door
                            character.gameObject.SetActive(false); //character enter
                            numberCharacterWhoEnteredTheDoor++; //One character more is entered
                        }
                    }
                }
            }
        }
        if (numberCharacterWhoEnteredTheDoor >= 4){
            OnDoorEntered();
        }
        
    }
    //Open the door
    protected virtual void OnDoorEntered(){ //When the door is entered by all the characters
        FindObjectOfType<GameManager>().WinTheGame(this);
    }

    [PunRPC]
    private void CharacterEntered(int index){
        characters[index].gameObject.SetActive(false); //character enter
        numberCharacterWhoEnteredTheDoor++; //One character more is entered
    }

    
}
