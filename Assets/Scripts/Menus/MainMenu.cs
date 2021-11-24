using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject cadenas1;
    public GameObject cadenas2;
    public GameObject cadenas3;
    public GameObject cadenas4;

    public void Cadenas(string scene)
    {
        if (scene == "Level1")
        {
            cadenas1.gameObject.SetActive(false);
        }
        if (scene == "Level2")
        {
            cadenas2.gameObject.SetActive(false);
        }
        if (scene == "Level3")
        {
            cadenas3.gameObject.SetActive(false);
        }
        if (scene == "Level4")
        {
            cadenas4.gameObject.SetActive(false);
        }
    }

    public void ExitGame() {
        //Quit the game
        Application.Quit();
    }

    public void PlayGame() {
        // Go to the nest scene in the stack
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayLevel1Solo() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        GetComponent<GameManager>().solo = true;
    }

    public void PlayLevel2Solo() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
    }
}
