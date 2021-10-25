using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    //--- Name for the room --- //
    //when create a room
    public InputField createLobby;
    //when join a room
    public InputField joinLobby;

    //UserName input field
    public InputField userName;

    //Player list menu
    public GameObject PlayerListMenu;
    //Menu for online game
    public GameObject OnlineMenu;

    //Menu for errors
    public GameObject ErrorMenu;
    public Text errorMessage;

    //Prefab item for the room (button allowing to join)
    public GameObject roomListItemPrefab;
    //Image containing the room list
    public Transform roomListContent;
    //Text for the title of the PlayerListMenu => Name of the current room
    public Text roomName;

    // //Image containing the player list
    public Transform playerListContent;

    //Prefab item for the player (name of the player)
    public GameObject playerListItemPrefab;


    //Allow a player to create a room thanks to photon
    public void CreateRoom(){
        GetUserName();
        if(createLobby.text != ""){
            PhotonNetwork.CreateRoom(createLobby.text.ToUpper()); //Create room (upper case to avoid players errors))
        }
        Debug.Log(PhotonNetwork.CountOfRooms); 
    }

    //Allow the player to Join the room thanks to photon
    public void JoinRoom(){
        Debug.Log("join room" + PhotonNetwork.CountOfPlayers);
        GetUserName();
        
        PhotonNetwork.JoinRoom(joinLobby.text.ToUpper()); //Join the room (upper case to avoid players errors)
        
    }

    //When a room is joined, redirect the player to the player list menu (TODO wait until 4 players) and name the room
    public override void OnJoinedRoom()
    {
        Debug.Log("Room Joined");
        OnlineMenu.SetActive(false); //deactivate the current menu
        PlayerListMenu.SetActive(true); //activate the menu of the list of players in the room
        roomName.text = PhotonNetwork.CurrentRoom.Name; //Name the room + number of players
        foreach(Transform transform in playerListContent){
            Destroy(transform.gameObject);
        }
        foreach(Player p in PhotonNetwork.PlayerList){ //Fill the players name in the room
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(p); //Display the list of the player currently in the room
        }
       // PhotonNetwork.LoadLevel("MainScene");
    }

    //Leave the room when click on back button
    public void LeaveRoom(){
        PhotonNetwork.LeaveRoom();
    }

    public void OnPhotonCreateRoomFailed(object[] codeAndMsg){
        ErrorMenu.SetActive(true); //Active the popup
        errorMessage.text = "Room could not be joined : \n" + codeAndMsg; //Error message add in the popup
    }

    // If the player can't join the room inform him with a popup
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        ErrorMenu.SetActive(true); //Active the popup
        errorMessage.text = "Room could not be joined : \n" + message; //Error message add in the popup
    }
    
    //When the room list updates itself, display the current rooms
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("List update, nb rooms : " + roomList.Count);
        foreach(Transform transform in roomListContent){ //Destroy the preexisting rooms
            Destroy(transform.gameObject);
        }
        for(int i = 0; i < roomList.Count; i++){ //Display the current rooms
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    //When a player enter the room display the new player name in the list and update the number of players currently in the room
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("player list updated");
        //UpdateNumberOfPlayer();
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    // Gets the username contained in the input Player Username and gives it to PhotonNetwork
    private void GetUserName(){
        if(userName.text == ""){
            PhotonNetwork.NickName = "Player" + Random.Range(0,1000).ToString("0000");
        }
        else {
            PhotonNetwork.NickName = userName.text;
        }
    }

    public void UpdateNumberOfPlayer(){
        roomName.text = PhotonNetwork.CurrentRoom.Name + " " + PhotonNetwork.CountOfPlayers + "/4"; //Name the room + number of players
    }
}

