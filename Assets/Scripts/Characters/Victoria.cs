using System.Collections;
using UnityEngine;
using Photon.Pun;

public class Victoria : Character
{
    
    private bool isCapableOfThrowingFireBalls = false; //is she capable of throwing fireballs

    public GameObject fireBallPrefab; //fireBall prefab

    public Transform hand; //Position of Victoria's hand (to throw fire ball)

    public Animator animator; // Animator for the animations of the character => throw

    public float durationDisabilityFireBall = 0.5f; //duration of disability of the power throwing fireballs

    public bool solo = false;
    private bool multi = false;

    private KeyCode shootKey;

    public AudioSource fireballSound;

    void Start()
    {
        solo = FindObjectOfType<GameManager>().solo;
        multi = FindObjectOfType<GameManager>().multi;
        if(solo){
            shootKey = GetComponent<MovementSolo>().shootKey;
        }
        else if (multi){
            shootKey = GetComponent<MovementMultiplayerMode>().shootKey;
        }
        else {
            shootKey = GetComponent<Movement>().shootKey;
        }

    }
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
    private void Update() {
        if (solo)
        {
            if(((Input.GetKeyUp(shootKey)) ||(Input.GetKeyUp(GetComponent<MovementSolo>().shootKey2))) && isCapableOfThrowingFireBalls && isPowerActivate){ //if her power are activates and she press X 
                ThrowFireBall(); //throw
                fireballSound.Play();
            }
        }
        else if(multi){
            if ((Input.GetKeyUp(shootKey) || Input.GetKeyUp(KeyCode.Joystick1Button2)) && isCapableOfThrowingFireBalls && isPowerActivate)
            { //if her power are activates and she press X 
                PhotonView photonView = PhotonView.Get(this);
                if(photonView.IsMine){
                    photonView.RPC("ThrowFireBallVisibleForAll", RpcTarget.All);
                }   
            }
        }    
        else {
            
            if (Input.GetKeyUp(GetComponent<Movement>().shootKey) && isCapableOfThrowingFireBalls && isPowerActivate)
            { //if her power are activates and she press W
                ThrowFireBall(); //throw
                fireballSound.Play();
            }
        }
    }

    //Intantiate a new fireball
    public void ThrowFireBall(){ 
        isCapableOfThrowingFireBalls = false; //Prevent throwing fireball again 
        animator.SetTrigger("throw"); //animate Victoria
        GameObject fireBall = Instantiate(fireBallPrefab); //instantiate the fireball
        fireBall.transform.position = hand.position; //set its position to the Victoria's hand
        fireBall.GetComponent<FireBall>().SetDirection(this.direction); //Set the direction to Victoria's direction        
        if(direction.x < -0.5){ //if the character goes left
        //swap the direction of the sprite
            fireBall.transform.rotation = Quaternion.Euler(new Vector3(this.transform.rotation.x, 180, this.transform.rotation.z));
        }
        StopAllCoroutines();//Stop other running routines
        StartCoroutine(WaitUntilAvailable()); //Will allow the throw in 0.5s
    }


     //Disable the fireball capability for 0.5s
    private IEnumerator WaitUntilAvailable(){ 

        float elapsed = 0.0f;

        // Animate the opening
        while (elapsed < durationDisabilityFireBall)
        {
            elapsed += Time.deltaTime;
            yield return null; //do nothing while waiting
        }

        isCapableOfThrowingFireBalls = true; //The fireball can be thrown again

    }

    [PunRPC]
    public void ThrowFireBallVisibleForAll(){ 
        isCapableOfThrowingFireBalls = false; //Prevent throwing fireball again 
        animator.SetTrigger("throw"); //animate Victoria
        GameObject fireBall = Instantiate(fireBallPrefab); //instantiate the fireball
        fireBall.transform.position = hand.position; //set its position to the Victoria's hand
        fireBall.GetComponent<FireBall>().SetDirection(this.direction); //Set the direction to Victoria's direction        
        if(direction.x < -0.5){ //if the character goes left
        //swap the direction of the sprite
            fireBall.transform.rotation = Quaternion.Euler(new Vector3(this.transform.rotation.x, 180, this.transform.rotation.z));
        }
        StopAllCoroutines();//Stop other running routines
        StartCoroutine(WaitUntilAvailable()); //Will allow the throw in 0.5s
    }

}
