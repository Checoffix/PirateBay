using UnityEngine;

public class ArmHeroComponent : MonoBehaviour
{
    public void ArmHero(GameObject gameObject)
    {
        gameObject.GetComponent<HeroAttack>().ArmHero();
    }
}
