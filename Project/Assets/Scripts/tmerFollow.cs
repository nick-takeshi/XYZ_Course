using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tmerFollow : MonoBehaviour
{
    [SerializeField] private Transform _hero;
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.position = new Vector3(_hero.position.x, _hero.position.y+0.75f, _hero.position.z);
    }
}
