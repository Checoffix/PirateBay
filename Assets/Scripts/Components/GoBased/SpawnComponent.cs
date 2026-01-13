using System;
using UnityEngine;

public class SpawnComponent : MonoBehaviour
{
    [SerializeField] private Particles[] _particles;
    public void Spawn(string particleName)
    {
        foreach (var val in _particles)
        {
            if (val._name == particleName)
            {
                var instantiate = Instantiate(val._prefab, val._target.position, Quaternion.identity);
                instantiate.transform.localScale = transform.lossyScale;
            }
        }
    }
    [Serializable]
    public class Particles
    {
        public string _name;
        public GameObject _prefab;
        public Transform _target;
    }
}
