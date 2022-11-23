using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSecondLevel : MonoBehaviour
{
    [SerializeField] private float _delay;
    public void LoadSecondLvl()
    {
        SceneManager.LoadScene("SecondLvl");
    }

    public void LoadInSomeSec()
    {
        Invoke("LoadSecondLvl", _delay);
    }
}
