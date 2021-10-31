using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]

public class Character : MonoBehaviour
{
   protected Vector3 direction = new Vector3(1, 0, 0); //Direction of the character

   //When power is Activated
   virtual public void OnPowerActivate(){

   }

   //When power is deactivated
   virtual public void OnPowerDeactivate(){}

   //get the direction of the Character
   public Vector3 GetDirection(){
        return direction;
   }

   //Set the diretcion of the character with the Vector3 given
   public void SetDirection(Vector3 newDirection){
      direction = newDirection;
   }
}
