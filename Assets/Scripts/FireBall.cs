using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float duration;
    public float speed;
    private Vector3 direction;
    private void Awake() {
        direction = new Vector3(1, 0, 0);
        StartCoroutine(DieWhenTimeHasElapsed());
    }

    private void Update() {
        this.transform.position = this.transform.position + direction * Time.deltaTime * speed;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemies") || other.gameObject.layer == LayerMask.NameToLayer("Obstacles")){
            duration = 0.1f;
            StopAllCoroutines();
            StartCoroutine(DieWhenTimeHasElapsed());
        }
    }

    //Deactivate when time to live is elapsed
    private IEnumerator DieWhenTimeHasElapsed(){ 

        float elapsed = 0.0f;

        // Animate the opening
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            yield return null; //do nothing while waiting
        }

        this.gameObject.SetActive(false); //Deactivate the fireball

    }

}
