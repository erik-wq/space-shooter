using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject Game;
    public GameObject Menu;

    private PlayerMovement _player;
    public GameObject gameUI;

    private EnemySpawner _spawner;
    private void Awake()
    {
        _player = GetComponentInChildren<PlayerMovement>(true);
        _spawner = GetComponentInChildren<EnemySpawner>(true);
    }
    public void StartGame()
    {
        Menu.SetActive(false);
        Game.SetActive(true);
        if(_spawner == null)
        {
            Debug.LogError("missing spawner script in child");
            return;
        }
        _player.ResetToStart();
        _spawner.Activate();
    }
    public void ShowMenu()
    {
        _spawner.KillAllEnemies();
        _player.DestroyBullets();
        Menu.SetActive(true);
        Game.SetActive(false);
    }
    public void StartSetupGame()
    {
        _player.gameObject.SetActive(true);
        gameUI.SetActive(true);
        Game.SetActive(false);
    }
}
