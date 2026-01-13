using UnityEngine;

public class DestoryObjectComponent : MonoBehaviour
{
    [SerializeField] private GameObject _objectToDestroy;
    public void DestoyObject()
    {
        Destroy(_objectToDestroy);
    }
}
