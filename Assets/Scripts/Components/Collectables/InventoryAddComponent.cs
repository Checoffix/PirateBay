using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

[SerializeField] 
public class InventoryAddComponent : MonoBehaviour
{
    [InventoryId][SerializeField] private string _id;
    [SerializeField] private int _value;
    [SerializeField] private UnityEvent _onSuccess;
    [SerializeField] private UnityEvent _onFail;
    
    public void Add(GameObject go)
    {
        var heroInventory = go.GetInterface<ICanAddInInventrory>();
        if (heroInventory != default)
        {
            if (heroInventory.Add(_id, _value)) _onSuccess?.Invoke();
            else _onFail?.Invoke();
        }
        else _onFail?.Invoke();
    }
}
