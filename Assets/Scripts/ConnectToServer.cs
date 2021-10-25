using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    //Loading Menu
    public GameObject LoadingMenu;
    //Main Menu
    public GameObject MainMenu;

    // Connexion to Photon Network
    private void Start(){ 
        PhotonNetwork.ConnectUsingSettings();
    }

    // When connected, join the lobby
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    //When the lobby is joined, change the loading menu into the main menu
    public override void OnJoinedLobby()
    {
        LoadingMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

}
