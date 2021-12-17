using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class GiveOwnership : MonoBehaviour
{
    public List<PhotonView> charactersView;
    private List<Player> playersInRoom = new List<Player>();

    private int swapIndex = 1;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Awake()
    {       
        gameManager = FindObjectOfType<GameManager>();//Get the gameManager
        foreach(KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players){
            playersInRoom.Add(player.Value); //Get all players in the room
        }    
        playersInRoom.OrderBy(player => player.ActorNumber); //Order the player by their actor name in order that every client get the same order 
        int i = 0;
        foreach(Player player in playersInRoom){
            if(charactersView[i] != null){
                charactersView[i].TransferOwnership(player); //Initialise the ownership : 1 character by player
                i++;
            }
        }
        InvokeRepeating("SwapOwnership", 15f, 15f); //Launch the swap of characters
    }

    public void SwapOwnership(){ //Similar to the swap function of local mode
        gameManager.ResetSwapSlider(); 
        int i = 0;
        foreach(Player player in playersInRoom){
            if(charactersView[(i + swapIndex) % 4] != null){ 
                charactersView[(i + swapIndex) % 4].TransferOwnership(player); //Transfer the character to the next player (cycle of ownership)
                i++;
            }
        }
        swapIndex = (swapIndex + 1) % 4; //To assure that the index is always between 0 and 3
    }
}
