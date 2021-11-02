using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fei : Character
{
    // Decrease the size of the BoxCollider of Fei when he's small
    public override void OnPowerActivate()
    {
        base.OnPowerActivate();
        GetComponent<BoxCollider2D>().size = new Vector2(0.37f, 0.4f);
        GetComponent<BoxCollider2D>().offset = new Vector2(0.1f, -0.2f);

    }

    // Increase the size of the BoxCollider of Fei when he's small
    public override void OnPowerDeactivate()
    {
        base.OnPowerDeactivate();
        GetComponent<BoxCollider2D>().size = new Vector2(0.6f, 0.66f);
        GetComponent<BoxCollider2D>().offset = new Vector2(0f, 0f);


    }
}
