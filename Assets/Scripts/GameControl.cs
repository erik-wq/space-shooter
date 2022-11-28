using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject Game;
    public GameObject Menu;
    public GameObject roundWin;
    public GameObject roundLoss;
    public GameObject MoneyUpgrade;
    public GameObject settings;

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

        settings.SetActive(false);
        roundWin.SetActive(false);
        roundLoss.SetActive(false);
        MoneyUpgrade.SetActive(false);

        if (_spawner == null)
        {
            Debug.LogError("missing spawner script in child");
            return;
        }
        _player.ResetToStart();
        _spawner.Activate();
    }
    
    public void ShowWin()
    {
        _spawner.KillAllEnemies();
        _player.DestroyBullets();
        roundWin.SetActive(true);
        Game.SetActive(false);
        settings.SetActive(false);
    }

    public void ShowLoss()
    {
        _spawner.KillAllEnemies();
        _player.DestroyBullets();
        roundLoss.SetActive(true);
        Game.SetActive(false);
        settings.SetActive(false);
    }

    public void StartSetupGame()
    {
        _player.gameObject.SetActive(true);
        gameUI.SetActive(true);
        Game.SetActive(false);
    }

    public void ShowMenu()
    {
        Menu.SetActive(true);

        MoneyUpgrade.SetActive(false);
        roundWin.SetActive(false);
        roundLoss.SetActive(false);
        Game.SetActive(false);
        settings.SetActive(false);
    }

    public void ShowUpgradeMenu()
    {
        MoneyUpgrade.SetActive(true);
        settings.SetActive(false);
    }

    public void ShowSettings()
    {
        settings.SetActive(true);
        MoneyUpgrade.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
    
}
