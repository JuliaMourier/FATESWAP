using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    //When a fireball hits the target its gets deactivated
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Fireball")){
            this.gameObject.SetActive(false);
        }
    }
}
