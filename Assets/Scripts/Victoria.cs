using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victoria : Character
{

    private bool isCapableOfThrowingFireBalls = false;

    public GameObject fireBallPrefab;

    public Transform content;

    public Animator animator; // Animator for the different animations of the character => idle, walk, transfor, walkpower and idlepower

    public override void OnPowerActivate()
    {
        base.OnPowerActivate();
        isCapableOfThrowingFireBalls = true;
    }

    public override void OnPowerDeactivate()
    {
        base.OnPowerDeactivate();
        isCapableOfThrowingFireBalls = false;
    }

    private void Update() {
        if(Input.GetKeyUp(KeyCode.X) && isCapableOfThrowingFireBalls){
            ThrowFireBall();
        }
    }

    public void ThrowFireBall(){
        animator.SetTrigger("throw");
        GameObject fireBall = Instantiate(fireBallPrefab);
        fireBall.transform.position = content.position;
        //fireBall.GetComponent<FireBall>().Victoria = this;
    }


}
