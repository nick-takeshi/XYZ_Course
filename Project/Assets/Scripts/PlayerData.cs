using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PlayerData : MonoBehaviour
{
    [SerializeField] private InventoryData _inventory;

    public int _health = 5;

    public InventoryData Inventory => _inventory;
    public PlayerData Clone()
    {
        var json = JsonUtility.ToJson(this);
        return JsonUtility.FromJson<PlayerData>(json);
    }
}


