using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviour
{
    public List<GameObject> playerPrefabs;
    public List<Transform> charactersPosition;

    public GameObject GameManager;


    // Instantiate the good character for each player
    public void Awake()
    {
        int index = (int) PhotonNetwork.LocalPlayer.CustomProperties["number"] - 1; //index of the player
        PhotonNetwork.Instantiate(playerPrefabs[index].name, charactersPosition[index].position, Quaternion.identity);
        if(index == 3){
            GameManager.SetActive(true);
        }
    }
}
