using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject playerPrefab;
    // Start is called before the first frame update
    public void Start()
    {
        GameObject Player = Instantiate(playerPrefab);

    }
}
