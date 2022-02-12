using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    public delegate void EnemyKilled();
    public static event EnemyKilled OnEnemyKilled;

    protected override void DestroyUnit()
    {
        gameObject.SetActive(false);
        if (OnEnemyKilled != null)
            OnEnemyKilled.Invoke();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            gameObject.SetActive(false);
        }
    }

}
