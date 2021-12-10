using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Fei : Character
{
    private bool multi = false;

    private void Awake() {
        multi = FindObjectOfType<GameManager>().multi;
    }


    // Decrease the size of the BoxCollider of Fei when he's small
    public override void OnPowerActivate()
    {   
        
        base.OnPowerActivate();
        if(!multi){
            GetComponent<BoxCollider2D>().size = new Vector2(0.37f, 0.4f);
            GetComponent<BoxCollider2D>().offset = new Vector2(0.1f, -0.2f);
        }
        else {
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("DecreaseRigidBodyForAll", RpcTarget.All);
        }
    }

    // Increase the size of the BoxCollider of Fei when he's small
    public override void OnPowerDeactivate()
    {
        base.OnPowerDeactivate();
        
        if(!multi){
            GetComponent<BoxCollider2D>().size = new Vector2(0.6f, 0.66f);
            GetComponent<BoxCollider2D>().offset = new Vector2(0f, 0f);
        }
        else {
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("IncreaseRigidBodyForAll", RpcTarget.All);
        }
    }

    [PunRPC]
    public void IncreaseRigidBodyForAll(){
        GetComponent<BoxCollider2D>().size = new Vector2(0.6f, 0.66f);
        GetComponent<BoxCollider2D>().offset = new Vector2(0f, 0f);
    }

    [PunRPC]
    public void DecreaseRigidBodyForAll(){
        GetComponent<BoxCollider2D>().size = new Vector2(0.37f, 0.4f);
        GetComponent<BoxCollider2D>().offset = new Vector2(0.1f, -0.2f);
    }

}
