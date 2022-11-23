using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryData : MonoBehaviour
{
    [SerializeField] private List<InventoryItemData> _inventory = new List<InventoryItemData>();

    public delegate void OnInventaryChanged(string id, int value);

    public OnInventaryChanged OnChanged;
    public void Add(string id, int value)
    {
        if (value <= 0) return;

        var itemDef = DefsFacade.I.Items.Get(id);
        if (itemDef.IsVoid) return;
        
        var item = GetItem(id);

        if (item == null)
        {
            item = new InventoryItemData(id);
            _inventory.Add(item);
        }
       
        item.Value += value;
        OnChanged?.Invoke(id, Count(id));
    }

    public void Remove(string id, int value)
    {
        var itemDef = DefsFacade.I.Items.Get(id);
        if (itemDef.IsVoid) return;

        var item = GetItem(id);
        if (item == null) return;

        item.Value -= value;
        if (item.Value <= 0) _inventory.Remove(item);

        OnChanged?.Invoke(id, Count(id));

    }

    private InventoryItemData GetItem(string id)
    {
        foreach (var itemData in _inventory)
        {
            if (itemData.Id == id) return itemData;
        }
        return null;
    }

    public int Count(string id)
    {
        var count = 0;

        foreach (var item in _inventory)
        {
            if (item.Id == id) 
            {
                count += item.Value;
            }
        }
        return count;
    }
}

[Serializable]
public class InventoryItemData
{
    public string Id;
    public int Value;

    public InventoryItemData(string id)
    {
        Id = id;
    }
}
