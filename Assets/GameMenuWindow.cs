using System;
using System.Collections.Generic;
using Unity;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameMenuWindow : AnimatedWindow
{
    private GameObject menu;
    public void Awake()
    {
        menu = GameObject.Find("menu");
        menu.SetActive(false);
    }
    public void OnShowSettings()
    {
        var window = Resources.Load<GameObject>("UI/SettingsWindow");
        var canvas = FindObjectOfType<Canvas>();
        Instantiate(window, canvas.transform);
    }

    public void OnMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void OnRestart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void OnReesume()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
        menu.SetActive(true);
    }


}
