using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToBoss : Door
{
    protected override void OnDoorEntered() 
    {  
        SceneManager.LoadScene("DialogBoss"); //Load the scene of dialog of the boss scene
    }
}
