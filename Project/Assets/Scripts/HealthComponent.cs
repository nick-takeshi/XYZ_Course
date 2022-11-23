using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private UnityEvent _onDamage;
    [SerializeField] private UnityEvent _onDie;
    [SerializeField] private UnityEvent _onHealing;

    public void ApplyDamage(int damageValue)
    {
        if (_health < 0) return;

        _health -= damageValue;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Hero>()._session.Data._health -= damageValue;

        _onDamage?.Invoke();

        if (_health <= 0)
        {
            _onDie?.Invoke();
        }

        
    }
    public void HealHP(int healValue)
    {
        _health += healValue;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Hero>()._session.Data._health += healValue;

    }
}
