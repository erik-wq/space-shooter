using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Waves", menuName = "Wave/Wave Definition", order = 0)]
public class WaveDefinition : ScriptableObject
{
    public float levelNumber;
    
    [Header("enemies of level")]
    public float levelEnemies;
    public float levelElites;
    
    [Header("Which types of enemies will spawn in level")]
    public EnemyTypes[] normal;
    public EnemyTypes[] elite;
    public EnemyTypes[] boss;

    [Header("multiplayer")]
    [Tooltip("adds to overral multiplayer, this multiplayer makes enemies harder")]
    public float levelMultiplayer = 1;
    [Tooltip("adds to overral multiplayer, this multiplayer add how much  money you gain")]
    public float moneyMultiplayer = 1;
}
