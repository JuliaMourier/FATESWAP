using System.Collections;
using UnityEngine;


public class VictoriaSolo : Character
{


    private bool isCapableOfThrowingFireBalls = false; //is she capable of throwing fireballs

    public GameObject fireBallPrefab; //fireBall prefab

    public Transform hand; //Position of Victoria's hand (to throw fire ball)

    public Animator animator; // Animator for the animations of the character => throw

    public float durationDisabilityFireBall = 0.5f; //duration of disability of the power throwing fireballs
    public GameManager GameManager;
        //Vicsolo = GetComponent<GameManager>().solo;
 
    //When power is activated allow Victoria to throw fire balls
    public override void OnPowerActivate()
    {
        base.OnPowerActivate();
        isCapableOfThrowingFireBalls = true;
        
    }

    //When power is activated prevent Victoria to throw fire balls
    public override void OnPowerDeactivate()
    {
        base.OnPowerDeactivate();
        isCapableOfThrowingFireBalls = false;
    }

    //Check if Victoria wants to throw a fireball
    private void Update()
    {
        if (Input.GetKeyUp(GetComponent<MovementSolo>().shootKey) && isCapableOfThrowingFireBalls)
        { //if her power are activates and she press X 

            ThrowFireBall(); //throw
        }
    }

    //Intantiate a new fireball
    public void ThrowFireBall()
    {
        isCapableOfThrowingFireBalls = false; //Prevent throwing fireball again 
        animator.SetTrigger("throw"); //animate Victoria
        GameObject fireBall = Instantiate(fireBallPrefab); //instantiate the fireball
        fireBall.transform.position = hand.position; //set its position to the Victoria's hand
        fireBall.GetComponent<FireBall>().SetDirection(this.direction); //Set the direction to Victoria's direction        
        if (direction.x < -0.5)
        { //if the character goes left
          //swap the direction of the sprite
            fireBall.transform.rotation = Quaternion.Euler(new Vector3(this.transform.rotation.x, 180, this.transform.rotation.z));
        }
        StopAllCoroutines();//Stop other running routines
        StartCoroutine(WaitUntilAvailable()); //Will allow the throw in 0.5s
    }


    //Disable the fireball capability for 0.5s
    private IEnumerator WaitUntilAvailable()
    {

        float elapsed = 0.0f;

        // Animate the opening
        while (elapsed < durationDisabilityFireBall)
        {
            elapsed += Time.deltaTime;
            yield return null; //do nothing while waiting
        }

        isCapableOfThrowingFireBalls = true; //The fireball can be thrown again

    }

}
