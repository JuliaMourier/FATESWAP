using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Target : MonoBehaviour
{
    private bool multi;

    private void Awake() {
        multi = FindObjectOfType<GameManager>().multi;
    }
    //When a fireball hits the target its gets deactivated
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Fireball")){
            this.gameObject.SetActive(false);
            if(multi){
                PhotonView photonView = PhotonView.Get(this);
                photonView.RPC("DisableTargetForAll", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    private void DisableTargetForAll(){
        this.gameObject.SetActive(false);
    }
}
