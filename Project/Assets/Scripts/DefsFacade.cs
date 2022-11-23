using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Defs/DefsFacade", fileName = "DefsFacade")]
public class DefsFacade : ScriptableObject
{
    [SerializeField] private InventoryItemsDefinition _items;

    public InventoryItemsDefinition Items => _items;
    
        
    

    private static DefsFacade _instance;
    public static DefsFacade I => _instance == null ? LoadDefs() : _instance;

    private static DefsFacade LoadDefs()
    {
       return _instance = Resources.Load<DefsFacade>("DefsFacade");
    }
}
