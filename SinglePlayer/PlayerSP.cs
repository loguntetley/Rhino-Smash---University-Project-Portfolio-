using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSP : UnitSP
{
    public override void UnitDied()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy Bullet")
        {
            gameObject.SetActive(false);
        }
    }
}
