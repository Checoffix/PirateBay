using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Defs/InventoryItems", fileName = "InventoryItems")]
public class InventoryItemsDefinitions : ScriptableObject
{
    [SerializeField] private ItemDef[] _items;

    public ItemDef Get(string id)
    {
        foreach (var item in _items)
        {
            if (item.Id == id) return item;
        }
        return default;
    }
    public bool IsStackable(string id)
    {
        foreach (var item in _items)
        {
            if (item.Id == id) return item.IsStackable;
        }
        return false;
    }
    public int MaxSize(string id)
    {
        foreach (var item in _items)
        {
            if (item.Id == id) return item.MaxStackSize;
        }
        return -1;
    }
#if UNITY_EDITOR
    public ItemDef[] ItemsForEditor => _items;
#endif
}
[Serializable]
public struct ItemDef
{
    [SerializeField] private string _id;
    [SerializeField] private bool _isStackable;
    [SerializeField] private int _maxStackSize;
    public readonly string Id => _id;
    public readonly bool IsStackable => _isStackable;
    public readonly int MaxStackSize => _maxStackSize;
    public readonly bool IsVoid => string.IsNullOrEmpty(_id);
}
