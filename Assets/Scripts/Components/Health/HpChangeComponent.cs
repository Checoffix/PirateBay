using UnityEngine;

public class HpChangeComponent : MonoBehaviour
{
    [SerializeField] private int _hpImpact;
    public void ApplyHpChange(GameObject target)
    {
        HealthComponent healthComponent = target.GetComponent<HealthComponent>();
        if (healthComponent)
        {
            healthComponent.ApplyHpChange(_hpImpact);
        }
    }
}
