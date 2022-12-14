using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Defs/DefsFacade", fileName = "DefsFacade")]
public class DefsFacade : ScriptableObject
{
    [SerializeField] private InventoryItemsDefinition _items;
    [SerializeField] private PlayerDef _player;
    [SerializeField] private ThrowableItemsDef _throwableItems;
    [SerializeField] private UsableItemDef _usableItems;

    public UsableItemDef Usable => _usableItems;
    public InventoryItemsDefinition Items => _items;
    public ThrowableItemsDef Throwable => _throwableItems;
    public PlayerDef Player => _player;

    private static DefsFacade _instance;
    public static DefsFacade I => _instance == null ? LoadDefs() : _instance;

    private static DefsFacade LoadDefs()
    {
       return _instance = Resources.Load<DefsFacade>("DefsFacade");
    }
}
