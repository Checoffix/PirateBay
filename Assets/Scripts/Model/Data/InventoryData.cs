using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class InventoryData
{
    [SerializeField] private List<InventoryItemData> _inventory = new();

    public delegate void OnInventoryChanged(string id, int value);

    public OnInventoryChanged OnChanged;

    public bool Add(string id, int value)
    {
        if (value <= 0) return false;
        if (IsItemDefNotExist(id)) return false;
        
        var item = GetItem(id);
        int maxSize = CheckItemMaxStackSize(id);
        if (item == null)
        {
            if (IsItemStackable(id))
            {
                if (value > maxSize)
                {
                    value = maxSize;
                }
                item = new InventoryItemData(id);
                _inventory.Add(item);
            }
            else
            {
                for (int i = 0; i < value; i++)
                {
                    item = new InventoryItemData(id);
                    _inventory.Add(item);
                    item.Value = 1;
                    OnChanged?.Invoke(id, Count(id));
                }
                return true;
            }
        }
        else if (value + item.Value > maxSize)
        {
            if (IsItemStackable(id))
            {
                value = maxSize - item.Value;
                if (value == 0) return false;
            }
            else
            {
                for (int i = 0; i < value; i++)
                {
                    item = new InventoryItemData(id);
                    _inventory.Add(item);
                    item.Value = 1;
                    OnChanged?.Invoke(id, Count(id));
                }
                return true;
            }
        }
        item.Value += value;
        OnChanged?.Invoke(id, Count(id));
        return true;
    }
    private bool IsItemDefNotExist(string id)
    {
        var itemDef = DefsFacade.I.Items.Get(id);
        return itemDef.IsVoid;
    }
    private bool IsItemStackable(string id)
    {
        bool isStackable = DefsFacade.I.Items.IsStackable(id);
        return isStackable;
    }
    private int CheckItemMaxStackSize(string id)
    {
        int maxSize = DefsFacade.I.Items.MaxSize(id);
        return maxSize;
    }

    public void Remove(string id, int value)
    {
        var item = GetItem(id);
        if (item == null) return;
        item.Value -= value;
        if (item.Value <= 0) _inventory.Remove(item);
        OnChanged?.Invoke(id, Count(id));
    }

    public void SetValue(string id, int value)
    {
        var item = GetItem(id);
        if (item == null)
        {
            item = new InventoryItemData(id);
            _inventory.Add(item);
        }
        item.Value = value;
        if (item.Value <= 0) _inventory.Remove(item);
    }

    public int Count(string id)
    {
        var item = GetItem(id);
        if (item == null) return 0;
        return item.Value;
    }
    public InventoryItemData GetItem(string id)
    {
        foreach (InventoryItemData item in _inventory)
        {
            if (item.Id == id) return item;
        }
        return null;
    }
    public List<InventoryItemData> GetAllItems()
    {
        return _inventory;
    }
    public void Reset()
    {
        _inventory.Clear();
    }
}

[Serializable]
public class InventoryItemData
{
    [InventoryId]public string Id;
    public int Value;

    public InventoryItemData(string id)
    {
        Id = id;
    }
}