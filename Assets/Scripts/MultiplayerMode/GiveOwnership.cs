using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GiveOwnership : MonoBehaviour
{
    public List<PhotonView> charactersView;
    // Start is called before the first frame update
    void Awake()
    {       
        
        foreach( KeyValuePair<int, Player> players in PhotonNetwork.CurrentRoom.Players){
            Debug.Log("Player : " + players.Value + " number : " + players.Value.CustomProperties["number"]);
            
            charactersView[players.Key - 1].TransferOwnership(players.Value);
            
        }    
    }
}
