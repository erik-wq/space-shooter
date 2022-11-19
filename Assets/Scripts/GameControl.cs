using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject Game;
    public GameObject Menu;

    private EnemySpawner _spawner;
    private void Awake()
    {
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
        _spawner.Activate();
    }
    public void ShowMenu()
    {
        Menu.SetActive(true);
        Game.SetActive(false);
    }
}
