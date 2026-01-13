using System;
using UnityEngine;

public class SpawnListComponent : MonoBehaviour
{
    [SerializeField] private SpawnData[] _spawners;

    public void Spawn(string id)
    {
        foreach (var elem in _spawners) {
            if (elem.id == id)
            {
                elem.spawnComponent.Spawn();
                break;
            }
        }
    }

    [Serializable]
    public class SpawnData
    {
        public string id;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _target;
    }
}
