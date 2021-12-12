using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    private int numberOfToolFound = 0;
    public Robot prJavier;

    public Collectables key;


    public List<SpriteRenderer> toolsHUD;

    public void AddTool(int number){
        toolsHUD[number].color = Color.white;
        numberOfToolFound++;
        if(numberOfToolFound == toolsHUD.Count){
            prJavier.isKillable = true;
            key.gameObject.SetActive(true);
        }
    }
}
