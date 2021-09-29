using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    public Sprite dead;
    public Sprite alive;
    public SpriteRenderer heartRenderer;

    public void LooseLive(){
        this.heartRenderer.sprite = dead;
    }

    public void Reset(){
        this.heartRenderer.sprite = alive;
    }
}
