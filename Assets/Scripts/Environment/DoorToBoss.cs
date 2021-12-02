using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToBoss : Door
{
    protected override void OnDoorEntered() 
    {   
        if(FindObjectOfType<GameManager>().solo){
            SceneManager.LoadScene("DialogBoss_Solo"); //Load the scene of dialog of the boss scene for solo mode
        }
        else{
            SceneManager.LoadScene("DialogBoss"); //Load the scene of dialog of the boss scene
        }
    }
}
