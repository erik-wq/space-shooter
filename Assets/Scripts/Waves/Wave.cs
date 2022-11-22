using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave
{
    public float enemies { get; private set; }
    private float elites;
    private int toElite;
    private int eliteCounter;

    public EnemyTypes[] normal { get; private set; }
    public EnemyTypes[] elite { get; private set; }
    public EnemyTypes[] boss { get; private set; }

    public float levelMultiplayer { get; private set; }
    public float moneyMultiplayer { get; private set; }

    public Wave(float enemies, float elites, EnemyTypes[] normal, EnemyTypes[] elite, EnemyTypes[] boss, float money, float level)
    {
        this.enemies = enemies;
        this.elites = elites;
        this.toElite = (int)(enemies / (elites + 1));
        this.eliteCounter = toElite;

        this.normal = normal;
        this.elite = elite;
        this.boss = boss;

        this.moneyMultiplayer = money;
        this.levelMultiplayer = level;
        Debug.Log(eliteCounter);
    }
    public Enums.Enemies CheckNextEnemy()
    {
        // Debug.Log(enemies);
        if (enemies == 1)
        {
            this.enemies -= 1;
            return Enums.Enemies.boss;
        }
        this.enemies -= 1;
        this.eliteCounter -= 1;
        if (eliteCounter <= 0)
        {
            eliteCounter = toElite;
            return Enums.Enemies.elite;
        }
        return Enums.Enemies.normal;
    }
    public bool CanSpawn()
    {
        if (enemies > 0)
        {
            return true;
        }
        return false;
    }
}
