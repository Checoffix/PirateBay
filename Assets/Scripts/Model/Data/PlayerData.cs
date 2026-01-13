using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [SerializeField] private InventoryData _inventory;
    public int hp;
    public InventoryData Inventory => _inventory;
}
