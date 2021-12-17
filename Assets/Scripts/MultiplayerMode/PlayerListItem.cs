using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    //Text containing the player name in the item
    public Text playerName;

    //Current player
    Player player;

    //Set up the player and put its name in the item
    public void SetUp(Player _player){
        player = _player;
        playerName.text = _player.NickName;
    }

    // When a player leaves the room Destroy the gameObject if the player is the current player
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {   

        if(player == otherPlayer){
            Destroy(gameObject);
        }
    }
}
