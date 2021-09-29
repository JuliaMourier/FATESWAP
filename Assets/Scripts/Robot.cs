using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public Animator animator;

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.layer == LayerMask.NameToLayer("Characters")){
            FindObjectOfType<GameManager>().HeroesTakeDamage();            
        }
    }

    public void RobotDie(){
        animator.SetTrigger("boom");
        Invoke("Disable", 1);
            //StartCoroutine(DeactivateRobot());
    }

    public void Disable(){
        this.gameObject.SetActive(false);
    }

    private IEnumerator DeactivateRobot(){
        float duration = 0.4f;
        float elapsed = 0.0f;

        // Animate to the starting point
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        this.gameObject.SetActive(false);
    }
}
