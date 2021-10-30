using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        this.gameObject.SetActive(false);
    }
}
