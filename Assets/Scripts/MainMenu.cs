using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ExitGame(){ //Quit the game
        Application.Quit();
    }

    public void PlayGame(){ // Go to the nest scene inthe stack
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
