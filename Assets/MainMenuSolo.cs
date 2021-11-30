using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSolo : MonoBehaviour
{
    public void ExitGame()
    { //Quit the game
        Application.Quit();
    }

    public void PlayGame()
    { // Go to the nest scene inthe stack
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayLevel1()
    { // Go to the nest scene inthe stack
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void PlayLevel2()
    { // Go to the nest scene inthe stack
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
    }

    public void PlayLevel3()
    { // Go to the nest scene inthe stack
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 5);
    }

    public void PlayLevel4()
    { // Go to the nest scene inthe stack
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 7);
    }
}