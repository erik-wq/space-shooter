using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public float ofset;
    public bool active { get; private set; }
    //enemies control
    public float minTime;
    public float maxTime;
    public float maxEnemies;
    // other
    public Camera cam;

    public Transform player;
    public LayerMask enemyMask;
    private GameControl _gameControl;
    public TextMeshProUGUI levelText;
    // waves variables
    private float time = 0;
    private float _spawnTime;
    private float _aliveEnmies = 0;

    public List<Wave> waves = new List<Wave>();
    private int _currentLevel = 0;

    private float _moneyMult = 1;
    private float _levelMult = 1;
    private bool _stop = false;
    public bool earnMoney = true;

    public static EnemySpawner instance;

    private void Awake() 
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }
    private void Start()
    {
        _gameControl = GetComponentInParent<GameControl>();
        active = false;
        WaveDefinition[] wave = Resources.LoadAll<WaveDefinition>("Waves/");
        CreateWaves(wave.OrderByDescending(x => x.levelNumber).ToList<WaveDefinition>());
        ResetTimer();
        _gameControl.StartSetupGame();
    }

    private IEnumerator RunningLevel(int level)
    {
        levelText.SetText(("Level: " + (_currentLevel + 1) + " / " + waves.Count).ToString());
        var wait = new WaitForSeconds(0.05f);
        Wave currentWave = CopyCurentWave(level);
        print(currentWave.enemies);
        SpawnEnemy(currentWave);
        while (true)
        {
            if(_stop)
            {
                yield return new WaitForEndOfFrame();
                continue;
            }
            if (_aliveEnmies == 0 && !currentWave.CanSpawn())
            {
                _moneyMult *= currentWave.moneyMultiplayer;
                _levelMult *= currentWave.levelMultiplayer;
                _currentLevel += 1;
                Debug.Log("win " + _aliveEnmies);
                _gameControl.ShowWin();
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
    private Wave CopyCurentWave(int level)
    {
        return new Wave(waves[level].enemies, waves[level].elites, waves[level].normal,waves[level].elite,waves[level].boss,waves[level].moneyMultiplayer,waves[level].levelMultiplayer);
    }
    private void CheckEnemies()
    {
        var enemies = GetComponentsInChildren<Enemy>();
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
        enem.Init(player, this, pos,enemy.movingSpeed * _levelMult, enemy.baseLife * _levelMult, enemy.baseDamage * _levelMult, enemy.baseFireSpeed * _levelMult, enemy.bulletSpeed * _levelMult, enemy.money * _moneyMult);
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
        if (!(_currentLevel < waves.Count))
        {
            return;
        }
        StartCoroutine(RunningLevel(_currentLevel));
    }
    private void CreateWaves(List<WaveDefinition> wave)
    {
        wave.Reverse();
        foreach (var x in wave)
        {
            waves.Add(new Wave(x.levelEnemies, x.levelElites, x.normal, x.elite, x.boss, x.moneyMultiplayer, x.levelMultiplayer));
        }
    }
    public void KillAllEnemies()
    {
        earnMoney = false;
        Debug.Log(earnMoney);
        var enemies = GetComponentsInChildren<Enemy>();
        foreach(var x in enemies)
        {
            Destroy(x.gameObject);
        }
        var bullets = GetComponentsInChildren<Bullet>();
        foreach(var x in bullets)
        {
            Destroy(x.gameObject);
        }
        earnMoney = true;
        active = false;
        
    }
    public void StopSpawning()
    {
        _stop = true;
    }
    public void ResumeSpawning()
    {
        _stop = false;
    }
}
