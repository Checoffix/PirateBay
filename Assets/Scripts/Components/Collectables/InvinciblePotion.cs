using UnityEngine;

public class InvinciblePotion : MonoBehaviour
{
    [SerializeField] private float _duration;

    public void OnPickUp(GameObject _target)
    {
        var healthComponent = _target.GetComponent<HealthComponent>();
        if (healthComponent)
        {
            healthComponent.EnableInvincibleFrames(_duration);
        }    
    }
}
