using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] private PlayerData _data;
    public PlayerData Data => _data;

    private PlayerData _save;

    private void Awake()
    {
        LoadHUD();

        if (IsSessionExit())
        {
            Destroy(gameObject);

        }
        else
        {
          
            DontDestroyOnLoad(this);
        }
    }

    private void LoadHUD()
    {
        SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
    }

    private bool IsSessionExit()
    {
        var sessions = FindObjectsOfType<GameSession>();

        foreach (var gameSession in sessions)
        {
            if (gameSession != this)
            {
                return true;
            }
        }
        return false;
    }
    public void Save()
    {
        _save = _data.Clone();
    }

    public void LoadLastSave()
    {
        _data = _save.Clone();
    }

}




