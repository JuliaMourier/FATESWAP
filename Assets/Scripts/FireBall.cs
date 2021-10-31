using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float duration; //time to live
    public float speed; //speed of the Fireball
    public Vector3 direction; //direction of the fireball

    //When instantiated start to time to live routine => the fireball dies after "duration" seconds
    private void Awake() {
        StartCoroutine(DieWhenTimeHasElapsed());
    }

    private void Update() {
        this.transform.position = this.transform.position + direction * Time.deltaTime * speed;
    }


    //When the fireball hits a obstacle, ennemi or ally => deactivate the fireball after 0.1s   
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemies") || other.gameObject.layer == LayerMask.NameToLayer("Obstacles") || other.gameObject.layer == LayerMask.NameToLayer("Characters")){
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

    //Set the direction of the fireball with Vector3 given
    public void SetDirection(Vector3 newDirection){
        this.direction = newDirection;
    }

   
}
