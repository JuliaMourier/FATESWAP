using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;



public class LoadingMenu : MonoBehaviour
{
    private Slider loadingSlider;

    public TextMeshProUGUI percentage;
   
    private void Awake() {
        loadingSlider = FindObjectOfType<Slider>();
    }
    // Update the loading slider (this is just for style)
    private void Start() {
        StartCoroutine(Load()); //Load bar animation
    }

    //Stop the laser beam to be lethal for 0.7s 
    private IEnumerator Load(){ 

        float duration = 2f; //Duration of the disability
        float elapsed = 0.0f;

        // wait
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime; //increment with time
            //Change the value of the slider and the percentage text
            loadingSlider.value = loadingSlider.value + (loadingSlider.maxValue - loadingSlider.value) * elapsed /duration; 
            percentage.text = ((int)((loadingSlider.value + (loadingSlider.maxValue - loadingSlider.value) * elapsed /duration)*100) ).ToString() + "%";
            yield return null; //do nothing while waiting
        }

        SceneManager.LoadScene("MainMenu");

    }
}
