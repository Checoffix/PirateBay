using System.Collections.Generic;
using UnityEngine;

class CollectorComponent : MonoBehaviour, ICanAddInInventrory
{
    [SerializeField] private List<InventoryItemData> _items = new();

    public bool Add(string id, int value)
    {
        _items.Add(new InventoryItemData(id) { Value = value });
        return true;
    }

    public void DropInInventory()
    {
        var session = FindFirstObjectByType<GameSession>();
        foreach (var item in _items)
        {
            session.Data.Inventory.Add(item.Id, item.Value);
        }
        _items.Clear();
    }
}
