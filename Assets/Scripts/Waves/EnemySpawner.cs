using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    public float ofset;
    public bool active { get; private set; }

    public float minTime;
    public float maxTime;
    public float maxEnemies;
    public Camera cam;

    public Transform player;

    private float time = 0;
    private float _spawnTime;
    private float _aliveEnmies = 0;

    private List<Wave> waves = new List<Wave>();
    private int currentLevel = 0;

    private float moneyMult = 1;
    private float levelMult = 1;

    private void Start()
    {
        active = false;
        WaveDefinition[] wave = Resources.LoadAll<WaveDefinition>("Waves/");
        CreateWaves(wave.OrderByDescending(x => x.levelNumber).ToList<WaveDefinition>());
        ResetTimer();
    }

    private IEnumerator RunningLevel(int level)
    {
        var wait = new WaitForSeconds(0.2f);
        Wave currentWave = waves[level];
        print(currentWave);
        SpawnEnemy(currentWave);
        while (true)
        {
            if (currentWave.CheckWin())
            {
                moneyMult *= currentWave.moneyMultiplayer;
                levelMult *= currentWave.levelMultiplayer;
                //currentLevel += 1;
                break;
            }
            if (time >= _spawnTime && maxEnemies > _aliveEnmies)
            {
                SpawnEnemy(currentWave);
            }
            if (maxEnemies > _aliveEnmies)
            {
                time += 0.2f;
            }
            yield return wait;
        }
        active = false;
    }

    #region spawning
    private void SpawnEnemy(Wave wave)
    {
        switch (wave.CheckNextEnemy())
        {
            case Enums.Enemies.normal:
                Spawn(wave.normal[Random.Range(0, wave.normal.Length)]);
                break;
            case Enums.Enemies.elite:
                Spawn(wave.elite[Random.Range(0, wave.elite.Length)]);
                break;
            case Enums.Enemies.boss:
                Spawn(wave.boss[Random.Range(0, wave.boss.Length)]);
                break;
        }
    }
    private void Spawn(EnemyTypes enemy)
    {
        _aliveEnmies += 1;
        var enem = Instantiate(enemy.enemy);
        Vector3 pos = Vector3.zero;
        pos = SpawnPos();
        pos.z = 0.1f;
        enem.Init(player, this, pos,enemy.movingSpeed * levelMult, enemy.baseLife * levelMult, enemy.baseDamage * levelMult, enemy.baseFireSpeed * levelMult, enemy.bulletSpeed * levelMult, enemy.money * moneyMult);
        ResetTimer();
    }

    private Vector3 SpawnPos()
    {
        if (Random.value < 0.5)
        {
            if (Random.value < 0.5)
            {
                return Top();
            }
            return Bottom();
        }
        if (Random.value < 0.5)
        {
            return Left();
        }
        return Right();
    }
    #endregion
    #region spawn positions
    private Vector3 Left()
    {
        Vector3 pos = cam.ScreenToWorldPoint(new Vector2(0, Random.Range(0, Screen.height)));
        pos.x -= ofset;
        return pos;
    }
    private Vector3 Right()
    {
        Vector3 pos = cam.ScreenToWorldPoint(new Vector2(Screen.width, Random.Range(0, Screen.height)));
        pos.x += ofset;
        return pos;
    }
    private Vector3 Top()
    {
        Vector3 pos = cam.ScreenToWorldPoint(new Vector2(Random.Range(0, Screen.width), Screen.height));
        pos.y += ofset;
        return pos;
    }
    private Vector3 Bottom()
    {
        Vector3 pos = cam.ScreenToWorldPoint(new Vector2(Random.Range(0, Screen.width), 0));
        pos.y -= ofset;
        return pos;
    }
    #endregion
    private void ResetTimer()
    {
        time = 0;
        _spawnTime = Random.Range(minTime, maxTime);
    }

    public void DestroyEnemy()
    {
        waves[currentLevel].KilledEnemy();
        this._aliveEnmies -= 1;
    }
    public void Activate()
    {
        if (waves.Count == 0 || active)
        {
            return;
        }
        active = true;
        if (!(currentLevel < waves.Count))
        {
            return;
        }
        StartCoroutine(RunningLevel(currentLevel));
    }
    private void CreateWaves(List<WaveDefinition> wave)
    {
        foreach (var x in wave)
        {
            waves.Add(new Wave(x.levelEnemies, x.levelElites, x.normal, x.elite, x.boss, x.moneyMultiplayer, x.levelMultiplayer));
        }
    }
}
