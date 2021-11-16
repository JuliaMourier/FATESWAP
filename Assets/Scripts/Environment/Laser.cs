using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    
    private LineRenderer lineRenderer; //linerenderer to draw the beam
    public Transform initialPos; //Position of the fire point
    public Transform endPosition; //Position of the end of teh laser

    private bool isLaserDangerous = true; //is laser lethal => allow to wait until hero take damage again
    
    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>(); //get the lineRenderer of the laser beam
    }

    void Update() {
       ShootLaser(); //Shoot laser at anytime
    }

    void ShootLaser(){
        //If there is an object in the path of the laser beam :
        if(Physics2D.Raycast(initialPos.position, Vector2.left)){
            //get the object hit and if it s a character : make it take damage
            RaycastHit2D hit = Physics2D.Raycast(initialPos.position, Vector2.left);
            if((hit.collider.gameObject.layer == LayerMask.NameToLayer("Characters")) && isLaserDangerous){
                isLaserDangerous = false; //stop the laser to be lethal for 0.7s
                StopAllCoroutines();
                StartCoroutine(WaitUntilDangerous()); //wait 0.7s and put back the laser beam to be lethal
                FindObjectOfType<GameManager>().HeroesTakeDamage(); //heroes loose a life        
            }
            Draw2DRay(initialPos.position, hit.point); //Draw the laser betwen the fire point and the hit object
        }
        else {
            Draw2DRay(initialPos.position,endPosition.position); //Draw the laser between the fire point and the wall (end of the laser)
        }
    }

    //Draw a red laser beam between a start position and a end position
    void Draw2DRay(Vector2 startPos, Vector2 endPos){
        lineRenderer.SetPosition(0,startPos);
        lineRenderer.SetPosition(1,endPos);
    }

    //Stop the laser beam to be lethal for 0.7s 
    private IEnumerator WaitUntilDangerous(){ 

        float duration = 0.7f; //Duration of the disability
        float elapsed = 0.0f;

        // wait
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            yield return null; //do nothing while waiting
        }

        isLaserDangerous = true; //The laser beam is lethal again

    }
}
