using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]

public class Character : MonoBehaviour
{
   //if we want to had characters specifications
   virtual public void OnPowerActivate(){
      Debug.Log("Activate");
   }

   virtual public void OnPowerDeactivate(){}

}
