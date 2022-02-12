using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    void Update()
    {

        if (Input.GetKey(KeyCode.Space))
            Fire();

    }


}
