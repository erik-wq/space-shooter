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
    public GameObject settingsIngame;

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
        _spawner.ResumeSpawning();
        Time.timeScale = 1;

        settings.SetActive(false);
        roundWin.SetActive(false);
        roundLoss.SetActive(false);
        MoneyUpgrade.SetActive(false);

        if (_spawner == null)
        {
            Debug.LogError("missing spawner script in child");
            return;
        }
        _spawner.KillAllEnemies();
        _player.ResetToStart();
        _spawner.Activate();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            _spawner.StopSpawning();
            Time.timeScale = 0;
            settingsIngame.SetActive(true);
            Menu.SetActive(false);
            Game.SetActive(false);
        }
    }

    public void Continue()
    {
        _spawner.ResumeSpawning();
        Time.timeScale = 1;
        settingsIngame.SetActive(false);
        Game.SetActive(true);
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
        settingsIngame.SetActive(false);
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
        Menu.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
    
}
