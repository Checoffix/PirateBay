using System.Collections;
using UnityEditor.Compilation;
using UnityEngine;

public abstract class Patrol : MonoBehaviour
{
    public abstract IEnumerator DoPatrol();
}