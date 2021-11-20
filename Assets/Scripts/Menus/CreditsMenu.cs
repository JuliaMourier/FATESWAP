using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMenu : MonoBehaviour
{

    private void Awake() {
        StartCoroutine(WaitAnimation());
    }
    // Update is called once per frame


    private IEnumerator WaitAnimation(){ 

        float duration = 23.0f; //Duration of the disability
        float elapsed = 0.0f;

        // wait
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            yield return null; //do nothing while waiting
        }

        SceneManager.LoadScene("MainMenu");

    }

}
