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
    public LayerMask enemyMask;

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
        var wait = new WaitForSeconds(0.05f);
        Wave currentWave = waves[level];
        print(currentWave);
        SpawnEnemy(currentWave);
        while (true)
        {
            if (_aliveEnmies == 0 && !currentWave.CanSpawn())
            {
                moneyMult *= currentWave.moneyMultiplayer;
                levelMult *= currentWave.levelMultiplayer;
                currentLevel += 1;
                Debug.Log("win " + _aliveEnmies);
                break;
            }
            if (time >= _spawnTime && maxEnemies > _aliveEnmies && currentWave.CanSpawn())
            {
                SpawnEnemy(currentWave);
            }
            if (maxEnemies > _aliveEnmies)
            {
                time += 0.05f;
            }
            yield return wait;
        }
        active = false;
    }
    private void CheckEnemies()
    {
        var enemies = GetComponentsInChildren<Enemy>();
        Debug.Log(enemies.Length);
        _aliveEnmies = enemies.Length;
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
        Vector3 pos = Vector3.zero;
        pos = SpawnPos();
        if(!CheckPosition(pos))
        {
            return;
        }
        _aliveEnmies += 1;
        var enem = Instantiate(enemy.enemy);
        pos.z = 0.1f;
        enem.Init(player, this, pos,enemy.movingSpeed * levelMult, enemy.baseLife * levelMult, enemy.baseDamage * levelMult, enemy.baseFireSpeed * levelMult, enemy.bulletSpeed * levelMult, enemy.money * moneyMult);
        enem.transform.SetParent(transform);
        ResetTimer();
        CheckEnemies();
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
    private bool CheckPosition(Vector3 position)
    {
        if(Physics2D.OverlapCircle(new Vector2(position.x,position.y), 1.2f , enemyMask))
        {
            return false;
        }
        return true;
    }
    #endregion
    private void ResetTimer()
    {
        time = 0;
        _spawnTime = Random.Range(minTime, maxTime);
    }

    public void DestroyEnemy()
    {
        CheckEnemies();
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
        wave.Reverse();
        foreach (var x in wave)
        {
            waves.Add(new Wave(x.levelEnemies, x.levelElites, x.normal, x.elite, x.boss, x.moneyMultiplayer, x.levelMultiplayer));
        }
    }
}
