using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;


public class RoomListItem : MonoBehaviourPunCallbacks
{

    //Text for the item : name of the room
    public Text nameOfRoom;

    //information about the current room
    RoomInfo roomInfo;

    //Set up the info of the room and put the name of the room in the item
    public void SetUp(RoomInfo _info){
        roomInfo = _info;
        /*if(userName.text == ""){
            PhotonNetwork.NickName = "Player" + Random.Range(0,1000).ToString("0000");
        }*/
        nameOfRoom.text = _info.Name;
    }

    //When the player click on this room item button, join the room corresponding to its name
    public void OnClick(){
        PhotonNetwork.JoinRoom(nameOfRoom.text);
    } 
}
