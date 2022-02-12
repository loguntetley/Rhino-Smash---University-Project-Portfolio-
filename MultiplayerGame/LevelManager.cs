using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private int Enemies = 0;
    public Text EnemiesText;

    private int FindAmountOfEnemies()
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (object go in allObjects)
            Enemies++;
        return Enemies;
    }

    private void Awake()
    {
        Enemies = FindAmountOfEnemies();
        EnemiesText.text = Enemies.ToString();
        Enemy.OnEnemyKilled += OnEnemyKilledAction;
    }

    private void OnEnemyKilledAction()
    {
        Enemies--;
        EnemiesText.text = Enemies.ToString();
    }
}
