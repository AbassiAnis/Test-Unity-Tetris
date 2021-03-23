using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool isGamePaused = false;

    public static Action GameStarted;

    public static Action GameEnded;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogError("There is more the once instance of Game anager");
            return;
        }
    }

    public void StartGame()
    {
        GameStarted?.Invoke();
    }

    public void EndGame()
    {
        GameEnded?.Invoke();
    }
}