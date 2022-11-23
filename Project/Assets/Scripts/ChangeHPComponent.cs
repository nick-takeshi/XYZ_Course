using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHPComponent : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _healing;
    
    public void ApplyDamage(GameObject target)
    {
        
        var healthComponent = target.GetComponent<HealthComponent>();


        if (healthComponent != null)
        {
            healthComponent.ApplyDamage(_damage);
        }
       
    }

    public void ApplyHealing(GameObject target)
    {
        var healthComponent = target.GetComponent<HealthComponent>();


        if (healthComponent != null)
        {
            healthComponent.HealHP(_healing);
        }
    }
}
