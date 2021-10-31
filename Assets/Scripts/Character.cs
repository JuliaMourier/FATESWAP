using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]

public class Character : MonoBehaviour
{
   private Vector3 direction;

   //if we want to had characters specifications
   virtual public void OnPowerActivate(){

   }

   virtual public void OnPowerDeactivate(){}

   public Vector3 GetDirection(){
        return direction;
   }
   public void SetDirection(Vector3 newDirection){
      direction = newDirection;
   }
}
