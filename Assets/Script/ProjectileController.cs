using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed,
        damage,
        lifetime,
        piercing;

    public void Initialize(float speed, float damage, float lifetime, float piercing, float size)
    {
        this.speed = speed;
        this.damage = damage;
        this.lifetime = lifetime;
        this.piercing = piercing;
        transform.localScale *= size;
    }

    void Update()
    {
        transform.position += transform.forward * speed;

        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
