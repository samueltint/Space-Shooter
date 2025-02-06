using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 10;

    void Update()
    {
        if (health <= 0)
        {
            handleDeath();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player_Projectile")
        {
            ProjectileController proj = other.GetComponent<ProjectileController>();
            handleDamage(proj.damage);
            proj.piercing--;
        }
    }

    public void handleDamage(float damage)
    {
        //TODO: damage anim
        health -= damage;
    }

    public void handleDeath()
    {
        Destroy(gameObject);
    }
}
