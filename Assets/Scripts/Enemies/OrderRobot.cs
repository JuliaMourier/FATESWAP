using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderRobot : MonoBehaviour
{
    public List<Robot> listOfRobots;

    public Collectables key;

    public Switch hiddenSwitch;

    private int check = 0;

    private void Update() {
        if(check == listOfRobots.Count){
            key.gameObject.SetActive(true);
            hiddenSwitch.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
        
        for(int i = check; i < listOfRobots.Count; i++){
            if(listOfRobots[i].isAlive == false){
                if(i > check + 1){
                    check = 0;
                    FindObjectOfType<GameManager>().RestartLevel();
                }
            }
        }

        if(listOfRobots[check].isAlive == false){
            check++;
        }
    }

}
