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
        print("triggered");
        if (other.tag == "Player_Projectile")
        {
            print("tagged");
            ProjectileController proj = other.GetComponent<ProjectileController>();
            handleDamage(proj.damage);
            if (proj.piercing-- < 0)
            {
                Destroy(proj.gameObject);
            }
        }
    }

    public void handleDamage(float damage)
    {
        print("damaged");

        health -= damage;
    }

    public void handleDeath()
    {
        Destroy(gameObject);
    }
}
