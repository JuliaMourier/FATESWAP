using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OrderRobot : MonoBehaviour
{
    public List<Robot> listOfRobots; //list of ordered robots 

    public Collectables key; //hidden key (Set to active = false) and 'll be set active when all robot will be killed

    public Switch hiddenSwitch; //hidden switch (Set to active = false) and 'll be set active when all robot will be killed

    public List<GameObject> hintColors;
    private int check = 0; //Count of well killed robots (used as index)
    private void Update() { 
        
        
        for(int i = check; i < listOfRobots.Count; i++){ //Go through the list of robot
            if(listOfRobots[i].isAlive == false){ //if one of the robot is dead
                if(i > check){ //but not in the right order : restart
                    check = 0;
                    FindObjectOfType<GameManager>().RestartLevel();
                }
            }
        }

        if(!listOfRobots[check].isAlive && check < listOfRobots.Count){ //If the correct robot have been slained    
            if(check < hintColors.Count){
                hintColors[check].SetActive(true);
            }
            check++;
            if(check == listOfRobots.Count){ //if all the robot have been killed in the right order
                key.gameObject.SetActive(true); // Set active the key 
                hiddenSwitch.gameObject.SetActive(true); //and the switch
                this.gameObject.SetActive(false); //Deactive this object to stop this update to work for nothing
            }
        }
    }

}
