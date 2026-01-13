using UnityEngine;

public class Projectile : BaseProjectile
{
    protected override void Start()
    {
        base.Start();
        var _force = new Vector2(Direction * Speed, 0);
        Rigidbody.AddForce(_force, ForceMode2D.Impulse);
    }
}
