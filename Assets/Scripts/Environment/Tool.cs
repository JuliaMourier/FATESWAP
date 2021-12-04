using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public int id = 0;

    public Character characterWhoCanCollect;

    private ToolManager toolManager;

    private void Awake() {
        toolManager = FindObjectOfType<ToolManager>();
    }

    //when a character collect an object Collectable
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.Equals(characterWhoCanCollect.gameObject)){
            ToolFound();
        }
    }  

    private void ToolFound(){
        this.gameObject.SetActive(false);
        toolManager.AddTool(id);
    }
}
