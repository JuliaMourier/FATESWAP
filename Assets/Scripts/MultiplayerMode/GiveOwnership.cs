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

    // Start is called before the first frame update
    void Awake()
    {       
        foreach(KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players){
            playersInRoom.Add(player.Value);
        }    
        playersInRoom.OrderBy(player => player.ActorNumber);
        int i = 0;
        foreach(Player player in playersInRoom){
            if(charactersView[i] != null){
                charactersView[i].TransferOwnership(player);
                i++;
            }
        }
        //charactersView[players.Key - 1].TransferOwnership(players.Value);
    }
}
