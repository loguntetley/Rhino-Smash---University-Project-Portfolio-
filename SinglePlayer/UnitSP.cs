using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSP : MonoBehaviour
{
    public Slider HealthBar;
    [HideInInspector] public int health = 100;

    protected void TakeDamage(int damage)
    {
        health -= damage;
        //HealthBar.value = health;
        if (health <= 0)
            UnitDied();
    }

    public virtual void UnitDied() { }






}
