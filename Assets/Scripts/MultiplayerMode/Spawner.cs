using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviour
{
    public List<GameObject> playerPrefabs;
    public List<Transform> charactersPosition;
    // Start is called before the first frame update
    public void Start()
    {
        int index = (int) PhotonNetwork.LocalPlayer.CustomProperties["number"] - 1;
        PhotonNetwork.Instantiate(playerPrefabs[index].name, charactersPosition[index].position, Quaternion.identity);
/*
        if(GameObject.Find("Luciem(Clone)") == null){
            PhotonNetwork.Instantiate(playerPrefabs[0].name, charactersPosition[0].position, Quaternion.identity);
        }
        else {
            if(GameObject.Find("Feim(Clone)") == null){
                PhotonNetwork.Instantiate(playerPrefabs[1].name, charactersPosition[1].position, Quaternion.identity);
            }
            else {
                if(GameObject.Find("Victoriam(Clone)") == null){
                    PhotonNetwork.Instantiate(playerPrefabs[2].name, charactersPosition[2].position, Quaternion.identity);
                }
                else {
                    if(GameObject.Find("Henrikm(Clone)") == null){
                        PhotonNetwork.Instantiate(playerPrefabs[3].name, charactersPosition[3].position, Quaternion.identity);
                    }
                }
            }
        }*/
       // foreach(Player player in PhotonNetwork.CurrentRoom.PlayerList){ player.CustomProperties["number"]
        //}
    }
}
