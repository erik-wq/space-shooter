using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/EnemyType",order = 0)]
public class EnemyTypes : ScriptableObject
{
    public Enums.Enemies type;
    public float baseDamage;
    public float baseLife;
    public float baseFireSpeed;
    public float bulletSpeed;
    public float movingSpeed;
    public float money;
    public Enemy enemy;
}
