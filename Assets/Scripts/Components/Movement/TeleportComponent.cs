using UnityEngine;

public class TeleportComponent : MonoBehaviour
{
    [SerializeField] private Transform _destinationTransform;
    
    public void Teleport(GameObject _target)
    {
        _target.transform.position = _destinationTransform.position;
    }
}
